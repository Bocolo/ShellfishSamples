using Firebase.Auth;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Save.Logic;
namespace Save.Manager
{
    /// <summary>
    /// This Singleton manages all saves and retirievals from local storage
    /// </summary>
    public class SaveData : MonoBehaviour
    {
        /// <summary>
        /// checkif i c an remove mono bheaciour
        /// constructor isntead of wake,
        /// don think itll work
        /// </summary>
        /*
         FIX INSTANCE BS -= SMLLA LARG PRIVATE PUBLIC
         */
        public static SaveData Instance { get; private set; }
        private SaveDataLogic saveDataLogic;
        public List<Sample> UsersSubmittedSamples { get; private set; } = new List<Sample>();
        public List<Sample> UsersStoredSamples { get; private set; } = new List<Sample>();
        private string _storedSampleLocation = "/storedSamplesSave.dat";
        private string _submittedSampleLocation = "/submittedSamplesSave.dat";
        private string _userLocation = "/userSave.dat";
        /// <summary>
        /// Sets instance on awake
        /// Set stored and submitted samples lists
        /// </summary>
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
            saveDataLogic = new SaveDataLogic();
            UsersStoredSamples = saveDataLogic.LoadSamples(_storedSampleLocation);
            UsersSubmittedSamples = saveDataLogic.LoadSamples(_submittedSampleLocation);
        
        }
        #region "Samples"
        /// <summary>
        ///  Loads the stored samples from local storage and saves to the UsersStoredSamples lisr
        ///  returns stored samples
        /// </summary>
        /// <returns></returns>
        public List<Sample> LoadAndGetStoredSamples()
        {
            UsersStoredSamples = saveDataLogic.LoadSamples(_storedSampleLocation);
            return this.UsersStoredSamples;
        }
        /// <summary>
        /// Loads the submitted samples from local storage and saves to the UsersSubmittedSamples list
        ///  returns stubmitted samples
        /// </summary>
        /// <returns></returns>
        public List<Sample> LoadAndGetSubmittedSamples()
        {
            UsersSubmittedSamples = saveDataLogic.LoadSamples(_submittedSampleLocation);
            return this.UsersSubmittedSamples;
        }
        /// <summary>
        /// add a sample to the submitted samples list and saves the list to local storage
        /// </summary>
        /// <param name="sample">sample to save</param>
        public void AddAndSaveSubmittedSample(Sample sample)
        {
            AddToSubmittedSamples(sample);
            saveDataLogic.SaveSamples(_submittedSampleLocation, UsersSubmittedSamples);
        }
        /// <summary>
        /// add a sample to the stored samples list and saves the list to local storage
        /// </summary>
        /// <param name="sample">sample to save</param>
        public void AddAndSaveStoredSample(Sample sample)
        {
            AddToStoredSamples(sample);
            saveDataLogic.SaveSamples(_storedSampleLocation, UsersStoredSamples);
        }
        /// <summary>
        /// clears the stored samples and saves to local storage
        /// save submitted samples to local storage
        /// </summary>
        public void UpdateSubmittedStoredSamples()
        {
            ClearStoredSamplesList();
            saveDataLogic.SaveSamples(_storedSampleLocation, UsersStoredSamples);
            saveDataLogic.SaveSamples(_submittedSampleLocation, UsersSubmittedSamples);
        }
        /// <summary>
        /// clears the submitted samples list and saves it to local storage
        /// </summary>
        public void DeleteSubmittedSamplesFromDevice()
        {
            ClearSubmittedSamplesList();
            saveDataLogic.SaveSamples(_submittedSampleLocation, UsersSubmittedSamples);
        }

        /// <summary>
        /// Adds a sample to UsersSubmittedSamples
        /// </summary>
        /// <param name="sample">sample to add</param>
        public void AddToSubmittedSamples(Sample sample)
        {
            UsersSubmittedSamples.Add(sample);
        }
        /// <summary>
        /// Adds a sample to UsersStoredSamples
        /// </summary>
        /// <param name="sample">sample to add</param>
        public void AddToStoredSamples(Sample sample)
        {
            UsersStoredSamples.Add(sample);
        }
   
        /// <summary>
        /// clears the UsersStoredSamples list
        /// </summary>
        public void ClearStoredSamplesList()
        {
            UsersStoredSamples.Clear();
        }
        /// <summary>
        /// clears the UsersSubmittedSamples list
        /// </summary>
        public void ClearSubmittedSamplesList()
        {
            UsersSubmittedSamples.Clear();
        }


        #endregion
        #region "Profile"
        /// <summary>
        /// Saves the passed user to local storage
        /// </summary>
        /// <param name="user">user to save</param>
        public void SaveUserProfile(User user)
        {
            saveDataLogic.SaveUser(_userLocation, user);
        }
        /// <summary>
        /// aves the passed user to local storage
        /// if the firebase user is not null, adds the user to the firestore Users collection
        /// </summary>
        /// <param name="user"></param>
        /// <param name="firebaseUser"></param>
        public void SaveUserProfile(User user, FirebaseUser firebaseUser)
        {
            saveDataLogic.SaveUserProfile(_userLocation, user,firebaseUser);
        
        }
        /// <summary>
        /// Loads and return a USer from local storage file
        /// </summary>
        /// <returns></returns>
        public User LoadUserProfile()
        {
           return saveDataLogic.LoadUserProfile(_userLocation);
         
        }

        #endregion

#if UNITY_INCLUDE_TESTS
        public void SetSaveDataLogic()
        {
            saveDataLogic = new SaveDataLogic();
        }
#endif
    }
}