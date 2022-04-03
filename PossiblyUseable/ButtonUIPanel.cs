using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


[System.Serializable]
public class DataSelectedEvent: UnityEvent<DataSO> {
}
public class ButtonUIPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LinkedText;
    public DataSelectedEvent OnDataSelect = new DataSelectedEvent();
    DataSO LinkedData;
    public void Bind(DataSO data)
    {
        LinkedData = data;
        LinkedText.text = data.name;
    }
   
    public void OnSelected()
    {
        OnDataSelect.Invoke(LinkedData);
    }
}
