using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject popUpObject;

    public void RetrievalPage()
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user != null)
        {
            Debug.Log("USER IS NOT NULL");
            SceneManager.LoadScene(2);
        }
        else
        {
            popUpObject.GetComponent<PopUp>().SetPopUpText("You Must Be Signed in to Access the Retrieval Page");
            Debug.Log("Please sign in to access database");
        }
    }

    public void LogOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        SaveData.Instance.SaveUserProfile(new User { });
    }

    public void SubmitPage()
    {
        SceneManager.LoadScene(1);
    }
    public void UserSamplesPage()
    {
        SceneManager.LoadScene(3);
    }

    public void ProfilePage()
    {
        SceneManager.LoadScene(4);
    }

    public void LoginPage()
    {
        SceneManager.LoadScene(5);
    }

    public void HelpPage()
    {
        SceneManager.LoadScene(6);
    }
}
