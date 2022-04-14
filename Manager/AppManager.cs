using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
public class AppManager 
{
#if UNITY_INCLUDE_TESTS
    public bool IsLoggedInDB()
    {
        return FirebaseAuth.DefaultInstance.CurrentUser != null;
    }
#endif
}
