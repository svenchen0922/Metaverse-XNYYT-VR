using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Video;

public class KEY_Game : MonoBehaviour
{
	public GameObject SuiPian_1;
	public GameObject SuiPian_2;

	public GameObject NPC;


	private XRGrabInteractable grab_1;
	private XRGrabInteractable grab_2;
	public XRGrabInteractable Truth_grab_1;
	public XRGrabInteractable Truth_grab_2;
	public XRGrabInteractable Truth_grab_3;
	private TeleportationAnchor anchor;


	public GameObject Question_1;
	public GameObject Question_2;
	public GameObject Question_3;

	public GameObject teleport;
	public GameObject RightHand;
	public GameObject Yellow_Sphere;
	public GameObject Blue_Sphere;
	public GameObject VideoAudio;

	
	public GameObject Number;

	private Boolean flag = true;
	void Start()
	{
		anchor = teleport.GetComponent<TeleportationAnchor>();
		grab_1 = SuiPian_1.GetComponent<XRGrabInteractable>();
		grab_2 = SuiPian_2.GetComponent<XRGrabInteractable>();
	


		anchor.selectEntered.AddListener(GradSelect);


		grab_1.selectEntered.AddListener(GrabHandler);
		grab_2.selectEntered.AddListener(GrabHandler);

		Truth_grab_1.selectEntered.AddListener(Truth);
		Truth_grab_2.selectEntered.AddListener(Truth);
		Truth_grab_3.selectEntered.AddListener(Truth);

		grab_1.hoverEntered.AddListener(SuiPianHoverIN);
		grab_1.hoverExited.AddListener(SuiPianHoverOUT);
		grab_2.hoverEntered.AddListener(SuiPianHoverIN);
		grab_2.hoverExited.AddListener(SuiPianHoverOUT);


		NPC.transform.Find("yaoshi").GetComponent<XRGrabInteractable>().selectEntered.AddListener((SelectEnterEventArgs a) =>
			{
				VR_SceneManager.Instance.LoadScene("ShaLong");
			});


		GlobalData.Instance.KeyNumber = 3;

		//根据全局数据重置Vr_Office场景 布局
		if (GlobalData.Instance.KeyNumber<=0)
		{
			teleport.SetActive(false);
			StartCoroutine(Key_Active());
		}

		
	}




	private void GradSelect(SelectEnterEventArgs arg0)
	{

		if (GlobalData.Instance.KeyNumber == 3)
		{
			print(arg0.interactable.name);
			SuiPian_1.SetActive(true);
			SuiPian_2.SetActive(true);
			VideoAudio.GetComponent<VideoPlayer>().audioOutputMode = VideoAudioOutputMode.None;
			VideoAudio.SetActive(false);
			VideoAudio.SetActive(true);

			NPC.GetComponent<Animation>().Play("jiqiren");
			NPC.transform.Find("Dialog").gameObject.SetActive(true);
			NPC.transform.Find("Dialog").Find("number").GetComponent<TextMesh>().text = GlobalData.Instance.KeyNumber.ToString();
			Number.GetComponent<TextMesh>().text = (GlobalData.Instance.KeyNumber - 1).ToString();
			teleport.SetActive(false);
		}
		else if (GlobalData.Instance.KeyNumber==1)
		{
			//��������߼�

			if(this.flag == true)
            {
				if (((DateTime.Now.ToUniversalTime().Ticks)) % 3 == 0)
				{
					Question_1.SetActive(true);
				}
				else if ((((DateTime.Now.ToUniversalTime().Ticks)) % 3 == 1))
				{
					Question_2.SetActive(true);
				}
				else
				{
					Question_3.SetActive(true);
				}
				this.flag = false;
				NPC.transform.Find("Dialog").gameObject.SetActive(false);

			}
		}
		else if (GlobalData.Instance.KeyNumber==0)
		{
			
			//������Ƭ�ռ���Ͻ���ɳ������ 
		}



	}

	private void GrabHandler(SelectEnterEventArgs a)
	{
		print(a.interactable.name);
		a.interactable.GetComponent<Collider>().enabled = false;
		GlobalData.Instance.KeyNumber = GlobalData.Instance.KeyNumber - 1;
		NPC.transform.Find("Dialog").Find("number").GetComponent<TextMesh>().text = GlobalData.Instance.KeyNumber.ToString();
		Number.GetComponent<TextMesh>().text = (GlobalData.Instance.KeyNumber - 1).ToString();

		//a.interactable.transform.DOLocalMove(Number.transform.position, 3f).OnComplete(() =>
		//{
		//	a.interactable.gameObject.SetActive(false);
		//});

		StartCoroutine(load(a.interactable.name));

		if (GlobalData.Instance.KeyNumber ==1)
		{
			teleport.SetActive(true);
		}
		
	}
	IEnumerator load(string G)
	{
		yield return new WaitForSeconds(1);    //注意等待时间的写法
		GameObject.Find(G).SetActive(false);
	}


	private void Truth(SelectEnterEventArgs a)
	{
		Question_1.SetActive(false);
		Question_2.SetActive(false);
		Question_3.SetActive(false);
		GlobalData.Instance.KeyNumber = GlobalData.Instance.KeyNumber - 1;
		Number.GetComponent<TextMesh>().text = (GlobalData.Instance.KeyNumber - 1).ToString();
		StartCoroutine(Key_Active());
	}

	IEnumerator Key_Active()
	{
		Question_1.SetActive(false);
		Question_2.SetActive(false);
		Question_3.SetActive(false);
		NPC.transform.Find("hecheng1-1").gameObject.SetActive(true);
		NPC.transform.Find("Loading").gameObject.SetActive(true);
		yield return new WaitForSeconds(2.5f);
		NPC.transform.Find("yaoshi").gameObject.SetActive(true);

	
		
	}

	private void SuiPianHoverIN(HoverEnterEventArgs a)
	{
		RightHand.GetComponent<XRInteractorLineVisual>().reticle = Blue_Sphere;
		Yellow_Sphere.SetActive(false);
		Blue_Sphere.SetActive(true);
	

	}
	private void SuiPianHoverOUT(HoverExitEventArgs a)
	{
		RightHand.GetComponent<XRInteractorLineVisual>().reticle = Yellow_Sphere;
		Yellow_Sphere.SetActive(true);
		Blue_Sphere.SetActive(false);
	
	}
}
