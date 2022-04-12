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
    private FirebaseFirestore firestore;
    private FirebaseAuth auth;
    private User user;
    public UserDAO()
    {
        firestore = FirebaseFirestore.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;
    }
    public void AddUser(User user)
    {
        firestore.Collection("Users").Document(user.Email).SetAsync(user);
        ///////////
        ///NEW COLLECTION DATA IN USERS 
    }
    public void UpdateUser(FirebaseUser user)
    {
    }
    public async void GetUser()
    {
        DocumentReference docRef = firestore.Collection("Users").Document(auth.CurrentUser.Email);
        await docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            try
            {
                user = task.Result.ConvertTo<User>();
                SaveData.Instance.SaveUserProfile(user);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
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
        DocumentReference docRef = firestore.Collection("Users").Document(firebaseUser.Email);
      //  docRef.UpdateAsync("SubmittedSamplesCount", FieldValue.Increment(1))
        docRef.UpdateAsync("SubmittedSamplesCount", FieldValue.Increment(1)).ContinueWithOnMainThread(task => {
        });
        User user = SaveData.Instance.LoadUserProfile();
        user.SubmittedSamplesCount++;
        SaveData.Instance.SaveUserProfile(user);
    }
    public void UpdateUserSampleCount(FirebaseUser firebaseUser, int numberOfSamples)
    {
        DocumentReference docRef = firestore.Collection("Users").Document(firebaseUser.Email);
        docRef.UpdateAsync("SubmittedSamplesCount", FieldValue.Increment(numberOfSamples)).ContinueWithOnMainThread(task => {
        });
        User user = SaveData.Instance.LoadUserProfile();
        user.SubmittedSamplesCount += numberOfSamples;
        SaveData.Instance.SaveUserProfile(user);
    }
}
}