using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SuiDao;
    void Start()
    {
        SuiDao.transform.DOMove(new Vector3(0, -15, 0), 3f).OnComplete(() => {
            SceneManager.LoadScene(VR_SceneManager.Instance.SceneName_wai);
        });
    }


}
