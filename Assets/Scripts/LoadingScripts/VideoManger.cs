using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
/// <summary>
/// 导览助手视频功能 
/// </summary>
public class VideoManger : MonoBehaviour
{
    [SerializeField]
    VideoPlayer videoPlan;   //video播放元件
    private Button _Togo = null;
    private Button _GoBack = null;
    private Button _StopAndContine = null;
    private Slider _Slider = null;
    bool _isStop;
    //Image _stopImage;        //视频界面显示暂停按钮
    //Sprite[] textures;       //多个图片
    //GameObject _anima;       //视频多个按钮显示
    //[SerializeField]
    //GameObject _VideoPathBase;
    private string _CruPath;
    private bool IsShow = false;
    private float WaitTime = 5F;
    private float CRUTime;
    private Animation _Animation;
    public static VideoManger _instance = null;
    private int _index;
    /// <summary>
    /// 单例
    /// </summary>
    public void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            return;
    }
    public void OnEnable()
    {
        videoPlan.Play();
    }
    /// <summary>
    /// 界面的弹出
    /// </summary>
    private void FixedUpdate()
    {
        if (IsShow)
        {
            if (videoPlan.isPlaying)
            {
                //jindutaio.value = ((float)videoPlan.frame / videoPlan.frameCount);
                CRUTime += Time.deltaTime;  //时间计时器
                if (CRUTime > WaitTime)
                {
                    CRUTime = 0;
                    IsShow = false;
                    //_anima.SetActive(false);
                }
            }
        }
    }
    /// <summary>
    /// 按钮显示
    /// </summary>
    public void AnimatorPlay()
    {
        CRUTime = 0;
        if (!IsShow)
        {
            _Animation.Play();
            //_anima.SetActive(true);
            IsShow = true;
        }
    }
    private void VideoPlan_LoopPninter(VideoPlayer source)
    {
        NextMovie();
    }
    private void NextMovie()
    {
        CRUTime = 0;
        for (int i = 0; i < SelectSystem.VideoPath.Count; i++)
        {
            if (_CruPath.Equals(SelectSystem.VideoPath[i]))
            {
                if (i + 1 >= SelectSystem.VideoPath.Count)
                {
                    VideoPlay(SelectSystem.VideoPath[0], 0);
                }
                else
                    VideoPlay(SelectSystem.VideoPath[i + 1], i + 1);
                break;
            }
        }
    }
    private void LastMove()
    {
        CRUTime = 0;
        for (int i = 0; i < SelectSystem.VideoPath.Count; i++)
        {
            if (_CruPath.Equals(SelectSystem.VideoPath[i]))
            {
                if (i - 1 < 0)
                {
                    VideoPlay(SelectSystem.VideoPath[SelectSystem.VideoPath.Count - 1], SelectSystem.VideoPath.Count - 1);
                }
                else
                    VideoPlay(SelectSystem.VideoPath[i - 1], i - 1);
                break;
            }
        }
    }
    public void UpdateCon()
    {
        _isStop = true;
        //_stopImage.sprite = textures[0];
    }
    public void VideoPlay(string Path, int index)
    {
        UpdateCon();
        //_VideoPathBase.transform.GetChild(_index).GetComponent<Text>().color = new Color(1, 216f / 255f, 173f / 255f, 1);
        //_VideoPathBase.transform.GetChild(index).GetComponent<Text>().color = new Color(1, 1, 1, 1);

        _CruPath = Path;
        videoPlan.url = Path;
        videoPlan.Play();
        _index = index;
    }
    public VideoPlayer GetPlayer()
    {
        return videoPlan;
    }
    public void StopAndContinue()
    {
        CRUTime = 0;
        _isStop = !_isStop;
        if (_isStop)
        {
            //_stopImage.sprite = textures[0];
            videoPlan.Play();
        }
        else
        {
            //_stopImage.sprite = textures[1];
            videoPlan.Pause();
        }
    }
}

