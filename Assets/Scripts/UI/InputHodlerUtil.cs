using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHodlerUtil : MonoBehaviour
{

    private InputField inputField;

    private Text inputText;

    private Text placeHolderText;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<InputField>();
        inputText = inputField.textComponent.GetComponent<Text>();
        placeHolderText = inputField.placeholder.GetComponent<Text>();
       // inputText.color = new Color(0, 0, 0, 0);
    }

    private void Update()
    {
        inputField.text = placeHolderText.text;
    }
}
