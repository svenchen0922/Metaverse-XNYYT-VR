using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using UnityEngine;
using UnityEngine.UI;

public class AuthUI : MonoBehaviour
{

    protected readonly static string host = "https://meta.hb.icbc.com.cn/icbc/hbfh/metaweb/app";

    protected readonly static string vrBankHost = "https://meta.hb.icbc.com.cn/icbc/hbfh/metaweb/vrbank/vrbank";

    protected readonly static string key = "#<&>ICBCApp[^%]%";

    protected readonly static string iv = "#@&*APPICBC)^%(%";

    //图片验证码
    [SerializeField]
    protected Image codeImage;

    //图片验证码token
    protected string codeToken;

    //短信验证码token
    protected string smsToken;

    //手机号输入框
    [SerializeField]
    protected InputField phoneInput;

    //密码输入框
    [SerializeField]
    protected InputField passwordInput;

    //图片验证码输入框
    [SerializeField]
    protected InputField codeInput;

    //发送短信验证码按钮
    [SerializeField]
    protected Button sendSmsCodeBtn;

    //信息提示
    [SerializeField]
    protected Text msgText;

    //获取图形验证码
    public void GetCaptcha()
    {
        Text codeImageTip = this.codeImage.GetComponentInChildren<Text>();
        codeImageTip.text = "正在获取验证码...";
        try
        {
            JsonData retJson = this.InvokeHBFH("/captcha/getCaptcha", null, "GET");
            JsonData data = retJson["data"];

            string imageBase64 = data["image_str"].ToString();
            this.codeToken = data["code_token"].ToString();

            Debug.Log("Image Base64:" + imageBase64);
            Debug.Log("Code Token:" + this.codeToken);

            //显示验证码图片
            byte[] bytes = Convert.FromBase64String(imageBase64);
            Texture2D texture = new Texture2D((int)this.codeImage.rectTransform.rect.width, (int)this.codeImage.rectTransform.rect.height);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            this.codeImage.sprite = sprite;
            codeImageTip.text = "";
        }
        catch (Exception e)
        {
            codeImageTip.text = "获取验证码失败  " + e.Message;
            Debug.LogError("获取验证码失败:" + e.Message + " | " + e.StackTrace);
        }
    }

