using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ower : MonoBehaviour
{
    public GameObject Camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Camera.transform.position.x, transform.position.y, Camera.transform.position.z-0.2f );
     
        transform.localEulerAngles = new Vector3(0.0f, Camera.transform.localEulerAngles.y, 0.0f);
    
    }
}
