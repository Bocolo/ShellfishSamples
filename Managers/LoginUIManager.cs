using Firebase.Auth;
using UI.Popup;
using UnityEngine;
using Login.Logic;
using UnityEngine.UI;
namespace UI.Authentication
{
    /// <summary>
    /// Manages Log in and Sign out buttons
    /// </summary>
    public class LoginUIManager : MonoBehaviour
    {
        private bool _isLoggedIn; //{  private get;  set; }
        [SerializeField] private Button _logInButton;
        [SerializeField] private Button _signoutButton;
        [SerializeField] private PopUp _popUp;
        private LoginLogic loginLogic;
        /// <summary>
        /// sets the loggedIn bool based on whether a firestore user is logegd in
        /// calls SetLogInOutButtonInteractable passing the isLoggedIn bool
        /// </summary>
        private void Start()
        {
            loginLogic = new LoginLogic();
            _isLoggedIn = FirebaseAuth.DefaultInstance.CurrentUser != null;
            loginLogic.SetButtonInteractable(_isLoggedIn,_logInButton,_signoutButton);
            if ((UserPrefs.GetSignUpSuccessful().Equals("yes")) && (UserPrefs.GetLoginComplete().Equals("no")))
            {
                loginLogic.SuccessfulLoginPopup(_popUp);
            }
            if ((UserPrefs.GetSignUpSuccessful().Equals("yes")) && (UserPrefs.GetSignupComplete().Equals("no")))
            {
                loginLogic.SuccessfulSignUpPopup(_popUp);
            }
        }
     
        public void SignOut()
        {
            loginLogic.SetButtonInteractable(FirebaseAuth.DefaultInstance.CurrentUser != null, _logInButton, _signoutButton);
        }
  
    }
}