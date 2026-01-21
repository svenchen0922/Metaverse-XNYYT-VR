using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoAudio : MonoBehaviour
{
    public GameObject Video_1;
    public GameObject Video_2;
    public GameObject Video_3;

    public GameObject Player;

    private List<GameObject> Video_list = new List<GameObject>();
    void Start()
    {
        Video_list.Add(Video_1);
        Video_list.Add(Video_2);
        Video_list.Add(Video_3);
    }

    // Update is called once per frame
    void Update()
    {

		foreach (GameObject item in Video_list)
		{
            
            float dis = (item.transform.position - Player.transform.position).sqrMagnitude;
            
			if (dis<=30)
			{
                item.GetComponent<VideoPlayer>().enabled = true;
			}
			else
			{
                item.GetComponent<VideoPlayer>().enabled = false;
            }

        }




      //  
    }
}
