using Firebase;
using Firebase.Analytics;
using UnityEngine;
/*
 REVIEW THIS -ADD THE LOGIN CORROUTINES
https://www.youtube.com/watch?v=NsAUEyA2TRo
 */
public class FirebaseInit : MonoBehaviour
{
    private FirebaseApp app;
    private Firebase.Auth.FirebaseAuth auth;
    private Firebase.Auth.FirebaseUser user;
    // Handle initialization of the necessary firebase modules:
    /// <summary>
    /// why have this twice --- is firebasestate changed class as well
    /// </summary>
    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }
    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Debug.Log("Auth changed firebase init");
        if (auth.CurrentUser != user)
        {
            user = auth.CurrentUser;
        }
    }
    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }
    void Start()
    {
        CheckAndFixFBDependencies();
    }
    private void CheckAndFixFBDependencies()
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