using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class LoadVideo_2 : MonoBehaviour
{

    private VideoClip[] PathArray;
    //VideoPlayer ×é¼þ

    private VideoPlayer Video;

    private int VideoIndex=0;


    private double Timer;

    public GameObject Gift;

    public VideoClip VC_1;
    public VideoClip VC_2;
    public VideoClip VC_3;
    public VideoClip VC_4;
    void Awake()
    {
        Video = transform.GetComponent<VideoPlayer>();

        PathArray = new VideoClip[] { VC_1, VC_2, VC_3, VC_4 };


    }
    void Start()
    {
     

        Video.clip = PathArray[0];
        Timer = Video.clip.length;
        foreach (Transform item in Gift.transform)
		{
			if (!(PathArray[0].name.IndexOf(item.name) == -1))
			{
				item.gameObject.SetActive(true);
			}
			else
			{
				item.gameObject.SetActive(false);
			}
		}
	}

    // Update is called once per frame
    void Update()
    {

        Timer -= Time.deltaTime;
        print(Timer);
        if (Timer <= 0)
        {
            VideoIndex = VideoIndex + 1;
            if (VideoIndex >= PathArray.Length)
            {
                VideoIndex = 0;

            }

            Video.clip = PathArray[VideoIndex];

            foreach (Transform item in Gift.transform)
            {
                if (!(PathArray[VideoIndex].name.IndexOf(item.name) == -1))
                {
                    item.gameObject.SetActive(true);
                }
                else
                {
                    item.gameObject.SetActive(false);
                }
            }
            Timer = Video.clip.length;
			

        }


    }





   

   


}
