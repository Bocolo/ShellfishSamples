using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredDateSingleton : MonoBehaviour
{
    public static StoredDateSingleton Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance!= this)
        {
            Destroy(this);
            
        }
    }
}
