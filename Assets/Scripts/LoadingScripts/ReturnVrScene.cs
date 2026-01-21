using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReturnVrScene : MonoBehaviour
{
    private TeleportationAnchor anchor;
    void Start()
    {
        anchor = transform.GetComponent<TeleportationAnchor>();
        anchor.selectEntered.AddListener(GradSelect);
    }

	private void GradSelect(SelectEnterEventArgs arg0)
	{

        VR_SceneManager.Instance.LoadScene("VR_Office");

	}
}
