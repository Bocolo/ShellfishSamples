using App.Authentication;
using App.SaveSystem.Manager;
using App.Settings.Prefrences;
using App.UI;
using Firebase.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Users.Data;
using Users.Data.Access;
/// <summary>
/// This class managed the firebase authenticatioon : login, sign ups
/// and validation
/// </summary>
public class FirebaseAuthentication : MonoBehaviour
{
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_InputField _userNameInput;
    [SerializeField] private TMP_InputField _companyInput;
    [SerializeField] private PopUp _popUp;
    private UserDAO _userDAO;
    private FirebaseAuth _auth;
    private AuthenticationLogic _authenticationLogic;

    /// <summary>
    /// Start is called when the script is in the scene
    /// sets the _auth and _userDao
    /// </summary>
    public void Start()
    {
        _auth = FirebaseAuth.DefaultInstance;
        _userDAO = new UserDAO();
        _authenticationLogic = new AuthenticationLogic();
    }
    #region "Sign Up"
    /// <summary>
    /// Attempt to set up new authentication if the user name and company
    /// name fields are not empty
    /// otherwise set the _isSuccessfullSignUp bool to false
    /// </summary>
    public async void SignUp()
    {
        bool isSuccessful;
        if (!_userNameInput.text.Equals("") && !_companyInput.text.Equals(""))
        {
            await _authenticationLogic.SetUpAuthentication(_auth,_email.text, _password.text, _userNameInput.text);
            isSuccessful = _authenticationLogic.IsSuccessfulSignUp;
        }
        else
        {
            //here i can set those odd popups
            isSuccessful = false;
        }
        SuccessfulSignUp(isSuccessful);
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
    #endregion
    #region "Login"
    /// <summary>
    /// Calls the ValidateAuthenticationLogin with the email and password input
    /// calls the successfull login method with the _isSuccessfulLogin bool
    /// </summary>
    public async void LogIn()
    {
        await _authenticationLogic.ValidateAuthentication(_auth,_email.text, _password.text);
        SuccessfulLogin(_authenticationLogic.IsSuccessfulLogin);
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
    #endregion
    #region "Save User"
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
        _userDAO.AddUser(user);
        SaveData.Instance.SaveUserProfile(user, _auth.CurrentUser);
    }

    #endregion
}