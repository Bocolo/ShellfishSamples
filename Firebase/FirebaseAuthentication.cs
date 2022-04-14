using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UI.Popup;
using Data.Access;
using Save.Manager;

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
    public void Start()
    {
        _auth = FirebaseAuth.DefaultInstance;
        _userDAO = new UserDAO();
    }

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

    public async void LogIn()
    {
        await ValidateAuthenticationLogin(_email.text, _password.text);
        SuccessfulLogin(_isSuccessfulLogin);

    }
    private void SuccessfulLogin(bool isSuccesful)
    {
        if (isSuccesful)
        {
            _popUp.SuccessfulLogin();
            _userDAO.GetUser();
        }
        else
        {
            _popUp.UnSuccessfulLogin();
        }
    }
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
    private void SuccessfulSignUp(bool isSuccessful)
    {
        if (isSuccessful)
        {
            _popUp.SuccessfulSignUp();
            SaveNewUser();

        }
        else
        {
            _popUp.UnSuccessfulSignUp();
        }
    }

    private async Task ValidateAuthenticationLogin(string email, string password)
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
            /*      FirebaseUser user = task.Result;
                  Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                      user.DisplayName, user.UserId);*/
            _auth.CurrentUser.UpdateUserProfileAsync(new UserProfile
            {
                DisplayName = name,
            });
            _isSuccessfulSignUp = true;
            return;
        });
    }

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