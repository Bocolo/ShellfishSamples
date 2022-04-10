using Firebase.Auth;
using TMPro;
using UnityEngine;
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
        Debug.Log("Loading profile");
        LoadProfile();
        Debug.Log("Loaded profile");
    }
    private void LoadProfile()
    {
        user = SaveData.Instance.LoadUserProfile();
        //neeed to laod the user submitted sample stored
        //maybe do if protext not nulll - in order to correctly execute testing
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

    //is this used?
    //these should be private used in save profile
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
        /*      _profileText.gameObject.SetActive(true);
              _updateProfileButton.SetActive(true);
              //remove addintioal save prod button here?
              _saveProfileButton.SetActive(false);
              _userNameInput.gameObject.SetActive(false);
              _companyInput.gameObject.SetActive(false);
              _saveProfileButton.SetActive(false);
      */
        GoToViewProfile();
        LoadProfile();
    }
    public void GoToViewProfile()
    {
        _profileText.gameObject.SetActive(true);
        _updateProfileButton.SetActive(true);
        //remove addintioal save prod button here?
        _saveProfileButton.SetActive(false);
        _userNameInput.gameObject.SetActive(false);
        _companyInput.gameObject.SetActive(false);
        _saveProfileButton.SetActive(false);

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
#if UNITY_INCLUDE_TESTS
    //need to solve this without awake-- possibly use the internal testing for unity
    // review in morning
    //using awake in unity testing is still calling awake in actiual play mode
 /*   public void Awake()
    {
        SetInputTextFields();
        Debug.Log("Mid wake");
        SetTexts();
    }*/
    public void SetTexts()
    {
        _userNameInput.text = "Test Name";
        _companyInput.text = "Test Company";
    }
    public void SetInputTextFields()
    {
        Debug.Log("GP1TG");
        GameObject go1 = new GameObject();
        Debug.Log("GP2TG");
        GameObject go2 = new GameObject();
        GameObject go3 = new GameObject();
        // _profileText = go3.AddComponent<TMP_Text>();
        _profileText = go3.AddComponent<TextMeshPro>();
        _userNameInput = go1.AddComponent<TMP_InputField>();
        _companyInput = go2.AddComponent<TMP_InputField>();
        _updateProfileButton = new GameObject();
        _saveProfileButton = new GameObject();
    }
    public GameObject GetProfileTextGO()
    {
        Debug.Log("GP3TG");
        return _profileText.gameObject;
    }
    public GameObject GetUserNameInputGO()
    {
        return _userNameInput.gameObject;
    }
    public GameObject GetUserCompanyInputGO()
    {
        return _userNameInput.gameObject;
    }
    public GameObject GetSaveProfileButtonGO()
    {
        return _saveProfileButton;
    }
    public GameObject GetUpdateProfileButtonGO()
    {
        return _updateProfileButton;
    }
#endif
}
