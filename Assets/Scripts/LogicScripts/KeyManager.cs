using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using VRKeyboard.Utils;

public class KeyManager : UnitySingleton<KeyManager>
{
    public GameObject Keyboard;

    public GameObject LginTxt_1;
	public GameObject LginTxt_2;
    public GameObject LginTxt_3;

    public GameObject ReginTxt_1;
    public GameObject ReginTxt_2;
    public GameObject ReginTxt_3;
    public GameObject ReginTxt_4;
    public GameObject ReginTxt_5;

    private int Login_index =0;
    private int Regin_index = 0;

    private GameObject[] logintxt_arr =new GameObject[3];
    private GameObject[] regin_arr = new GameObject[5];

    public GameObject Keys;

    void Start()
    {
        logintxt_arr[0] = LginTxt_1;
        logintxt_arr[1] = LginTxt_2;
        logintxt_arr[2] = LginTxt_3;


        regin_arr[0] = ReginTxt_1;
        regin_arr[1] = ReginTxt_2;
        regin_arr[2] = ReginTxt_3;
        regin_arr[3] = ReginTxt_4;
        regin_arr[4] = ReginTxt_5;



     
		foreach (Transform row in Keys.transform)
		{


			foreach (Transform key in row)
			{
                //key.GetComponent<XRGrabInteractable>().hoverEntered.AddListener(SuiPianHoverIN);
                //key.GetComponent<XRGrabInteractable>().hoverExited.AddListener(SuiPianHoverOUT);
            }
           
        }
	}


    private void SuiPianHoverIN(HoverEnterEventArgs a)
    {


		if (a.interactable.name == "Back")
		{
            a.interactable.GetComponent<Image>().color = new Color(a.interactable.GetComponent<Image>().color.r, a.interactable.GetComponent<Image>().color.g, a.interactable.GetComponent<Image>().color.b, a.interactable.GetComponent<Image>().color.a/2);
        }
		else if (a.interactable.name == "Clear")
		{
            a.interactable.GetComponent<Image>().color = new Color(a.interactable.GetComponent<Image>().color.r, a.interactable.GetComponent<Image>().color.g, a.interactable.GetComponent<Image>().color.b, a.interactable.GetComponent<Image>().color.a / 2);

        }
        else
		{
            a.interactable.GetComponent<Image>().color = new Color(0.46f, 0.49f, 0.50f);
        }


     


    }
    private void SuiPianHoverOUT(HoverExitEventArgs a)
    {
		if (a.interactable.name == "Back")
		{
            a.interactable.GetComponent<Image>().color = new Color(a.interactable.GetComponent<Image>().color.r, a.interactable.GetComponent<Image>().color.g, a.interactable.GetComponent<Image>().color.b, a.interactable.GetComponent<Image>().color.a * 2);

		}
        else if (a.interactable.name == "Clear")
        {
            a.interactable.GetComponent<Image>().color = new Color(a.interactable.GetComponent<Image>().color.r, a.interactable.GetComponent<Image>().color.g, a.interactable.GetComponent<Image>().color.b, a.interactable.GetComponent<Image>().color.a * 2);

        }
        else
		{
            a.interactable.GetComponent<Image>().color = new Color(0.188f, 0.189f, 0.215f);
        }


      

    }



    public void OnEditEnd()
    {
		if (GlobalData.Instance.IsLogin)
        {
           
            Keyboard.GetComponent<KeyboardManager>().inputText = logintxt_arr[Login_index].GetComponent<Text>();
            Login_index = Login_index + 1;
            print("换行");
			if (Login_index>=3)
			{
                Login_index = 0;
			}

        }
		else
		{
           
            Keyboard.GetComponent<KeyboardManager>().inputText = regin_arr[Regin_index].GetComponent<Text>();
            Regin_index = Regin_index + 1;
            if (Regin_index >= 5)
            {
                Regin_index = 0;
            }
            print("换行");
        }
    }


	private void Update()
	{

		if (SceneManager.GetActiveScene().name == "LoginScene")
		{
            //提示当前输入框
            for (int i = 0; i < logintxt_arr.Length; i++)
            {
                if (Keyboard.GetComponent<KeyboardManager>().inputText.gameObject == logintxt_arr[i])
                {
                    logintxt_arr[i].transform.parent.GetComponent<Image>().color = new Color(0.48f, 0.87f, 1f);
                }
                else
                {
                    logintxt_arr[i].transform.parent.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                }
            }

            //注册框
            //提示当前输入框
            for (int i = 0; i < regin_arr.Length; i++)
            {
                if (Keyboard.GetComponent<KeyboardManager>().inputText.gameObject == regin_arr[i])
                {
                    regin_arr[i].transform.parent.GetComponent<Image>().color = new Color(0.48f, 0.87f, 1f);
                }
                else
                {
                    regin_arr[i].transform.parent.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                }
            }

        }




		
	}
}
