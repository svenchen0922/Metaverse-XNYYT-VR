using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUI : AuthUI
{
    //登录按钮
    [SerializeField]
    private Button loginBtn;

    //注册按钮
    [SerializeField]
    private Button registerBtn;

    //注册面板
    [SerializeField]
    private AuthUI resgiterPanel;


    private IEnumerator Login()
    {
        msgText.text = "正在登录，请稍后...";
        yield return new WaitForSeconds(1);
        try
        {
            loginBtn.enabled = false;
            phoneInput.enabled = false;
            passwordInput.enabled = false;
            codeInput.enabled = false;
            codeImage.enabled = false;

            string phone = phoneInput.text;
            string password = passwordInput.text;
            string code = codeInput.text;

            //数据合法性校验
            if (string.IsNullOrEmpty(phone))
            {
                throw new Exception("手机号不能为空");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("密码不能为空");
            }

            if (string.IsNullOrEmpty(code))
            {
                throw new Exception("验证码不能为空");
            }

            //登录
            JsonData payerJson = Login(phone, password, code, codeToken);
            PlayerInfo.Instance.SetLoginData(payerJson);
            msgText.text = "登录成功";
            VR_SceneManager.Instance.LoadScene("VR_Office");

        }
        catch (Exception e)
        {
            Debug.LogException(e);
            msgText.text = "登录失败  " + e.Message;
        }
        finally
        {
            loginBtn.enabled = true;
            phoneInput.enabled = true;
            passwordInput.enabled = true;
            codeInput.enabled = true;
            codeImage.enabled = true;
        }
    }

    public void OnLoginBtnClick()
    {
        StartCoroutine(Login());
    }

    public void OnRegisterBtnClick()
    {
        resgiterPanel.OnOpen();
        this.gameObject.SetActive(false);
        GlobalData.Instance.IsLogin = false;
    }

    private new void Start()
    {
        base.Start();
        loginBtn.onClick.AddListener(OnLoginBtnClick);
        registerBtn.onClick.AddListener(OnRegisterBtnClick);
        OnOpen();
    }

    private JsonData Login(string phone, string password, string captcha, string codeToken)
    {
        //加密手机号
        string phoneNum = DesUtil.Encrypt(phone, key, iv);

        //获取登录的盐
        Dictionary<string, object> parameters1 = new Dictionary<string, object>()
        {
            ["phone"] = phoneNum
        };
        JsonData retJson1 = this.InvokeHBFH("/tsUser/checkUserLoginByPhone", parameters1, "POST");
        JsonData data1 = retJson1["data"];
        string saltStr = (string)data1["salt"];

        //使用盐对密码MD5加密
        string pwdEncrypted = MD5Util.Encrypt(password, saltStr);

        //构造上送报文
        Dictionary<string, object> parameters2 = new Dictionary<string, object>()
        {
            ["phone"] = phoneNum,
            ["password"] = pwdEncrypted,
            ["captcha"] = captcha,
            ["code_token"] = codeToken,
            ["salt"] = saltStr
        };

        //调用后台
        JsonData retJson2 = this.InvokeHBFH("/tsUser/login", parameters2, "POST");

        //获取Token
        JsonData _params = retJson2["params"];
        string token = (string)_params["token"];
        Debug.Log("token:" + token);
        GlobalData.Instance.token = token;

        //获取modelId，name，gender
        JsonData data2 = retJson2["data"];
        string modelId = (string)data2["model_id"];
        string name = (string)data2["name"];
        string gender = (int)data2["sex"] + "";

        //调用自己的后台登录接口
        this.InvokeVrBankLogin(token, modelId, name, gender);

        JsonData palyerInfo = new JsonData
        {
            ["phone"] = phone,
            ["nickName"] = name,
            ["gender"] = gender,
            ["modelId"] = modelId,
            ["token"] = token
        };
        return palyerInfo;
    }
}
