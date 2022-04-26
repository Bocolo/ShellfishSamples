using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UI.Popup;
using Data.Access;
using Save.Manager;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// This class managed the firebase authenticatioon : login, sign ups
/// and validation
/// </summary>
public class FirebaseAuthentication : MonoBehaviour
{
    // protected FirebaseUser user;
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_InputField _userNameInput;
    [SerializeField] private TMP_InputField _companyInput;
    [SerializeField] private PopUp _popUp;
    private UserDAO _userDAO;
    private FirebaseAuth _auth;
    private bool _isSuccessfulLogin = false;
    private bool _isSuccessfulSignUp = false;
    /// <summary>
    /// Start is called when the script is in the scene
    /// sets the _auth and _userDao
    /// </summary>
    public void Start()
    {
        _auth = FirebaseAuth.DefaultInstance;
        _userDAO = new UserDAO();
    }
    /// <summary>
    /// Attempt to set up new authentication if the user name and company
    /// name fields are not empty
    /// otherwise set the _isSuccessfullSignUp bool to false
    /// </summary>
    public async void SignUp()
    {
        if (!_userNameInput.text.Equals("") && !_companyInput.text.Equals(""))
        {
            await SetUpAuthentication(_email.text, _password.text, _userNameInput.text);
        }
        else
        {
            _isSuccessfulSignUp = false;
        }
        SuccessfulSignUp(_isSuccessfulSignUp);
    }
    /// <summary>
    /// Calls the ValidateAuthenticationLogin with the email and password input
    /// calls the successfull login method with the _isSuccessfulLogin bool
    /// </summary>
    public async void LogIn()
    {
        await ValidateAuthentication(_email.text, _password.text);
        SuccessfulLogin(_isSuccessfulLogin);
    }
    /// <summary>
    /// Manages behaviour dependent on the success of a login attempt
    /// calls the pop up unsuccessfull login if the passed bool is false
    /// calls the pop up successfull login if the passed bool is true
    /// calls the userDao getUser if the passed bool is true
    /// 
    /// </summary>
    /// <param name="isSuccessful">bool representing if a login has been successful</param>
    private void SuccessfulLogin(bool isSuccessful)
    {
        if (isSuccessful)
        {
            _userDAO.GetUser();
            UserPrefs.SetLoginSuccessful("yes");
            UserPrefs.SetLoginComplete("no");
            SceneManager.LoadScene(0);

        }
        else
        {
            UserPrefs.SetLoginSuccessful("no");
            UserPrefs.SetLoginComplete("yes");
            _popUp.UnSuccessfulLogin();
        }
    }

    /// <summary>
    /// Creates a new user based on the input texts and, using the userDao
    /// add the user to the firestore collection
    /// saves the new users profile
    /// </summary>
    private void SaveNewUser()
    {
        User user = new User
        {
            Name = _userNameInput.text,
            Company = _companyInput.text,
            Email = _email.text.ToLower(),
            SubmittedSamplesCount = 0,
        };
        ///review use od this block
        //////check if unblocking this affected anything
       // _userDAO = new UserDAO();
        _userDAO.AddUser(user);
        ///////////////////////
        SaveData.Instance.SaveUserProfile(user, _auth.CurrentUser);
    }
    /// <summary>
    /// Manages behaviour dependent on the success of a sign up attempt
    /// calls the pop up UnSuccessfulSignUp if the passed bool is false
    /// calls the pop up SuccessfulSignUp if the passed bool is true
    /// calls SaveNewUser if the passed bool is true
    /// 
    /// </summary>
    /// <param name="isSuccessful">bool representing if a login has been successful</param>
    private void SuccessfulSignUp(bool isSuccessful)
    {
        if (isSuccessful)
        {
            SaveNewUser();
            UserPrefs.SetSignUpSuccessful("yes");
            UserPrefs.SetSignUpComplete("no");
            SceneManager.LoadScene(0);
        }
        else
        {
            UserPrefs.SetSignUpSuccessful("no");
            UserPrefs.SetSignUpComplete("yes");
            _popUp.UnSuccessfulSignUp();
        }
    }
    /// <summary>
    /// Attempts to validate a firebase user login using the email and password params
    /// sets the _isSuccessfulLogin bool based on the sign attempts success
    /// </summary>
    /// <param name="email">represents the login email</param>
    /// <param name="password">representd the login password</param>
    /// <returns></returns>
    private async Task ValidateAuthentication(string email, string password)
    {
        await _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                _isSuccessfulLogin = false;
                return;
            }
            if (task.IsFaulted)
            {
                _isSuccessfulLogin = false;
                return;
            }
            _isSuccessfulLogin = true;
        });
    }
    /// <summary>
    /// Attempts to set up a firebase user auth account using the email, password and name params
    /// sets the _isSuccessfulSignUp bool based on the sign up attempts success
    /// sets the new account display name if successful
    /// </summary>
    /// <param name="email">represents the login email</param>
    /// <param name="password">representd the login password</param>
    /// <param name="name"></param>
    /// <returns></returns>
    private async Task SetUpAuthentication(string email, string password, string name)
    {
        await _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                _isSuccessfulSignUp = false;
                return;
            }
            if (task.IsFaulted)
            {
                _isSuccessfulSignUp = false;
                return;
            }
            _auth.CurrentUser.UpdateUserProfileAsync(new UserProfile
            {
                DisplayName = name,
            });
            Debug.Log("Sign Up Succeeded. Current User: " + _auth.CurrentUser.DisplayName);
            _isSuccessfulSignUp = true;
            return;
        });
    }
    /*      FirebaseUser user = task.Result;
      Debug.LogFormat("Firebase user created successfully: {0} ({1})",
          user.DisplayName, user.UserId);*/
#if UNITY_INCLUDE_TESTS
    public async Task AuthenticationTest(string email, string password, string name)
    {
        await SetUpAuthentication(email, password, name);
    }
#endif
}
/*private void ConvertDocSnapshotToUserData(DocumentReference docRef)
{
    docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
    {
        try
        {
            User user = task.Result.ConvertTo<User>();
            SaveData.Instance.SaveUserProfile(user);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    });
}*/