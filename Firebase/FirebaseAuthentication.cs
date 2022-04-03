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
        if (!_userNameInput.text.Equals("") && !_companyInput.text.Equals("")) {
              await SetUpAuthentication(email.text, password.text, _userNameInput.text, _companyInput.text);
            //pass user here
     //       await userDAO.AddUser(email.text, password.text, _userNameInput.text, _companyInput.text);
        }
        else
        {
            isSuccessfulSignUp = false;
        }
    //    if (userDAO.GetSignUpSuccess())
            if(isSuccessfulSignUp)
        {
            popUpObject.GetComponent<PopUp>().SuccessfulSignUp();

            UserData user = new UserData
            {
                Name = _userNameInput.text,
                Company = _companyInput.text,
                Email =email.text.ToLower(), //Setting to lower because when samples are sent to subcollection, the email is sending as lowercase and so data is being store is stoering in a seperate collection with lowercase - not mixed
                SubmittedSamplesCount =0,
            };
            SaveData.Instance.SaveUserProfile(user, auth.CurrentUser);
            Debug.Log(user.Name + "___CREATED_InFule__" + user.Company);
            
        }
        else
        {
            popUpObject.GetComponent<PopUp>().UnSuccessfulSignUp();
        }

        // await test(email.text, password.text, _userNameInput.text, _companyInput.text);
        Debug.Log("sign up finished");

    }
    public async void LogIn()
    {
        userDAO = new UserDAO();
        Debug.Log(" LOgin started");
        var firestore = FirebaseFirestore.DefaultInstance;

        await ValidateAuthenticationLogin(email.text, password.text);
        if (isSuccessfulLogin)
        {
            Debug.LogFormat("return found");
            popUpObject.GetComponent<PopUp>().SuccessfulLogin();//SetPopUpText("You Must Be Signed in to Access the Retrieval Page");
            //Debug.LogFormat("poporeprsdf");
            //UserData user = new UserData
            //{
            //    Name = _userNameInput.text,
            //    Company = _companyInput.text,
            //};
            //SaveData.Instance.SaveUserProfile(user);
            DocumentReference docRef = firestore.Collection("Users").Document(auth.CurrentUser.Email);
            Debug.Log(auth.CurrentUser.Email);
            userDAO.GetUser();
          //  ConvertDocSnapshotToUserData(docRef);




        }
        else
        {
            popUpObject.GetComponent<PopUp>().UnSuccessfulLogin();
        }
        Debug.Log(" lo9gin finished");
    }
    private void ConvertDocSnapshotToUserData(DocumentReference docRef)
    {
         docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Debug.Log(task.Result + " --- > " + task.Result.GetValue<int>("SubmittedSamplesCount"));
            try
            {
                UserData userData = task.Result.ConvertTo<UserData>();
                SaveData.Instance.SaveUserProfile(userData);
                Debug.Log(SaveData.Instance.LoadUserProfile().Email + "new usre data");
            }
            catch (Exception e)
            {
                Debug.Log(e + " __ error converting to userData");
            }

        });
    }
   private async Task  ValidateAuthenticationLogin(string email, string password) {
        auth = FirebaseAuth.DefaultInstance;//?

        await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
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

                //pop up invalid login
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
       
            isSuccessfulLogin = true;
        });
       
    }

   private async Task test(string email, string password, string name, string company)
    {
        Debug.Log("Testing 3");

        auth = FirebaseAuth.DefaultInstance;//?
        Debug.Log("Testing 4");

       await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            Debug.Log("Testing 5");

            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                isSuccessfulSignUp = false;

                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception.Message);
                isSuccessfulSignUp = false;

                return;
            }

            // Firebase user has been created.
            //Firebase.Auth.FirebaseUser newUser = task.Result;
            //Debug.LogFormat("Firebase user created successfully: {0} ({1})",
            //    newUser.DisplayName, newUser.UserId);

            FirebaseUser user = task.Result;
            //user. = "__";

            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                user.DisplayName, user.UserId);

            auth.CurrentUser.UpdateUserProfileAsync(new UserProfile
            {
                DisplayName = name,
            });
            isSuccessfulSignUp = true;

        });
        ///
    }
    private async Task SetUpAuthentication(string email, string password, string name, string company)
    {

        Debug.Log("Testing 3");

        auth = FirebaseAuth.DefaultInstance;//?
        Debug.Log("Testing 4");

        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            Debug.Log("Testing 5");

            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                isSuccessfulSignUp = false;

                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception.Message);
                isSuccessfulSignUp = false;

                return;
            }

            // Firebase user has been created.
            //Firebase.Auth.FirebaseUser newUser = task.Result;
            //Debug.LogFormat("Firebase user created successfully: {0} ({1})",
            //    newUser.DisplayName, newUser.UserId);

            FirebaseUser user = task.Result;
            //user. = "__";

            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                user.DisplayName, user.UserId);

            auth.CurrentUser.UpdateUserProfileAsync(new UserProfile
            {
                DisplayName = name,
            });
            isSuccessfulSignUp = true;
            return;
        });
        ///
        ///Wont wotk gere
        ///
      //sign up logs in


    }
  
    //private bool LogTaskCompletion(Task task, string v)
    //{
    //    throw new NotImplementedException();
    //}
}
//void InitializeFirebase()
//{
//    auth = FirebaseAuth.DefaultInstance;
//    auth.StateChanged += AuthStateChanged;
//    AuthStateChanged(this, null);
//}

//void AuthStateChanged(object sender, System.EventArgs eventArgs)
//{
//    if (auth.CurrentUser != user)
//    {
//        bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
//        if (!signedIn && user != null)
//        {
//            Debug.Log("Signed out " + user.UserId);
//        }
//        user = auth.CurrentUser;
//        if (signedIn)
//        {
//            Debug.Log("Signed in " + user.UserId);
//           // displayName = user.DisplayName ?? "";
//            email.text = user.Email ?? "";
//          //  photoUrl = user.PhotoUrl ?? "";
//        }
//    }
//}
//public void SignUpForm()
//{
//    _companyInput.gameObject.SetActive(true);
//    _userNameInput.gameObject.SetActive(true);

//}
//public void LoginForm()
//{
//    _companyInput.gameObject.SetActive(false);
//    _userNameInput.gameObject.SetActive(false);
//}