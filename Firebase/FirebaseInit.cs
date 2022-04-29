using Firebase;
using Firebase.Analytics;
using UnityEngine;
using Firebase.Auth;
/// <summary>
/// The class initialises Firebase and monitors Authorisation State Changes with a listener
/// Handle initialization of the necessary firebase modules:
/// </summary>
public class FirebaseInit : MonoBehaviour
{
    private FirebaseApp _app;
    private FirebaseAuth _auth;
    private FirebaseUser _user;

    /// <summary>
    /// calls the CheckAndFixFBDependencies method on start
    /// </summary>
    private void Start()
    {
        CheckAndFixFBDependencies();
    }

    /// <summary>
    /// Initilizes firebase
    /// </summary>
    void InitializeFirebase()
    {
        _auth = FirebaseAuth.DefaultInstance;
        _auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }
    /// <summary>
    /// Tracks state changes of the auth object.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (_auth.CurrentUser != _user)
        {
            bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null;
            if (!signedIn && _user != null)
            {
                Debug.Log("Signed out " + _user.UserId);
            }
            _user = _auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + _user.UserId);
            }
        }
    }
    /// <summary>
    /// resets values on destroy
    /// </summary>
    void OnDestroy()
    {
        _auth.StateChanged -= AuthStateChanged;
        _auth = null;
    }
    /// <summary>
    /// checks  firebase dependencies are present
    /// calls initialize firebase method
    /// </summary>
    private void CheckAndFixFBDependencies()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                _app = FirebaseApp.DefaultInstance;
                InitializeFirebase();
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