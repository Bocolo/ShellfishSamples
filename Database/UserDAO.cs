using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

using Firebase.Auth;
using System.Threading.Tasks;
using System;

public class UserDAO 
{
    private FirebaseFirestore firestore;
    private FirebaseAuth auth;
    private bool isSignUpSuccesful;
    private bool isSuccessfulLogin;
    private User user;
    public UserDAO()
    {
        firestore = FirebaseFirestore.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;
    }

    public void addUser(User user)
    {
        firestore.Collection("Users").Document(user.Email).SetAsync(user);
        ///////////
        ///NEW COLLECTION DATA IN USERS 
    }
    public void UpdateUser(FirebaseUser user)
    {
//check the firebase authentication - combine this mess
    }
 

    //does this gotta await sync?
    public async void GetUser()
    {
      //  User user = new User();
        DocumentReference docRef = firestore.Collection("Users").Document(auth.CurrentUser.Email);
        await docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Debug.Log(task.Result + " --- > " + task.Result.GetValue<int>("SubmittedSamplesCount"));
            try
            {
                user = task.Result.ConvertTo<User>();
                SaveData.Instance.SaveUserProfile(user);
                Debug.Log(SaveData.Instance.LoadUserProfile().Email + "new usre data");
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
    /// <param name="user"></param>
    public void UpdateUserSampleCount(FirebaseUser user)
    {
        ///much better
        DocumentReference docRef = firestore.Collection("Users").Document(user.Email);
      //  docRef.UpdateAsync("SubmittedSamplesCount", FieldValue.Increment(1))
        docRef.UpdateAsync("SubmittedSamplesCount", FieldValue.Increment(1)).ContinueWithOnMainThread(task => {

        });
        User user = SaveData.Instance.LoadUserProfile();
        user.SubmittedSamplesCount++;
        SaveData.Instance.SaveUserProfile(user);



    }
    public void UpdateUserSampleCount(FirebaseUser user, int numberOfSamples)
    {

        //adding the await ---
        DocumentReference docRef = firestore.Collection("Users").Document(user.Email);
        docRef.UpdateAsync("SubmittedSamplesCount", FieldValue.Increment(numberOfSamples)).ContinueWithOnMainThread(task => {
     
        });
        User user = SaveData.Instance.LoadUserProfile();
        user.SubmittedSamplesCount += numberOfSamples;
        SaveData.Instance.SaveUserProfile(user);

    }










    /// <summary>
    /// //////fiire base auth is different from user
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>


    //These maybe should not be here
    private async Task ValidateAuthenticationLogin(string email, string password)
    {
        auth = FirebaseAuth.DefaultInstance;//?

        await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                isSuccessfulLogin = false;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception.Message);
                isSuccessfulLogin = false;

                //pop up invalid login
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            isSuccessfulLogin = true;
        });

    }
    public async Task AddUser(string email, string password, string name, string company)
    {

        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            Debug.Log("Testing 5");

            if (task.IsCanceled)
            {
                isSignUpSuccesful = false;

                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                isSignUpSuccesful = false;

                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception.Message);
                return;
            }

            FirebaseUser user = task.Result;

            auth.CurrentUser.UpdateUserProfileAsync(new UserProfile
            {
                DisplayName = name,
            });
            isSignUpSuccesful = true;
            return;
        });
        ///
        ///Wont wotk gere
        ///
      //sign up logs in


    }
    public bool GetSignUpSuccess()
    {
        return isSignUpSuccesful;
    }
}

/// <summary>
/// gotta check if this actually works 
/// </summary>
/// <param name="firebaseUser"></param>
/// <returns></returns>
/*    public async Task<User> GetUser( )
    {
        User user = new User();
        DocumentReference docRef = firestore.Collection("Users").Document(auth.CurrentUser.Email);
        await docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Debug.Log(task.Result + " --- > " + task.Result.GetValue<int>("SubmittedSamplesCount"));
            try
            {
                 user = task.Result.ConvertTo<User>();
                SaveData.Instance.SaveUserProfile(user);
                Debug.Log(SaveData.Instance.LoadUserProfile().Email + "new usre data");
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

        });
        return user;
    }*/

/* int previouslyStoredSampleCount = 0;
 docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
 {
     DocumentSnapshot documentSnapshot = task.Result;
     previouslyStoredSampleCount = documentSnapshot.GetValue<int>("SubmittedSamplesCount");
     previouslyStoredSampleCount += 1;
     docRef.SetAsync(new Dictionary<string, int> { { "SubmittedSamplesCount", previouslyStoredSampleCount } }, SetOptions.MergeAll);
     User user = SaveData.Instance.LoadUserProfile();
     user.SubmittedSamplesCount = previouslyStoredSampleCount;
     SaveData.Instance.SaveUserProfile(user);
     Debug.Log("_TESTING--- Fs DB Count update");
 });
*/



/*  docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
  {
      DocumentSnapshot documentSnapshot = task.Result;
      previouslyStoredSampleCount = documentSnapshot.GetValue<int>("SubmittedSamplesCount");
      previouslyStoredSampleCount += 1;
      docRef.UpdateAsync("SubmittedSampleCount", FieldValue.Increment(1));
      docRef.UpdateAsync("SubmittedSamplesCount", previouslyStoredSampleCount); //, SetOptions.MergeAll); 
      User user = SaveData.Instance.LoadUserProfile();
      user.SubmittedSamplesCount = previouslyStoredSampleCount;
      SaveData.Instance.SaveUserProfile(user);
  });*/
/*
        DocumentReference docRef = firestore.Collection("Users").Document(user.Email);

        int previouslyStoredSampleCount = 0;
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot documentSnapshot = task.Result;
            previouslyStoredSampleCount = documentSnapshot.GetValue<int>("SubmittedSamplesCount");
            previouslyStoredSampleCount += numberOfSamples;
            //ocRef.UpdateAsync("SubmittedSamplesCount", previouslyStoredSampleCount);
            docRef.SetAsync(new Dictionary<string, int> { { "SubmittedSamplesCount", previouslyStoredSampleCount } }, SetOptions.MergeAll);
            User user = SaveData.Instance.LoadUserProfile();
            user.SubmittedSamplesCount = previouslyStoredSampleCount;
            SaveData.Instance.SaveUserProfile(user);
            Debug.Log("_TESTING--- Fs DB Count update-number samples");
        });*/
