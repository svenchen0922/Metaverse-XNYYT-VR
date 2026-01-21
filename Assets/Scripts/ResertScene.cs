using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResertScene : MonoBehaviour
{
    public GameObject resertBtn;
    public GameObject tip;
    public GameObject tips;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = resertBtn.GetComponent<Button>();
        btn.onClick.AddListener(this.resert);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void resert()
    {
        Debug.Log("≤‚ ‘1111111111111");
        this.tip.active = false;
        this.tips.active = true;
    }
}
