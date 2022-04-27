using Firebase.Auth;
using Save.Manager;
using TMPro;
using UnityEngine;
using Profile.Logic;
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
        private string _profileFilePath = "/userSave.dat";
        private User _user;

        /// An example of seperation of Logic
        private ProfileLogic profileLogic;

        private void Awake()
        {
            profileLogic = new ProfileLogic();
        }
        /// <summary>
        /// calls load user and set profile text on start
        /// </summary>
        public void Start()
        {
            _user = SaveData.Instance.LoadUserProfile();
            _profileText.text = profileLogic.GetProfileText(_user,
                SaveData.Instance.LoadAndGetStoredSamples().Count,
                 SaveData.Instance.LoadAndGetSubmittedSamples().Count);
        }
        /// <summary>
        /// if no "/userSave.dat" file exits, calls create profile
        /// other wise calles update profile
        /// 
        /// Then show the profile view and sets the profile text
        /// </summary>
        public void SaveProfile()
        {
            profileLogic.UpdateCreateProfile(_userNameInput.text,
                _companyInput.text, _profileFilePath);
            _user = SaveData.Instance.LoadUserProfile();
            SetEditView(false);
            string profileText = profileLogic.GetProfileText(_user,
                SaveData.Instance.LoadAndGetStoredSamples().Count,
                 SaveData.Instance.LoadAndGetSubmittedSamples().Count);
            profileText = profileLogic.GetFirebaseUserProfileText(_user, profileText);
            _profileText.text = profileText;
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

    }
}