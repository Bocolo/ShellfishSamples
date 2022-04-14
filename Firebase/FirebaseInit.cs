using Firebase;
using Firebase.Analytics;
using UnityEngine;
/*
 REVIEW THIS -ADD THE LOGIN CORROUTINES
https://www.youtube.com/watch?v=NsAUEyA2TRo
 */
public class FirebaseInit : MonoBehaviour
{
    private FirebaseApp _app;
    private Firebase.Auth.FirebaseAuth _auth;
    private Firebase.Auth.FirebaseUser _user;
    // Handle initialization of the necessary firebase modules:
    /// <summary>
    /// why have this twice --- is firebasestate changed class as well
    /// </summary>
    void InitializeFirebase()
    {
        _auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        _auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }
    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (_auth.CurrentUser != _user)
        {
            _user = _auth.CurrentUser;
        }
    }
    void OnDestroy()
    {
        _auth.StateChanged -= AuthStateChanged;
        _auth = null;
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
                _app = FirebaseApp.DefaultInstance;
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