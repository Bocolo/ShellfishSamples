using UnityEngine;
using Firebase.Auth;
using TMPro;
using Firebase.Extensions;
using System.Threading.Tasks;
using Firebase.Firestore;
using System;
public class FirebaseAuthentication : MonoBehaviour
{
    protected FirebaseAuth auth;
    protected FirebaseUser user;
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField _userNameInput;
    [SerializeField] private TMP_InputField _companyInput;
    private UserDAO userDAO;
    [SerializeField] private GameObject popUpObject;
    private bool isSuccessfulLogin = false;
    private bool isSuccessfulSignUp = false;
    public async void SignUp()
    {
        userDAO = new UserDAO();
        Debug.Log("singing up stated");
        if (!_userNameInput.text.Equals("") && !_companyInput.text.Equals(""))
        {
            await SetUpAuthentication(email.text, password.text, _userNameInput.text, _companyInput.text);
            //pass user here
            //       await userDAO.AddUser(email.text, password.text, _userNameInput.text, _companyInput.text);
        }
        else
        {
            isSuccessfulSignUp = false;
        }
        if (isSuccessfulSignUp)
        {
            popUpObject.GetComponent<PopUp>().SuccessfulSignUp();
            User user = new User
            {
                Name = _userNameInput.text,
                Company = _companyInput.text,
                Email = email.text.ToLower(), 
                SubmittedSamplesCount = 0,
            };
            SaveData.Instance.SaveUserProfile(user, auth.CurrentUser);
        }
        else
        {
            popUpObject.GetComponent<PopUp>().UnSuccessfulSignUp();
        }
        Debug.Log("sign up finished");
    }
    public async void LogIn()
    {
        userDAO = new UserDAO();
        var firestore = FirebaseFirestore.DefaultInstance;
        await ValidateAuthenticationLogin(email.text, password.text);
        if (isSuccessfulLogin)
        {
            popUpObject.GetComponent<PopUp>().SuccessfulLogin();
            //why is this here
            DocumentReference docRef = firestore.Collection("Users").Document(auth.CurrentUser.Email);
            Debug.Log(auth.CurrentUser.Email);
            userDAO.GetUser();
        }
        else
        {
            popUpObject.GetComponent<PopUp>().UnSuccessfulLogin();
        }
    }
    private void ConvertDocSnapshotToUserData(DocumentReference docRef)
    {
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
       {
           Debug.Log(task.Result + " --- > " + task.Result.GetValue<int>("SubmittedSamplesCount"));
           try
           {
               User user = task.Result.ConvertTo<User>();
               SaveData.Instance.SaveUserProfile(user);
               Debug.Log(SaveData.Instance.LoadUserProfile().Email + "new usre data");
           }
           catch (Exception e)
           {
               Debug.Log(e + " __ error converting to user");
           }
       });
    }
    private async Task ValidateAuthenticationLogin(string email, string password)
    {
        auth = FirebaseAuth.DefaultInstance;//?
        await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                isSuccessfulLogin = false;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception.Message);
                isSuccessfulLogin = false;
                return;
            }
            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            isSuccessfulLogin = true;
        });
    }
    private async Task SetUpAuthentication(string email, string password, string name, string company)
    {
        auth = FirebaseAuth.DefaultInstance;//?
        Debug.Log("RAWR"+auth);
        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            Debug.Log("RAWR");
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
            FirebaseUser user = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                user.DisplayName, user.UserId);
            auth.CurrentUser.UpdateUserProfileAsync(new UserProfile
            {
                DisplayName = name,
            });
            isSuccessfulSignUp = true;
            return;
        });
    }
#if UNITY_INCLUDE_TESTS
    public async Task AuthenticationTest(string email, string password, string name, string company)
    {
         await SetUpAuthentication( email,  password,  name,  company);
    }
#endif
}