using App.Settings.Prefrences;
using App.UI;
using UnityEngine.UI;
namespace App.Authentication.UI
{
    /// <summary>
    /// This script is responsible login related to login behaviour
    /// </summary>
    public class LoginLogic
    {
        /// <summary>
        /// Sets the interactability of the login and signout button based on the isLoggedIn bool
        /// </summary>
        /// <param name="isLoggedIn">bool used in if else, represents if a user id logged in</param>
        public void SetButtonInteractable(bool isLoggedIn, Button loginButton, Button logOutButton)
        {
            if (isLoggedIn)
            {
                loginButton.interactable = false;
                logOutButton.interactable = true;
            }
            else
            {
                loginButton.interactable = true;
                logOutButton.interactable = false;
            }
        }

        /// <summary>
        /// calls the pop up successfull login method
        /// and set the login player pref
        /// </summary>
        /// <param name="popUp">the pop to activate</param>
        public void SuccessfulLoginPopup(PopUp popUp)
        {
            popUp.SuccessfulLogin();
            UserPrefs.SetLoginComplete("yes");

        }
        /// <summary>
        /// calls the pop up successfull signup method
        /// and sets the sign up player pref
        /// </summary>
        /// <param name="popUp">the pop to activate</param>
        public void SuccessfulSignUpPopup(PopUp popUp)
        {
            popUp.SuccessfulSignUp();
            UserPrefs.SetSignUpComplete("yes");
        }
    }
}