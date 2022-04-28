using App.SaveSystem.Manager;
using App.UI;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using Users.Data;

namespace App.Navigation
{
    /// <summary>
    /// Manages Main menu operations
    /// </summary>
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject _popUpObject;
        /// <summary>
        /// Redrects to the retrieval page /scene build index 2 if a firebase user is logged in
        /// other wise displays a pop up notification to the user
        /// </summary>
        public void RetrievalPage()
        {
            FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
            if (user != null)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                _popUpObject.GetComponent<PopUp>().SetPopUpText("You Must Be Signed in to Access the Retrieval Page");
            }
        }
        /// <summary>
        /// Signs out the current use from firebase and resets the user profile to 
        /// an empty user
        /// </summary>
        public void LogOut()
        {
            FirebaseAuth.DefaultInstance.SignOut();
            SaveData.Instance.SaveUserProfile(new User { });
        }
        /// <summary>
        /// Redrects to the submit page /scene build index 1
        /// </summary>
        public void SubmitPage()
        {
            SceneManager.LoadScene(1);
        }
        /// <summary>
        /// Redrects to the user sample page /scene build index 3
        /// </summary>
        public void UserSamplesPage()
        {
            SceneManager.LoadScene(3);
        }
        /// <summary>
        /// Redrects to the profile page /scene build index 4
        /// </summary>
        public void ProfilePage()
        {
            SceneManager.LoadScene(4);
        }
        /// <summary>
        /// Redrects to the login page /scene build index 5
        /// </summary>
        public void LoginPage()
        {
            SceneManager.LoadScene(5);
        }
        /// <summary>
        /// Redrects to the help page /scene build index 6
        /// </summary>
        public void HelpPage()
        {
            SceneManager.LoadScene(6);
        }
    }
}