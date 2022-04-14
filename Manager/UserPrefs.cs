using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPrefs
{
    public static void SetPreferredCanvas(string value)
    {
        PlayerPrefs.SetString("Submit Canvas", value);
    }
  
    public static string GetPreferredCanvas()
    {
        return PlayerPrefs.GetString("Submit Canvas");
    }
}
