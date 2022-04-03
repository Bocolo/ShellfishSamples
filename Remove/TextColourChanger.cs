using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextColourChanger : MonoBehaviour
{
    TextMeshProUGUI textProUI;
    bool isSet;
    void Start()
    {
        textProUI = gameObject.GetComponent<TextMeshProUGUI>();
        isSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSet && textProUI!=null)
        {
            Debug.Log("setting color");
            //Changing last char to red color - used for text with asterix
            textProUI.text = textProUI.text.Replace(textProUI.text[textProUI.text.Length-1].ToString(), "<color=#E01212>" + textProUI.text[textProUI.text.Length-1].ToString() + "</color>"); 
            isSet = true;
            Debug.Log("setting color complete" + isSet);
        }
    }
}
