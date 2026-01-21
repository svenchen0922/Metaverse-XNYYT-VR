using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour
{
    public float timer = 5.0f; // 定时2秒
    public RawImage Image;
    private Texture[] TxtureArray;
    private int TextureIndex = 0;
    void Start()
    {
        TxtureArray = new Texture[] { Resources.Load("Video/太平有象0624") as Texture, Resources.Load("Video/星耀八方0624") as Texture, Resources.Load("Video/如意凤凰0624") as Texture };
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {

            doSomething();

            timer = 5.0f;
        }
    }


    private void doSomething()
    {
        Image.texture = TxtureArray[TextureIndex];
        TextureIndex = TextureIndex + 1;
		if (TextureIndex>=TxtureArray.Length)
		{
            TextureIndex = 0;
		}

    }
}
