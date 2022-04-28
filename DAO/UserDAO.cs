using App.SaveSystem.Manager;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using UnityEngine;

namespace Users.Data.Access
{
    /// <summary>
    /// Manages access to to User data in the Firestore Database
    /// </summary>
    public class UserDAO
    {
        private FirebaseFirestore _firestore;
        private FirebaseAuth _auth;
        private User _user;
        private readonly string _usersCollection = "Users";
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
            _firestore.Collection(_usersCollection).Document(user.Email).SetAsync(user);
        }
     
        /// <summary>
        /// Retrieves a user from the firestore USers collection, 
        /// located in adocument named after the current firestore users email
        /// and saves the user as a user profile
        /// </summary>
        public async void GetUser()
        {
            DocumentReference docRef = _firestore.Collection(_usersCollection).Document(_auth.CurrentUser.Email);
            await docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                try
                {
                    _user = task.Result.ConvertTo<User>();
                    SaveData.Instance.SaveUserProfile(_user);
                }
                catch (Exception e)
                {
                    Debug.Log("Get User Failed: "+e.StackTrace);
                }
            });
        }
 
        /// <summary>
        /// Incremenets the firebaseUsers submitted sample count by one
        /// </summary>
        /// <param name="firebaseUser"> the firebaseUser whose email is used as a document name</param>
        public void UpdateUserSampleCount(FirebaseUser firebaseUser)
        {
            DocumentReference docRef = _firestore.Collection(_usersCollection).Document(firebaseUser.Email);
            //  docRef.UpdateAsync("SubmittedSamplesCount", FieldValue.Increment(1))
            docRef.UpdateAsync("SubmittedSamplesCount", FieldValue.Increment(1)).ContinueWithOnMainThread(task =>
            {
                Debug.Log("User Sample Count has been updated, increased by 1");
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
            DocumentReference docRef = _firestore.Collection(_usersCollection).Document(firebaseUser.Email);
            docRef.UpdateAsync("SubmittedSamplesCount", 
                FieldValue.Increment(numberOfSamples)).ContinueWithOnMainThread(task =>
            {
                Debug.Log("User Sample Count has been updated, increased by "+numberOfSamples);
            });
            User user = SaveData.Instance.LoadUserProfile();
            user.SubmittedSamplesCount += numberOfSamples;
            SaveData.Instance.SaveUserProfile(user);
        }
    }
}