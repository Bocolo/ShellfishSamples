using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
    private void Update()
    {
        if (Screen.orientation == ScreenOrientation.Landscape)
        {
            Debug.Log("---------- land");
            Screen.orientation = ScreenOrientation.Portrait;
            Debug.Log("---------- land");
        }
    }
}
