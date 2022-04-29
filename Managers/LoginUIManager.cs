using App.Settings.Prefrences;
using App.UI;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

namespace App.Authentication.UI
{
    /// <summary>
    /// Manages Ui related to Log in and Sign out behaviours
    /// </summary>
    public class LoginUIManager : MonoBehaviour
    {
        private bool _isLoggedIn;
        [SerializeField] private Button _logInButton;
        [SerializeField] private Button _signoutButton;
        [SerializeField] private PopUp _popUp;
        private LoginLogic loginLogic;
        /// <summary>
        /// Creates the login logic object 
        /// check for a logged in user andsets the required buttons active
        /// Activates a pop up if the correct user pref are set 
        /// </summary>
        private void Start()
        {
            loginLogic = new LoginLogic();
            _isLoggedIn = FirebaseAuth.DefaultInstance.CurrentUser != null;
            loginLogic.SetButtonInteractable(_isLoggedIn, _logInButton, _signoutButton);
            if ((UserPrefs.GetSignUpSuccessful().Equals("yes")) &&
                (UserPrefs.GetLoginComplete().Equals("no")))
            {
                loginLogic.SuccessfulLoginPopup(_popUp);
            }
            if ((UserPrefs.GetSignUpSuccessful().Equals("yes")) &&
                (UserPrefs.GetSignupComplete().Equals("no")))
            {
                loginLogic.SuccessfulSignUpPopup(_popUp);
            }
        }
        /// <summary>
        /// The sign out function sets the correct login/signout buttons active
        /// </summary>
        public void SignOut()
        {
            loginLogic.SetButtonInteractable(FirebaseAuth.DefaultInstance.CurrentUser != null, _logInButton, _signoutButton);
        }

    }
}