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
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField _userNameInput;
    [SerializeField] private TMP_InputField _companyInput;
 
    [SerializeField] private GameObject popUpObject;
    private UserDAO userDAO;
    protected FirebaseAuth auth;

    private bool isSuccessfulLogin = false;
    private bool isSuccessfulSignUp = false;
    public void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        userDAO = new UserDAO();
    }
    public async void SignUp()
    {
        if (!_userNameInput.text.Equals("") && !_companyInput.text.Equals(""))
        {
            await SetUpAuthentication(email.text, password.text, _userNameInput.text);
        }
        else
        {
            isSuccessfulSignUp = false;
        }
        SuccessfulSignUp(isSuccessfulSignUp);
    }

    public async void LogIn()
    {
        await ValidateAuthenticationLogin(email.text, password.text);
        SuccessfulLogin(isSuccessfulLogin);
   
    }
    private void SuccessfulLogin(bool isSuccesful)
    {
        if (isSuccesful)
        {
            popUpObject.GetComponent<PopUp>().SuccessfulLogin();
            userDAO.GetUser();
        }
        else
        {
            popUpObject.GetComponent<PopUp>().UnSuccessfulLogin();
        }
    }
    private void SaveNewUser()
    {
        User user = new User
        {
            Name = _userNameInput.text,
            Company = _companyInput.text,
            Email = email.text.ToLower(),
            SubmittedSamplesCount = 0,
        };
        ///review use od this block
        userDAO = new UserDAO();
        userDAO.AddUser(user);
        ///////////////////////
        SaveData.Instance.SaveUserProfile(user, auth.CurrentUser);
    }
    private void SuccessfulSignUp(bool isSuccessful)
    {
        if (isSuccessful)
        {
            popUpObject.GetComponent<PopUp>().SuccessfulSignUp();
            SaveNewUser();
    
        }
        else
        {
            popUpObject.GetComponent<PopUp>().UnSuccessfulSignUp();
        }
    }

    private async Task ValidateAuthenticationLogin(string email, string password)
    {
        await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            { 
                isSuccessfulLogin = false;
                return;
            }
            if (task.IsFaulted)
            {
                isSuccessfulLogin = false;
                return;
            }
         
            isSuccessfulLogin = true;
        });
    }
    private async Task SetUpAuthentication(string email, string password, string name)
    {
       // auth = FirebaseAuth.DefaultInstance;//?
        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                isSuccessfulSignUp = false;
                return;
            }
            if (task.IsFaulted)
            {
                isSuccessfulSignUp = false;
                return;
            }
      /*      FirebaseUser user = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                user.DisplayName, user.UserId);*/
            auth.CurrentUser.UpdateUserProfileAsync(new UserProfile
            {
                DisplayName = name,
            });
            isSuccessfulSignUp = true;
            return;
        });
    }
/*    private void UpdateFirebaseProfile()
    {

    }*/

#if UNITY_INCLUDE_TESTS
    public async Task AuthenticationTest(string email, string password, string name)
    {
         await SetUpAuthentication( email,  password,  name);
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