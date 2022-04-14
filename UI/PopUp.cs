using TMPro;
using UnityEngine;
namespace UI.Popup
{
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
        public void SuccessfulSubmission()
        {
            SetPopUpText("Sample Successfully Submitted");
        }
        public void SuccessfulStorage()
        {
            SetPopUpText("Sample Successfully Stored");
        }
#if UNITY_INCLUDE_TESTS
        public void SetPopUp()
        {
            popUpText = this.gameObject.AddComponent<TextMeshPro>();
        }

        public string GetPopUpText()
        {
            return popUpText.text;
        }
#endif
    }
}