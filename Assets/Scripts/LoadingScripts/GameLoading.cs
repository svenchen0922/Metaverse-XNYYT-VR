using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameLoading : MonoBehaviour
{
    private string jsonUrl = "https://meta.hb.icbc.com.cn/icbc/hbfh/metaweb/app/" + "tsUser/getSystemInfo/";
    private string token = GlobalData.Instance.token;
    public VideoPlayer vid_1;
    public VideoPlayer vid_2;
    public VideoPlayer vid_3;
    private string resOtherPath = "https://meta.hb.icbc.com.cn/icbc/hbfh/metacontent";
    
    public RawImage pic;

    void Start()
    {
        StartCoroutine(get_Request(jsonUrl, token));
        
    }


    IEnumerator get_Request(string _url, string _token)
    {
        UnityWebRequest www = UnityWebRequest.Get(_url);
        //请求添加token验证
        www.SetRequestHeader("token", _token);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
       
            Debug.Log(www.downloadHandler);
            Debug.Log(www.downloadHandler.text);
            Debug.Log("获取成功!");
        }

        JsonData jsonObj = JsonMapper.ToObject(www.downloadHandler.text);

        var temp_1 = jsonObj["data"];

        GlobalData.Instance.apple_photo_one=((string)temp_1["apple_photo_one"]);
        GlobalData.Instance.apple_photo_two=((string)temp_1["apple_photo_two"]);
        GlobalData.Instance.apple_photo_three=((string)temp_1["apple_photo_three"]);
        GlobalData.Instance.apple_photo_four=((string)temp_1["apple_photo_four"]);
        GlobalData.Instance.apple_photo_five=((string)temp_1["apple_photo_five"]);
        var temp_2 = temp_1["adv_info_dto"];
        GlobalData.Instance.VideoURL = (string)temp_2["file_path"];

        GlobalData.Instance.IS_Ready = true;


        vid_1.url = resOtherPath + GlobalData.Instance.VideoURL;
        vid_2.url = resOtherPath + GlobalData.Instance.VideoURL;
        vid_3.url = resOtherPath + GlobalData.Instance.VideoURL;

        vid_1.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        vid_2.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        vid_3.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }




 
}
