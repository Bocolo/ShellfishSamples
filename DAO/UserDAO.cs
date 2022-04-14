using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using Save.Manager;
using System;
using UnityEngine;

namespace Data.Access
{
    public class UserDAO
    {
        private FirebaseFirestore _firestore;
        private FirebaseAuth _auth;
        private User _user;
        public UserDAO()
        {
            _firestore = FirebaseFirestore.DefaultInstance;
            _auth = FirebaseAuth.DefaultInstance;
        }
        public void AddUser(User user)
        {
            _firestore.Collection("Users").Document(user.Email).SetAsync(user);
        }
        public void UpdateUser(FirebaseUser user)
        {
        }
        public async void GetUser()
        {
            DocumentReference docRef = _firestore.Collection("Users").Document(_auth.CurrentUser.Email);
            await docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                try
                {
                    _user = task.Result.ConvertTo<User>();
                    SaveData.Instance.SaveUserProfile(_user);
                }
                catch (Exception e)
                {
                    Debug.Log("GetUser: "+e.StackTrace);
                }
            });
        }
        /// <summary>
        /// fix with update async 
        /// https://firebase.google.com/docs/firestore/manage-data/add-data
        /// </summary>
        /// <param name="firebaseUser"></param>
        public void UpdateUserSampleCount(FirebaseUser firebaseUser)
        {
            DocumentReference docRef = _firestore.Collection("Users").Document(firebaseUser.Email);
            //  docRef.UpdateAsync("SubmittedSamplesCount", FieldValue.Increment(1))
            docRef.UpdateAsync("SubmittedSamplesCount", FieldValue.Increment(1)).ContinueWithOnMainThread(task =>
            {
            });
            User user = SaveData.Instance.LoadUserProfile();
            user.SubmittedSamplesCount++;
            SaveData.Instance.SaveUserProfile(user);
        }
        public void UpdateUserSampleCount(FirebaseUser firebaseUser, int numberOfSamples)
        {
            DocumentReference docRef = _firestore.Collection("Users").Document(firebaseUser.Email);
            docRef.UpdateAsync("SubmittedSamplesCount", FieldValue.Increment(numberOfSamples)).ContinueWithOnMainThread(task =>
            {
            });
            User user = SaveData.Instance.LoadUserProfile();
            user.SubmittedSamplesCount += numberOfSamples;
            SaveData.Instance.SaveUserProfile(user);
        }
    }
}