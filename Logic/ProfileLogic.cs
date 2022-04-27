using Firebase.Auth;
using Save.Manager;
using UnityEngine;
namespace Profile.Logic { 
    public class ProfileLogic
    {
        public string GetProfileText(User user, int storedSamplesCount,int submittedSamplesCount)
        {
            string profileText = "<b>Name : </b>" + user.Name
                    + "\n\n<b>Company: </b>" + user.Company
                    + "\n\n<b>No of Stored Samples on Device: </b>" + 
                    storedSamplesCount
                    + "\n\n<b>No of Submitted Samples from this Device: </b>" + 
                    submittedSamplesCount;
     
            return profileText;
        }
        public string GetFirebaseUserProfileText(User user, string profileText)
        {
            if (FirebaseAuth.DefaultInstance.CurrentUser != null)
            {
                profileText += "\n\n<b>No of Submitted Samples from logged in user: </b>" +
                        user.SubmittedSamplesCount;
            }
            return profileText;
        }
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
        private User LoadUser()
        {
            return SaveData.Instance.LoadUserProfile();
        }
        public void UpdateProfile(string newUsername, string newCompanyName)
        {
            User user = LoadUser();
            user.Name = newUsername;
            user.Company = newCompanyName;
            SaveUserProfile(user);
        }
        public void CreateProfile(string newUsername, string newCompanyName)
        {
            User newUser = new User
            {
                Name = newUsername,
                Company = newCompanyName,
        };
            SaveData.Instance.SaveUserProfile(newUser);
        }
        public void UpdateCreateProfile(string newUsername, string newCompanyName,string fileLocation)
        {
            string filepath = Application.persistentDataPath + fileLocation;
            if (System.IO.File.Exists(filepath))
            {
                UpdateProfile(newUsername,newCompanyName);
            }
            else
            {
                CreateProfile(newUsername, newCompanyName);
            }
        }
    }
}