using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
namespace UI.Authentication
{
    public class LogInOutButtonManager : MonoBehaviour
    {
        public bool IsLoggedIn {  private get;  set; }
        [SerializeField] private Button logInButton;
        [SerializeField] private Button signoutButton;
        private void Start()
        {
            IsLoggedIn =FirebaseAuth.DefaultInstance.CurrentUser != null;
            SetLogInOutButtonInteractable(IsLoggedIn);
        }
   /*     private void SetLoggedIn(bool isLoggedIn)
        {
            this.IsLoggedIn = isLoggedIn;
            //   SetLogInOutButtonInteractable(isLoggedIn);
        }*/
        private void SetLogInOutButtonInteractable(bool isLoggedIn)
        {
            if (isLoggedIn)
            {
                logInButton.interactable = false;
                signoutButton.interactable = true;
            }
            else
            {
                logInButton.interactable = true;
                signoutButton.interactable = false;
            }
        }
#if UNITY_INCLUDE_TESTS
        public bool GetLoggedIn()
        {
            return this.IsLoggedIn;
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
            logInButton = go.AddComponent<Button>();
        }
        public void SetSignoutButton(GameObject go)
        {
            signoutButton = go.AddComponent<Button>();
        }
        public Button GetLoginButton()
        {
            return this.logInButton;
        }
        public Button GetSignoutButton()
        {
            return this.signoutButton;
        }
        public void TestButtonInteractable(bool isLoggedIn)
        {
            SetLogInOutButtonInteractable(isLoggedIn);
        }
#endif
    }
}