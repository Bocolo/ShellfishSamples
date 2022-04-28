using TMPro;
using UnityEngine;
namespace App.UI
{
    /// <summary>
    /// Manages Pop up objects:
    /// their active status and text
    /// </summary>
    public class PopUp : MonoBehaviour
    {
        [SerializeField] private TMP_Text _popUpText;
        /// <summary>
        /// deactivates the game object holding this script
        /// </summary>
        public void PopUpAcknowleged()
        {
            this.gameObject.SetActive(false);
        }
        /// <summary>
        /// activates the game object holding this script
        /// sets the pop up text to the passed text
        /// </summary>
        /// <param name="text">text to set the pop up text to</param>
        public void SetPopUpText(string text)
        {
            this.gameObject.SetActive(true);
            _popUpText.text = "\n\n" + text;
        }
        /// <summary>
        /// calls SetPopUpText with login text
        /// </summary>
        public void SuccessfulLogin()
        {
            SetPopUpText("Login Successful");
        }
        /// <summary>
        /// calls SetPopUpText with sign up text
        /// </summary>
        public void SuccessfulSignUp()
        {
            SetPopUpText("Sign Up Successful");
        }
        /// <summary>
        /// calls SetPopUpText with login text
        /// </summary>
        public void UnSuccessfulLogin()
        {
            SetPopUpText("Login NOT Successful");
        }
        /// <summary>
        /// calls SetPopUpText with signup text
        /// </summary>
        public void UnSuccessfulSignUp()
        {
            SetPopUpText("Sign Up NOT Successful");
        }
        /// <summary>
        /// calls SetPopUpText with submission text
        /// </summary>
        public void SuccessfulSubmission()
        {
            SetPopUpText("Sample Successfully Submitted");
        }
        /// <summary>
        /// calls SetPopUpText with storage text
        /// </summary>
        public void SuccessfulStorage()
        {
            SetPopUpText("Sample Successfully Stored");
        }

    }
}