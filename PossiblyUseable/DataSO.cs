using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Data",fileName ="Data")]
public class DataSO : ScriptableObject
{
    public string name;
    [TextArea(5, 50)] public string content;
}
