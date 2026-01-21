using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesScript : MonoBehaviour
{
    public GameObject yesBtn;
    public GameObject noBtn;
    public GameObject tip;
    public GameObject tips;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = yesBtn.GetComponent<Button>();
        btn.onClick.AddListener(this.yes);
        Button nobtn = noBtn.GetComponent<Button>();
        nobtn.onClick.AddListener(this.no);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void yes()
    {
        VR_SceneManager.Instance.LoadScene("VR_Office");
    }

    private void no()
    {
        this.tip.active = true;
        this.tips.active = false;
    }
}
