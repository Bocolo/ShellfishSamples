using App.SaveSystem.Manager;
using TMPro;
using UnityEngine;
using Users.Data;

namespace App.Profile.UI
{
    /// <summary>
    /// Manages the behavaiour of the Profile page
    /// </summary>
    public class ProfileManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _profileText;
        [SerializeField] private TMP_InputField _userNameInput;
        [SerializeField] private TMP_InputField _companyInput;
        [SerializeField] private GameObject _updateProfileButton;
        [SerializeField] private GameObject _saveProfileButton;
        private readonly string _profileFilePath = "/userSave.dat";
        private User _user;
        private ProfileLogic profileLogic;
        /// <summary>
        /// creates the profile logic object
        /// </summary>
        private void Awake()
        {
            profileLogic = new ProfileLogic();
        }
        /// <summary>
        ///  load the user and sets profile text on start
        /// </summary>
        public void Start()
        {
            _user = SaveData.Instance.LoadUserProfile();
            _profileText.text = profileLogic.GetProfileText(_user,
                SaveData.Instance.LoadAndGetStoredSamples().Count,
                 SaveData.Instance.LoadAndGetSubmittedSamples().Count);
        }
        /// <summary>
        /// updates or create a user profile
        /// loads the user profile
        /// set the profile view and populates the profile text
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
        /// game objects for profile or edit view
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