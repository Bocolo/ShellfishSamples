using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/*
 REVIEW THIS -ADD THE LOGIN CORROUTINES
https://www.youtube.com/watch?v=NsAUEyA2TRo
 
 */

public class FirebaseInit : MonoBehaviour
{
    private FirebaseApp app;
    private Firebase.Auth.FirebaseAuth auth;
    private Firebase.Auth.FirebaseUser user;
   //[SerializeField] private Menu menu;


    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Debug.Log("In Auth STATE CHANGED");
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }
  
    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }
    void Start()
    {

     
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = FirebaseApp.DefaultInstance;
                InitializeFirebase();
                // Set a flag here to indicate whether Firebase is ready to use by your app.
                Debug.Log("Got as far as here");
            }
            else
            {
                Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

   
}
//4.45 sec https://www.youtube.com/watch?v=b5h1bVGhuRk&t=276s
/*

public UnityEvent OnFirebaseLoaded = new UnityEvent();


public UnityEvent OnFirebaseFailed = new UnityEvent();


async void Start()
{
    //4.45 sec https://www.youtube.com/watch?v=b5h1bVGhuRk&t=276s


    var dependencyStatus = await FirebaseApp.CheckDependenciesAsync();
    if (dependencyStatus == dependencyStatus.Available)
    {
        OnFirebaseLoaded.Invoke();
    }
    else
    {
        OnFirebaseFailed.Invoke();
    }
}*/