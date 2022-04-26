using Firebase.Auth;
using Save.Manager;
using TMPro;
using UnityEngine;
namespace UI.Profile
{
    /// <summary>
    /// Manages the UI of the Profile page
    /// </summary>
    public class ProfileManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _profileText;
        [SerializeField] private TMP_InputField _userNameInput;
        [SerializeField] private TMP_InputField _companyInput;
        [SerializeField] private GameObject _updateProfileButton;
        [SerializeField] private GameObject _saveProfileButton;
        private string _profileFilePath= "/userSave.dat";
        private User _user;
        /// <summary>
        /// calls load user and set profile text on start
        /// </summary>
        public void Start()
        {
            LoadUser();
            SetProfileText();
        }
        /// <summary>
        /// if no "/userSave.dat" file exits, calls create profile
        /// other wise calles update profile
        /// 
        /// Then show the profile view and sets the profile text
        /// </summary>
        public void SaveProfile()
        {
            string filepath = Application.persistentDataPath + _profileFilePath;
            if (System.IO.File.Exists(filepath))
            {
                UpdateProfile();
            }
            else
            {
                CreateProfile();
            }
            SetEditView(false);
            SetProfileText();
        }
        /// <summary>
        /// sets the user name and company to the name and company input texts
        /// </summary>
        private void SetUserDetailsForUpdate()
        {
            _userNameInput.text = _user.Name;
            _companyInput.text = _user.Company;
        }
        /// <summary>
        /// sets the update profile page inputs
        /// activates the game objects for the update profile page
        /// </summary>
        public void GoToUpdateProfile()
        {
            SetUserDetailsForUpdate();
            SetEditView(true);
        }
      /// <summary>
      /// Uses the passed bool to activate the appropriate
      /// game objects for profile or edif view
      /// </summary>
      /// <param name="isEditView"></param>
       
        private void SetEditView(bool isEditView)
        {
            _profileText.gameObject.SetActive(!isEditView);
            _updateProfileButton.SetActive(!isEditView);
            _saveProfileButton.SetActive(isEditView);
            _userNameInput.gameObject.SetActive(isEditView);
            _companyInput.gameObject.SetActive(isEditView);
        }
        /// <summary>
        /// Sets the profile text with user details
        /// adds additional details if firebase user is logged in
        /// </summary>
        private void SetProfileText()
        {
            string profileText = "<b>Name : </b>" + _user.Name
                 + "\n\n<b>Company: </b>" + _user.Company
                 + "\n\n<b>No of Stored Samples on Device: </b>" + SaveData.Instance.LoadAndGetStoredSamples().Count
                 + "\n\n<b>No of Submitted Samples from this Device: </b>" + SaveData.Instance.LoadAndGetSubmittedSamples().Count;
            //Can fic theis by making stores submitted samples equal to the upd.ssc
            //then no need for if else
            if (FirebaseAuth.DefaultInstance.CurrentUser != null)
            {
                profileText += "\n\n<b>No of Submitted Samples from logged in user: </b>" + _user.SubmittedSamplesCount;
            }
            _profileText.text = profileText;
        }
        /// <summary>
        /// sets the _user to the loaded user from the Save Data instance
        /// </summary>
        private void LoadUser()
        {
            _user = SaveData.Instance.LoadUserProfile();
        }
        /// <summary>
        /// creates a new user and saves that user profile
        /// </summary>
        private void CreateProfile()
        {
            User newUser = new User
            {
                Name = _userNameInput.text,
                Company = _companyInput.text,
            };
            SaveData.Instance.SaveUserProfile(newUser);
        }
        //https://www.youtube.com/watch?v=52yUcKLMKX0
        /// <summary>
        /// Loads a user, updates the name and company details and saves user profile
        /// </summary>
        private void UpdateProfile()
        {
            LoadUser();
            _user.Name = _userNameInput.text;
            _user.Company = _companyInput.text;
            SaveUserProfile();
        }
        /// <summary>
        /// call the  SaveData.Instance.SaveUserProfile passeing user and if a firebase users is logged in
        /// passing the firebase user
        /// </summary>
        private void SaveUserProfile()
        {
            if (FirebaseAuth.DefaultInstance.CurrentUser != null)
            {
                SaveData.Instance.SaveUserProfile(_user, FirebaseAuth.DefaultInstance.CurrentUser);
            }
            else
            {
                SaveData.Instance.SaveUserProfile(_user);
            }
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
        public User GetUser()
        {
            return this._user;
        }
        public void SetUser(User user) => user = this._user;
        public void SetNameAndCompanyTexts()
        {
            _userNameInput.text = "Test Name";
            _companyInput.text = "Test Company";
        }
        public void SetGameObjects()
        {
            GameObject go1 = new GameObject();
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
}