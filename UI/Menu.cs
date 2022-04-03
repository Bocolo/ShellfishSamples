using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using UnityEngine.UI;
using TMPro;
public class Menu : MonoBehaviour
{
   // [SerializeField] private TMP_Text popUP;
    [SerializeField] private GameObject popUpObject;
 
    public void RetrievalPage()
    {
        // FirebaseAuth auth;
        //FirebaseAuth.DefaultInstance.SignOut();
         FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user != null)
        {
            Debug.Log("USER IS NOT NULL");
            SceneManager.LoadScene(2);
        }
        else
        {
            popUpObject.GetComponent<PopUp>().SetPopUpText("You Must Be Signed in to Access the Retrieval Page");
            //error.transform.GetChild(0).GetChild(0).GetComponent<TMP_InputField>().text = "TEST";
            Debug.Log("Please sign in to access database");
        }
    }
   
    public void LogOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        SaveData.Instance.SaveUserProfile(new UserData { });
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

//private void SetPopUpText(string popUpText)
//{
//    popUP.transform.parent.parent.gameObject.SetActive(true);
//    popUP.text = "\n\n"+popUpText;
//}
//public void PopUpAcknowleged()
//{
//    //error.SetActive(false);
//    popUP.transform.parent.parent.gameObject.SetActive(false);

//}