using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 文件中的视频的选取
/// </summary>
public class SelectSystem : MonoBehaviour
{
    public static SelectSystem Ins;
    //public GameObject _text;
    public static List<string> VideoPath = new List<string>();
    private List<string> VideoPathShow = new List<string>();
    private int VideoIndex;
    void Awake()
    {
        LoadVideoPath();
    }
    //private void UpdateList()
    //{
    //    foreach (var item in VideoPathShow)
    //    {
    //        UpdateList(VideoPathShow[VideoIndex], VideoPath[VideoIndex]);
    //    }
    //}
    //public void UpdateList(string VideoName, string VideoPath)
    //{
    //    GameObject t = Instantiate(_text, _Content.transform);

    //    t.GetComponent<Text>().text = (VideoIndex + 1).ToString() + ":" + VideoName;
    //    t.GetComponent<VdieoNameButton>().VideoPath = VideoPath;
    //    t.GetComponent<VdieoNameButton>().VideoIndex = VideoIndex;
    //    t.transform.localScale = Vector3.one;
    //    //t.transform.SetParent(_Content.transform);
    //    VideoIndex++;
    //}
    public void LoadVideoPath()
    {
        DirectoryInfo info = new DirectoryInfo(Application.streamingAssetsPath + "");
        FileInfo[] fileInfo = info.GetFiles("*.mp4");
        FileInfo[] fileInfo1 = info.GetFiles("*.avi");
        foreach (var item in fileInfo)
        {
            VideoPath.Add(item.FullName);
            VideoPathShow.Add(item.Name);
        }
        foreach (var item in fileInfo1)
        {
            VideoPath.Add(item.FullName);
            VideoPathShow.Add(item.Name);
        }
        //UpdateList();
    }
}
public class VdieoNameButton : MonoBehaviour
{
    public string VideoPath;
    public int VideoIndex;
    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (!VideoManger._instance.GetPlayer().url.Equals(VideoPath))
            VideoManger._instance.VideoPlay(VideoPath, VideoIndex);

    }
}
