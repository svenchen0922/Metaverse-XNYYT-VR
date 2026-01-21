using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicturePlay : MonoBehaviour
{
    private string resOtherPath = "http://122.51.65.88:8093/";
    public RawImage rawImage;
    public int Index=1;

    public float timer = 2.0f; // ∂® ±2√Î
    void Update()
    {
        if (GlobalData.Instance.IS_Ready == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                switch (Index % 5)
                {
                    case 1:
                    
                        StartCoroutine(LoadTeture(resOtherPath + GlobalData.Instance.apple_photo_one, (tex) => {
                            rawImage.texture = tex;
                        }));
                        break;
                    case 2:

                        StartCoroutine(LoadTeture(resOtherPath + GlobalData.Instance.apple_photo_two, (tex) => {
                            rawImage.texture = tex;
                        }));
                        break;
                    case 3:

                        StartCoroutine(LoadTeture(resOtherPath + GlobalData.Instance.apple_photo_three, (tex) => {
                            rawImage.texture = tex;
                        }));
                        break;
                    case 4:

                        StartCoroutine(LoadTeture(resOtherPath + GlobalData.Instance.apple_photo_four, (tex) => {
                            rawImage.texture = tex;
                        }));
                        break;
                    case 0:

                        StartCoroutine(LoadTeture(resOtherPath + GlobalData.Instance.apple_photo_five, (tex) => {
                            rawImage.texture = tex;
                        }));
                        break;
                    default:
                        break;
                }
                Index = Index + 1;
                timer = 2.0f;

            };
         
            }
        }
    IEnumerator LoadTeture(string url, Action<Texture2D> cb)
    {
      
     yield return LoadImageMgr.instance.LoadImage(url, cb);
         
    

    }
}


