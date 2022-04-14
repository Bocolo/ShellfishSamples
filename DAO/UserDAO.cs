using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using Save.Manager;
using System;
using UnityEngine;
namespace Data.Access
{
    /// <summary>
    /// Manages access to to User data in Firestore Database
    /// </summary>
    public class UserDAO
    {
        private FirebaseFirestore _firestore;
        private FirebaseAuth _auth;
        private User _user;
        /// <summary>
        /// Constructor: sets the firestore instance
        /// </summary>
        public UserDAO()
        {
            _firestore = FirebaseFirestore.DefaultInstance;
            _auth = FirebaseAuth.DefaultInstance;
        }
        /// <summary>
        /// Adds a user to the firestore User collection
        /// </summary>
        /// <param name="user">the user to add</param>
        public void AddUser(User user)
        {
            _firestore.Collection("Users").Document(user.Email).SetAsync(user);
        }
        public void UpdateUser(FirebaseUser user)
        {
        }
        /// <summary>
        /// Retrieves a user from the firestore USers collection, 
        /// located in adocument named after the current firestore users email
        /// and saves the user as a user profile
        /// </summary>
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
        /// 
        /// <summary>
        /// Incremenets the firebaseUsers submitted sample count by one
        /// </summary>
        /// <param name="firebaseUser"> the firebaseUser whose email is used as a document name</param>
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
        /// <summary>
        /// Incremenets the firebaseUsers submitted sample count by the param numberOfSamples
        /// </summary>
        /// <param name="firebaseUser"> the firebaseUser whose email is used as a document name</param>
        /// <param name="numberOfSamples">the number to increment</param>
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