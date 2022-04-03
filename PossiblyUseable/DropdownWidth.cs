using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropdownWidth : MonoBehaviour
{
    Dropdown dropdown;
   

    void Start()
    {
        //access the dropdown UI component from the gameObject
        dropdown = GetComponent<Dropdown>();
        int counter = 0;
        foreach(var option in dropdown.options)
        {
            counter++;
            Debug.Log(option.ToString() + "     ___ "+counter);

        }
    }


}
