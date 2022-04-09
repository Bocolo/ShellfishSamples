using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

public class LogInOutButtonManager : MonoBehaviour
{
    [SerializeField] private bool isLoggedIn;
    [SerializeField] private Button logInButton;
    [SerializeField] private Button signoutButton;

    private void Start()
    {
        SetLoggedIn(FirebaseAuth.DefaultInstance.CurrentUser != null);
    }

    public void SetLoggedIn(bool isLoggedIn)
    {
        this.isLoggedIn = isLoggedIn;
        Debug.Log("Is someone logged in ?? " + this.isLoggedIn);
        SetLogInOutButtonInteractable();

    }
    private void SetLogInOutButtonInteractable()
    {
        if (this.isLoggedIn)
        {
            logInButton.interactable = false;
            signoutButton.interactable = true;
            Debug.Log("Loggin button should be inactive");
        }
        else
        {
            logInButton.interactable = true;
            signoutButton.interactable = false;
            Debug.Log("SIGNOUY button should be inactive");

        }
    }
    #if UNITY_INCLUDE_TESTS
    public bool GetLoggedIn()
    {
        return this.isLoggedIn;
    }
    public void SetButtons()
    {
        GameObject go1 = new GameObject();
        GameObject go2 = new GameObject();
        SetLoginButton(go1);
        SetSignoutButton(go2);

    }
    //bacse adding to this, doesnt work --- mroe than one btn component
    public void SetLoginButton(GameObject go)
    {
        logInButton= go.AddComponent<Button>();
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
    public void TestButtonInteractable()
    {
        //prob not needed
        SetLogInOutButtonInteractable();
    }
#endif
}