using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CalanderDate : MonoBehaviour
{

    [SerializeField] private Dropdown DayDrop;
    [SerializeField] private Dropdown MonthDrop;
    [SerializeField] private Dropdown YearDrop;
   // [SerializeField] private Button submit;

    private string date;
    private string day;
    private string month;
    private string year;
    private void Start()
    {
   /*     submit.onClick.AddListener(() =>
        {
            CheckDate();
        });*/
        
    }
    public void CheckDate()
    {
        day = DayDrop.options[DayDrop.value].text;
        month = MonthDrop.options[MonthDrop.value].text;
        year = YearDrop.options[YearDrop.value].text;
        date = year + "-" + month + "-" + day;
        try
        {
            var datetime = DateTime.Parse(date);
            Debug.Log(datetime);
        }catch(Exception e)
        {
            Debug.Log(e);
            Debug.Log("Date test failed");
        }
       
    }

    


}
