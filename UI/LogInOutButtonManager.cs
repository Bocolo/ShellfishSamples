using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
namespace UI.Authentication
{
    /// <summary>
    /// Manages Log in and Sign out buttons
    /// </summary>
    public class LogInOutButtonManager : MonoBehaviour
    {
        private bool _isLoggedIn; //{  private get;  set; }
        [SerializeField] private Button _logInButton;
        [SerializeField] private Button _signoutButton;
        /// <summary>
        /// sets the loggedIn bool based on whether a firestore user is logegd in
        /// calls SetLogInOutButtonInteractable passing the isLoggedIn bool
        /// </summary>
        private void Start()
        {
            _isLoggedIn = FirebaseAuth.DefaultInstance.CurrentUser != null;
            SetLogInOutButtonInteractable(_isLoggedIn);
        }
        /// <summary>
        /// Sets the interactability of the login and signout button based on the isLoggedIn bool
        /// </summary>
        /// <param name="isLoggedIn">bool used in if else, represents if a user id logged in</param>
        private void SetLogInOutButtonInteractable(bool isLoggedIn)
        {
            if (isLoggedIn)
            {
                _logInButton.interactable = false;
                _signoutButton.interactable = true;
            }
            else
            {
                _logInButton.interactable = true;
                _signoutButton.interactable = false;
            }
        }
#if UNITY_INCLUDE_TESTS
        public bool GetLoggedIn()
        {
            return this._isLoggedIn;
        }
        public void SetTestButtons()
        {
            GameObject go1 = new GameObject();
            GameObject go2 = new GameObject();
            SetLoginButton(go1);
            SetSignoutButton(go2);
        }
        //bacse adding to this, doesnt work --- mroe than one btn component
        public void SetLoginButton(GameObject go)
        {
            _logInButton = go.AddComponent<Button>();
        }
        public void SetSignoutButton(GameObject go)
        {
            _signoutButton = go.AddComponent<Button>();
        }
        public Button GetLoginButton()
        {
            return this._logInButton;
        }
        public Button GetSignoutButton()
        {
            return this._signoutButton;
        }
        public void TestButtonInteractable(bool isLoggedIn)
        {
            SetLogInOutButtonInteractable(isLoggedIn);
        }
#endif
    }
}