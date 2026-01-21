using LitJson;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit;

public class YingXiaoScreen : MonoBehaviour
{
    [SerializeField]
    private string host = "https://meta.hb.icbc.com.cn/icbc/hbfh/metaweb/vrbank";

    private VideoPlayer videoPlayer;

    private List<ProductInfo> productInfos = new List<ProductInfo>();

    private int index = 0;

    private float timer = 0;

    [SerializeField]
    private GameObject wan;

    [SerializeField]
    private GameObject NPC;
    [SerializeField]
    private  XRGrabInteractable grab_1;
    [SerializeField]
    private GameObject RegMessage;

 


    private ProductInfo currentProduct;




    private void GrabHandler(SelectEnterEventArgs a)
    {
        JsonData jsonObj =  Follow();
        grab_1.gameObject.SetActive(false);
        print(jsonObj);
        string temp = ((string)jsonObj["retMsg"]);
        
        RegMessage.SetActive(true);
        RegMessage.transform.GetChild(0).GetComponent<TextMesh>().text= ((string)jsonObj["retMsg"]);

    }

    void Start()
    {
        grab_1.selectEntered.AddListener(GrabHandler);
      

        videoPlayer = GetComponent<VideoPlayer>();
        string url = host + "/vrbank/productInfo/list";
        WebClient webClient = new WebClient();
        webClient.Headers.Add("token", PlayerInfo.Instance.Token);
        string res = webClient.DownloadString(url);
        Debug.Log(res);
        if (!string.IsNullOrEmpty(res))
        {
            JsonData data = JsonMapper.ToObject(res);
            for (int i = 0; i < data.Count; i++)
            {
                string str = JsonMapper.ToJson(data[i]);
                ProductInfo product = JsonMapper.ToObject<ProductInfo>(str);
                productInfos.Add(product);
            }
        }
    }

    void Update()
    {
        if (productInfos.Count > 0)
        {
            if (timer >= 3)
            {
                if (!videoPlayer.isPlaying || (videoPlayer.isPlaying && videoPlayer.frame == (long)videoPlayer.frameCount))
                {
                    if (videoPlayer.isPlaying)
                    {
                        videoPlayer.Stop();
                    }
                    int prodIndex = index % productInfos.Count;
                    ProductInfo product = productInfos[prodIndex];
                    if (product.videoPath.Count > 0)
                    {
                        string url = product.videoPath[0];
                        Debug.Log("index:" + prodIndex + " url: " + url);
                        videoPlayer.source = VideoSource.Url;
                        videoPlayer.url = url;

                        videoPlayer.gameObject.GetComponent<Renderer>().material.color = Color.white;
                        videoPlayer.Play();
                        currentProduct = product;
                        if ("–«∫”ÕÚ¿Ô".Equals(product.productDetail))
                        {
                         
                            NPC.GetComponent<Animator>().SetBool("JieShao", true);
                        }
                        else
                        {
                       
                        }
                        index++;
                    }
                    else
                    {
                        return;
                    }
                }
                timer = 0;
            }
            else
            {
               
            }
        }

        timer += Time.deltaTime;
        wan.transform.Rotate(wan.transform.up * Time.deltaTime * 10);
    }

    public JsonData Follow()
    {
        string url = host + "/vrbank/productInfo/follow/" + currentProduct.productId;
        WebClient webClient = new WebClient();
        webClient.Headers.Add("token", PlayerInfo.Instance.Token);
        byte[] respBytes = webClient.UploadData(url, "POST", new byte[0]);
        string res = Encoding.UTF8.GetString(respBytes);
        Debug.Log(res);
        return JsonMapper.ToObject(res);
    }
}
