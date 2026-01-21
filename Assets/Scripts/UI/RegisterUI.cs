using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterUI : AuthUI
{
    //姓名输入框
    [SerializeField]
    private InputField nameInput;

    //短信验证码输入框
    [SerializeField]
    private InputField smsCodeInput;

    //模型选择组件
    [SerializeField]
    private ModelSelectUI modelSelectUI;

    //注册按钮
    [SerializeField]
    private Button registerBtn;

    //返回登录界面按钮
    [SerializeField]
    private Button backBtn;

    //登录面板
    [SerializeField]
    private AuthUI loginPanel;


    private IEnumerator Register()
    {
        msgText.text = "正在注册，请稍后...";
        yield return new WaitForSeconds(1);
        try
        {
            registerBtn.enabled = false;
            phoneInput.enabled = false;
            nameInput.enabled = false;
            passwordInput.enabled = false;
            codeInput.enabled = false;
            smsCodeInput.enabled = false;
            codeImage.enabled = false;

            string phone = phoneInput.text;
            string name = nameInput.text;
            string password = passwordInput.text;
            string code = codeInput.text;
            string smsCode = smsCodeInput.text;

            //数据合法性校验
            if (string.IsNullOrEmpty(phone))
            {
                throw new Exception("手机号不能为空");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("姓名不能为空");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("密码不能为空");
            }

            if (string.IsNullOrEmpty(code))
            {
                throw new Exception("验证码不能为空");
            }

            if (string.IsNullOrEmpty(smsCode))
            {
                throw new Exception("短信验证码不能为空");
            }

            //注册
            JsonData retData = Register(phone, name, password, code, codeToken, smsCode, smsToken, modelSelectUI.CurrentModel.modelId, modelSelectUI.CurrentModel.gender);
            msgText.text = "注册成功";

        }
        catch (Exception e)
        {
            Debug.LogException(e);
            msgText.text = "注册失败  " + e.Message;
        }
        finally
        {
            registerBtn.enabled = true;
            phoneInput.enabled = true;
            nameInput.enabled = true;
            passwordInput.enabled = true;
            codeInput.enabled = true;
            smsCodeInput.enabled = true;
            codeImage.enabled = true;
        }
    }

    public void OnRegisterBtnClick()
    {
        StartCoroutine(Register());
    }

    public void OnBackClick()
    {
        loginPanel.OnOpen();
        this.gameObject.SetActive(false);
        GlobalData.Instance.IsLogin = true;

    }

    private new void Start()
    {
        base.Start();
        registerBtn.onClick.AddListener(OnRegisterBtnClick);
        backBtn.onClick.AddListener(OnBackClick);
    }

    private JsonData Register(string phone, string name, string password, string captcha, string codeToken, string smsCode, string smsToken, string modelId, string gender)
    {
        //加密手机号
        string phoneNum = DesUtil.Encrypt(phone, key, iv);

        //获取注册的盐
        Dictionary<string, object> parameters1 = new Dictionary<string, object>()
        {
            ["phone"] = phoneNum
        };
        JsonData retJson1 = this.InvokeHBFH("/tsUser/checkUserRegistryByPhone", parameters1, "POST");
        JsonData data1 = retJson1["data"];
        string saltStr = (string)data1["salt"];

        //使用盐对密码MD5加密
        string pwdEncrypted = MD5Util.Encrypt(password, saltStr);

        //构造上送报文
        Dictionary<string, object> parameters2 = new Dictionary<string, object>()
        {
            ["phone"] = phoneNum,
            ["password"] = pwdEncrypted,
            ["name"] = name,
            ["captcha"] = captcha,
            ["code_token"] = codeToken,
            ["sms_code"] = smsCode,
            ["sms_token"] = smsToken,
            ["salt"] = saltStr
        };

        //调用后台注册
        JsonData retJson2 = this.InvokeHBFH("/tsUser/register", parameters2, "POST");

        //获取Token
        JsonData _params = retJson2["params"];
        string token = (string)_params["token"];
        Debug.Log("token:" + token);
        GlobalData.Instance.token = token;
        //选模型和性别
        Dictionary<string, object> parameters3 = new Dictionary<string, object>()
        {
            ["model_id"] = modelId,
            ["sex"] = int.Parse(gender),

        };

        Dictionary<string, string> headers = new Dictionary<string, string>()
        {
            ["token"] = token
        };

        JsonData retJson3 = this.InvokeHBFH("/tsUser/updateUserModel", parameters3, "POST", headers);
        return retJson3;
    }
}
 