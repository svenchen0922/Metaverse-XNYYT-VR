using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class LoadVideo : MonoBehaviour
{
    //播放列表
    private  List<string> VideoPath = new List<string>();
    private string[] PathArray;
    //VideoPlayer 组件

    private VideoPlayer Video;

    private int VideoIndex=0;


    private bool IsTimer = false;
    private float Timer;

    public GameObject Gift;
    void Awake()
    {
        Video = transform.GetComponent<VideoPlayer>();
		

        LoadVideoPath();
    }
    void Start()
    {
        Video.prepareCompleted += StartVideoPlay;
        Video.loopPointReached += EndVideoPlay;

        PathArray = VideoPath.ToArray();
        Video.Prepare();
        Video.url = PathArray[0];
        Video.Play();

		foreach (Transform item in Gift.transform)
		{
			if (!(PathArray[0].IndexOf(item.name) == -1))
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
		if (IsTimer== true)
		{
            Timer -= Time.deltaTime;
            print(Timer);
			if (Timer<=0)
			{

             
              
            }
        }




    }




    public void LoadVideoPath()
    {
        DirectoryInfo info = new DirectoryInfo(Application.streamingAssetsPath + "");
        FileInfo[] fileInfo = info.GetFiles("*.mp4");
        FileInfo[] fileInfo1 = info.GetFiles("*.avi");
        foreach (var item in fileInfo)
        {
            VideoPath.Add(item.FullName);
        }
        foreach (var item in fileInfo1)
        {
            VideoPath.Add(item.FullName);
        }
  
    }

   

    private void StartVideoPlay(VideoPlayer source)
    {
        //计算得出视频时长
        Timer = (source.frameCount / source.frameRate);
        print(Timer);
        IsTimer = true;
    }

  
    private void EndVideoPlay(VideoPlayer source)
    {
        VideoIndex = VideoIndex + 1;

		if (VideoIndex>=VideoPath.Count)
		{
            VideoIndex = 0;
		}

        Video.prepareCompleted -= StartVideoPlay;
        Video.prepareCompleted += StartVideoPlay;


        Video.url = PathArray[VideoIndex];

  
			foreach (Transform item in Gift.transform)
			{
            if (!(PathArray[VideoIndex].IndexOf(item.name) == -1))
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
			}

        

        Video.Prepare();
        Video.Play();
    }


}
