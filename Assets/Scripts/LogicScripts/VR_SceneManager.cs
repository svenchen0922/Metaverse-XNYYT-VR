using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VR_SceneManager : UnitySingleton<VR_SceneManager>
{
    // Start is called before the first frame update
    public string SceneName_wai;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadScene(string SceneName)
    {
        SceneName_wai = SceneName;
        SceneManager.LoadScene("Loading");
       
    }
}
