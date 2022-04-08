using TMPro;
using UnityEngine;
public class PopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text popUpText;

    public void PopUpAcknowleged()
    {
        this.gameObject.SetActive(false);

    }
    public void SetPopUpText(string text)
    {
        this.gameObject.SetActive(true);
        popUpText.text = "\n\n" + text;
    }
    public void SuccessfulLogin()
    {
        SetPopUpText("Login Successful");
    }
    public void SuccessfulSignUp()
    {
        SetPopUpText("Sign Up Successful");
    }
    public void UnSuccessfulLogin()
    {
        SetPopUpText("Login NOT Successful");
    }
    public void UnSuccessfulSignUp()
    {
        SetPopUpText("Sign Up NOT Successful");
    }


}
