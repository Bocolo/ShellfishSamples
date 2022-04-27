using System.Collections;
using System.Collections.Generic;
using UI.Popup;
using UnityEngine;
using UnityEngine.UI;
namespace Login.Logic { 
    public class LoginLogic 
    {
        /// <summary>
        /// Sets the interactability of the login and signout button based on the isLoggedIn bool
        /// </summary>
        /// <param name="isLoggedIn">bool used in if else, represents if a user id logged in</param>
        public void SetButtonInteractable(bool isLoggedIn,Button loginButton, Button logOutButton)
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


        public void SuccessfulLoginPopup(PopUp popUp)
        {
            popUp.SuccessfulLogin();
            UserPrefs.SetLoginComplete("yes");

        }

        public void SuccessfulSignUpPopup(PopUp popUp)
        {
            popUp.SuccessfulSignUp();
            UserPrefs.SetSignUpComplete("yes");
        }
    }
}