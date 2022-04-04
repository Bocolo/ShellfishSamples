using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Auth;
public class UserAppProfile : MonoBehaviour
{
    [SerializeField] private TMP_Text _profileText;

    [SerializeField] private TMP_InputField _userNameInput;
    [SerializeField] private TMP_InputField _companyInput;

    [SerializeField] private GameObject _updateProfileButton;
    [SerializeField] private GameObject _saveProfileButton;
    private User user;
    public void Start()
    {
        LoadProfile();
        Debug.Log("Loaded profile");
    }
    private void LoadProfile()
    {
        user = SaveData.Instance.LoadUserProfile();
        //neeed to laod the user submitted sample stored
        string profileText = "<b>Name : </b>" + user.Name 
             + "\n\n<b>Company: </b>" + user.Company
             + "\n\n<b>No of Stored Samples on Device: </b>" + SaveData.Instance.GetUserStoredSamples().Count
             + "\n\n<b>No of Submitted Samples from this Device: </b>" + SaveData.Instance.GetUserSubmittedSamples().Count;

        //Can fic theis by making stores submitted samples equal to the upd.ssc
        //then no need for if else
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            profileText += "\n\n<b>No of Submitted Samples from logged in user: </b>" + user.SubmittedSamplesCount;
        }
     
        _profileText.text = profileText;
            Debug.Log(user.Name + "___LOADED___" + user.Company);
    }

    public void CreateProfile()
    {
        User user = new User
        {
            Name = _userNameInput.text,
            Company = _companyInput.text,

        };
        SaveData.Instance.SaveUserProfile(user);
        Debug.Log(user.Name + "___CREATED___" + user.Company);

    }

    //https://www.youtube.com/watch?v=52yUcKLMKX0
    public void UpdateProfile()
    {
        user = SaveData.Instance.LoadUserProfile();
        user.Name = _userNameInput.text;
        user.Company = _companyInput.text;
        if(FirebaseAuth.DefaultInstance.CurrentUser!= null){
          SaveData.Instance.SaveUserProfile(user, FirebaseAuth.DefaultInstance.CurrentUser);

        }
        else
        {
            SaveData.Instance.SaveUserProfile(user);
        }
        Debug.Log(user.Name + "__Updating user____" + user.Company);
    }
    public void SaveProfile()
    {
        string filepath = Application.persistentDataPath + "/userSave.dat";

        if (System.IO.File.Exists(filepath))
        {
            UpdateProfile();
        }
        else
        {
            CreateProfile();
        }
        _profileText.gameObject.SetActive(true);
        _updateProfileButton.SetActive(true);

        _saveProfileButton.SetActive(false);
        _userNameInput.gameObject.SetActive(false);
        _companyInput.gameObject.SetActive(false);
        _saveProfileButton.SetActive(false);

        LoadProfile();
    }
    public void GoToUpdateProfile()
    {
        _profileText.gameObject.SetActive(false);
        _updateProfileButton.SetActive(false);

        _saveProfileButton.SetActive(true);
        _userNameInput.gameObject.SetActive(true);
        _companyInput.gameObject.SetActive(true);
        _saveProfileButton.SetActive(true);
    }
}
