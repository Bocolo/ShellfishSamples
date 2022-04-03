using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MessageEntryUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI msgText;
     public void Bind(string msg)
        {
            Debug.Log("in bind msg)");
            msgText.text = msg;
            Debug.Log("in bind msg, set tect");
        var dimensions =msgText.GetPreferredValues();
        Debug.Log("in bind msg, set dimenstions");
        ((RectTransform)msgText.transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimensions.y);
        Debug.Log("in bind msg, set trandsform");
    }
}
