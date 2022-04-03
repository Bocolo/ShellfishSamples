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
    private UserData userProfileData;
    public void Start()
    {
        LoadProfile();
        Debug.Log("Loaded profile");
    }
    private void LoadProfile()
    {
        userProfileData = SaveData.Instance.LoadUserProfile();
        //neeed to laod the user submitted sample stored
        string profileText = "<b>Name : </b>" + userProfileData.Name 
             + "\n\n<b>Company: </b>" + userProfileData.Company
             + "\n\n<b>No of Stored Samples on Device: </b>" + SaveData.Instance.GetUserStoredSamples().Count
             + "\n\n<b>No of Submitted Samples from this Device: </b>" + SaveData.Instance.GetUserSubmittedSamples().Count;

        //Can fic theis by making stores submitted samples equal to the upd.ssc
        //then no need for if else
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            profileText += "\n\n<b>No of Submitted Samples from logged in user: </b>" + userProfileData.SubmittedSamplesCount;
        }
     
        _profileText.text = profileText;
            Debug.Log(userProfileData.Name + "___LOADED___" + userProfileData.Company);
    }

    public void CreateProfile()
    {
        UserData user = new UserData
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
        userProfileData = SaveData.Instance.LoadUserProfile();
        userProfileData.Name = _userNameInput.text;
        userProfileData.Company = _companyInput.text;
        //FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        //if (user)
        if(FirebaseAuth.DefaultInstance.CurrentUser!= null){
          SaveData.Instance.SaveUserProfile(userProfileData, FirebaseAuth.DefaultInstance.CurrentUser);

        }
        else
        {
            SaveData.Instance.SaveUserProfile(userProfileData);
        }
        //else
        //{
        //    SaveData.Instance.SaveUserProfile(userProfileData);

        //}
        Debug.Log(userProfileData.Name + "__Updating user____" + userProfileData.Company);
    }
    public void SaveProfile()
    {
        string filepath = Application.persistentDataPath + "/userProfileSave.dat";

        if (System.IO.File.Exists(filepath))
        {
            ///
            ///FIC THIS -- to messy simpler solution excists
            ///
            ///
            UpdateProfile();

        }
        else
        {
            CreateProfile();

        }
        //UpdateProfile();
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
