using App.SaveSystem.Manager;
using Firebase.Auth;
using UnityEngine;
using Users.Data;

namespace App.Profile
{
    /// <summary>
    /// Manages profile logic and behaviour
    /// </summary>
    public class ProfileLogic
    {
        #region "Profile Strings"
        /// <summary>
        /// returns a string representing profile information
        /// uses user and sample list details to populate string
        /// </summary>
        /// <param name="user">the user to extract details from</param>
        /// <param name="storedSamplesCount">the number of stored samples</param>
        /// <param name="submittedSamplesCount">the number of submitted sample</param>
        /// <returns></returns>
        public string GetProfileText(User user, int storedSamplesCount, int submittedSamplesCount)
        {
            string profileText = "<b>Name : </b>" + user.Name
                    + "\n\n<b>Company: </b>" + user.Company
                    + "\n\n<b>No of Stored Samples on Device: </b>" +
                    storedSamplesCount
                    + "\n\n<b>No of Submitted Samples from this Device: </b>" +
                    submittedSamplesCount;

            return profileText;
        }
        /// <summary>
        ///  returns a string representing profile information related to 
        ///  the firebase user, if one is logged in
        /// </summary>
        /// <param name="user">the current user to extract sample details from</param>
        /// <param name="profileText">the string to add to </param>
        /// <returns></returns>
        public string GetFirebaseUserProfileText(User user, string profileText)
        {
            if (FirebaseAuth.DefaultInstance.CurrentUser != null)
            {
                profileText += "\n\n<b>No of Submitted Samples from logged in user: </b>" +
                        user.SubmittedSamplesCount;
            }
            return profileText;
        }
        #endregion
        #region "Profile account management"
        /// <summary>
        /// Saves the passed users details through the save data instance's
        /// save user profile method
        /// </summary>
        /// <param name="user">the user to save</param>
        private void SaveUserProfile(User user)
        {
            if (FirebaseAuth.DefaultInstance.CurrentUser != null)
            {
                SaveData.Instance.SaveUserProfile(user, FirebaseAuth.DefaultInstance.CurrentUser);
            }
            else
            {
                SaveData.Instance.SaveUserProfile(user);
            }
        }
        /// <summary>
        /// loads a user from the save data instance, updates the user
        /// details with the passed params and saves the updated user
        /// </summary>
        /// <param name="newUsername">the new user name</param>
        /// <param name="newCompanyName"> the new user company</param>
        public void UpdateProfile(string newUsername, string newCompanyName)
        {
            User user = SaveData.Instance.LoadUserProfile();
            user.Name = newUsername;
            user.Company = newCompanyName;
            SaveUserProfile(user);
        }
        /// <summary>
        /// creates a user profile from the passed params and saves the profile
        /// </summary>
        /// <param name="newUsername">the new users name</param>
        /// <param name="newCompanyName">the new users companu</param>
        public void CreateProfile(string newUsername, string newCompanyName)
        {
            User newUser = new User
            {
                Name = newUsername,
                Company = newCompanyName,
            };
            SaveData.Instance.SaveUserProfile(newUser);
        }
        /// <summary>
        /// Decides whether to create a new profile or update a profile
        /// based on whether a save file exists
        /// </summary>
        /// <param name="newUsername"></param>
        /// <param name="newCompanyName"></param>
        /// <param name="fileLocation"></param>
        public void UpdateCreateProfile(string newUsername, string newCompanyName, string fileLocation)
        {
            string filepath = Application.persistentDataPath + fileLocation;
            if (System.IO.File.Exists(filepath))
            {
                UpdateProfile(newUsername, newCompanyName);
            }
            else
            {
                CreateProfile(newUsername, newCompanyName);
            }
        }
        #endregion
    }
}