    //获取短信验证码
    public void GetSmsCode()
    {
        msgText.text = "";
        string phoneNum = phoneInput.text;
        if (!string.IsNullOrEmpty(phoneNum))
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                ["phone"] = DesUtil.Encrypt(phoneNum, key, iv)
            };
            JsonData retJson = this.InvokeHBFH("/sms/sendSMS", parameters, "POST");
            JsonData data = retJson["data"];
            this.smsToken = data["sms_token"].ToString();
            Debug.Log("SMS Token:" + smsToken);
        }
        else
        {
            msgText.text = "手机号未输入";
        }
    }

    //组件打开时
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
        this.msgText.text = "";
        this.GetCaptcha();
    }


    protected void Start()
    {
        this.msgText.text = "";
        this.codeToken = "";
        this.smsToken = "";
        //按钮事件
        this.codeImage.gameObject.GetComponent<Button>().onClick.AddListener(GetCaptcha);
        if (null != this.sendSmsCodeBtn)
        {
            this.sendSmsCodeBtn.onClick.AddListener(GetSmsCode);
        }
    }


    public JsonData InvokeHBFH(string api, Dictionary<string, object> parameters, string method, Dictionary<string, string> headers = null)
    {
        string url = host + api;
        WebClient webClient = new WebClient();
        webClient.Headers.Add("Content-Type", "application/json;charset=utf-8");
        webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.134 Safari/537.36 Edg/103.0.1264.77");
        if (headers == null)
        {
            headers = new Dictionary<string, string>();
        }

        //添加header
        foreach (var header in headers)
        {
            webClient.Headers.Add(header.Key, header.Value);
        }

        string res = "";
        if ("POST".Equals(method))
        {
            //将参数转成JSON
            string jsonStr = JsonMapper.ToJson(parameters);
            byte[] dataBytes = Encoding.UTF8.GetBytes(jsonStr);
            Debug.Log(api + " REQ: " + jsonStr);
            byte[] respBytes = webClient.UploadData(url, "POST", dataBytes);
            res = Encoding.UTF8.GetString(respBytes);
        }
        else if ("GET".Equals(method))
        {
            res = webClient.DownloadString(url);
        }

        //HttpWebRequest request = WebRequest.Create(host + api) as HttpWebRequest;

        //if ("POST".Equals(method))
        //{
        //    request.Method = "POST";
        //    request.ContentType = "application/json;charset=utf-8";

        //    将参数转成JSON
        //    string jsonStr = JsonMapper.ToJson(parameters);
        //    byte[] payload = Encoding.UTF8.GetBytes(jsonStr);
        //    Debug.Log(api + " REQ: " + jsonStr);


        //    request.ContentLength = payload.Length;
        //    Stream writer = request.GetRequestStream();
        //    writer.Write(payload, 0, payload.Length);
        //    writer.Close();
        //}
        //else if ("GET".Equals(method))
        //{
        //    request.Method = "GET";
        //}

        //request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.134 Safari/537.36 Edg/103.0.1264.77";

        //if (headers == null)
        //{
        //    headers = new Dictionary<string, string>();
        //}

        //添加header
        //foreach (var header in headers)
        //{
        //    string value = HttpUtility.UrlEncode(header.Value, Encoding.UTF8);
        //    request.Headers.Set(header.Key, value);
        //}

        //打印所有的header
        //foreach (var key in request.Headers.AllKeys)
        //{
        //    Debug.Log(api + " Header: " + key + ": " + request.Headers[key]);
        //}

        //发起请求
        //HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

        //Debug.Log(api + " Status Code: " + resp.StatusCode);

        //检查状态码
        //if (resp.StatusCode != HttpStatusCode.OK)
        //{
        //    throw new Exception("Status Code: " + resp.StatusCode);
        //}

        //将报文转成json
        //Stream respStream = resp.GetResponseStream();
        //StreamReader streamReader = new StreamReader(respStream, Encoding.UTF8);
        //string res = streamReader.ReadToEnd();
        //streamReader.Close();
        //resp.Close();
        //request.Abort();
        if (string.IsNullOrEmpty(res))
        {
            throw new Exception("返回报文为null或空");
        }
        Debug.Log(api + " RESP: " + res);

        JsonData retJson;
        try
        {
            retJson = JsonMapper.ToObject(res);
        }
        catch (Exception e)
        {
            throw new Exception("返回报文解析失败：" + e.Message);
        }

        if (null == retJson)
        {
            throw new Exception("返回报文为空");
        }

        //判断返回码code
        if (0 != (int)retJson["code"])
        {
            throw new Exception(retJson["msg"].ToString());
        }

        //返回retJson
        return retJson;
    }


    public void InvokeVrBankLogin(string h5Token, string modelId, string nickName, string gender)
    {
        string url = vrBankHost + "/session/login";
        WebClient webClient = new WebClient();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.134 Safari/537.36 Edg/103.0.1264.77");


        //HttpWebRequest request = WebRequest.Create(vrBankHost + "/session/login") as HttpWebRequest;
        //request.Method = "POST";
        //request.ContentType = "application/json;charset=utf-8";

        //上送参数
        Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            ["h5Token"] = h5Token,
            ["modelId"] = modelId,
            ["nickName"] = nickName,
            ["gender"] = gender,
        };

        //将参数转成JSON
        string jsonStr = JsonMapper.ToJson(parameters);
        byte[] dataBytes = Encoding.UTF8.GetBytes(jsonStr);
        Debug.Log("VRBANK LOGIN REQ: " + jsonStr);
        byte[] respBytes = webClient.UploadData(url, "POST", dataBytes);
        string res = Encoding.UTF8.GetString(respBytes);

        ////将参数转成JSON
        //string jsonStr = JsonMapper.ToJson(parameters);
        //byte[] payload = Encoding.UTF8.GetBytes(jsonStr);
        //Debug.Log("VRBANK LOGIN REQ: " + jsonStr);


        //request.ContentLength = payload.Length;
        //Stream writer = request.GetRequestStream();
        //writer.Write(payload, 0, payload.Length);
        //writer.Close();

        ////发送请求
        //HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

        ////检查状态码
        //if (resp.StatusCode != HttpStatusCode.OK)
        //{
        //    throw new Exception("Status Code: " + resp.StatusCode);
        //}

        ////将报文转成json
        //Stream respStream = resp.GetResponseStream();
        //StreamReader streamReader = new StreamReader(respStream, Encoding.UTF8);
        //string res = streamReader.ReadToEnd();

        if (string.IsNullOrEmpty(res))
        {
            throw new Exception("VRBANK LOGIN 返回报文为null或空");
        }
        Debug.Log("VRBANK LOGIN RESP: " + res);

        JsonData retJson;
        try
        {
            retJson = JsonMapper.ToObject(res);
        }
        catch (Exception e)
        {
            throw new Exception("VRBANK LOGIN 返回报文解析失败：" + e.Message);
        }

        if (null == retJson)
        {
            throw new Exception("VRBANK LOGIN 返回报文为空");
        }

        string retCode = retJson["retCode"].ToString();
        //判断返回码code
        if (!"0".Equals(retCode))
        {
            throw new Exception("VRBANK LOGIN FAIL, retCode=" + retCode);
        }
    }
}
