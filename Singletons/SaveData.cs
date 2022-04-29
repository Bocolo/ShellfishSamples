using Firebase.Auth;
using Samples.Data;
using System.Collections.Generic;
using UnityEngine;
using Users.Data;

namespace App.SaveSystem.Manager
{

    /// <summary>
    /// This Singleton manages all saves and retrievals 
    /// of data from local storage
    /// </summary>
    public class SaveData : MonoBehaviour
    {
        public static SaveData Instance { get; private set; }
        public List<Sample> UsersSubmittedSamples { get; private set; } = new List<Sample>();
        public List<Sample> UsersStoredSamples { get; private set; } = new List<Sample>();
        private SaveDataLogic saveDataLogic;
        private readonly string _storedSampleLocation = "/storedSamplesSave.dat";
        private readonly string _submittedSampleLocation = "/submittedSamplesSave.dat";
        private readonly string _userLocation = "/userSave.dat";
        /// <summary>
        /// Sets instance on awake
        /// Set stored and submitted samples lists
        /// creates SaveDataLogic object
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

        #region "Load"
        #region "Load Samples"
        /// <summary>
        ///  Loads the stored samples from local storage and saves to the UsersStoredSamples list
        ///  returns stored samples
        /// </summary>
        /// <returns> stored samples list</returns>
        public List<Sample> LoadAndGetStoredSamples()
        {
            UsersStoredSamples = saveDataLogic.LoadSamples(_storedSampleLocation);
            return this.UsersStoredSamples;
        }
        /// <summary>
        /// Loads the submitted samples from local storage and saves to the UsersSubmittedSamples list
        ///  returns submitted samples
        /// </summary>
        /// <returns> submitted samples list</returns>
        public List<Sample> LoadAndGetSubmittedSamples()
        {
            UsersSubmittedSamples = saveDataLogic.LoadSamples(_submittedSampleLocation);
            return this.UsersSubmittedSamples;
        }
        #endregion

        #region "Load User"
        /// <summary>
        /// Loads and return a User from local storage file
        /// </summary>
        /// <returns>a user</returns>
        public User LoadUserProfile()
        {
            return saveDataLogic.LoadUserProfile(_userLocation);

        }
        #endregion
        #endregion
        #region "Save"
        #region "Add and Save Samples"
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
        /// adds a sample to the stored samples list and saves the list to local storage
        /// </summary>
        /// <param name="sample">sample to save</param>
        public void AddAndSaveStoredSample(Sample sample)
        {
            AddToStoredSamples(sample);
            saveDataLogic.SaveSamples(_storedSampleLocation, UsersStoredSamples);
        }
        #endregion
        #region "Save Profile"
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
            saveDataLogic.SaveUserProfile(_userLocation, user, firebaseUser);

        }
        #endregion
        #endregion
        #region "Manage Lists"
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

#if UNITY_INCLUDE_TESTS
        public void SetSaveDataLogic()
        {
            saveDataLogic = new SaveDataLogic();
        }
#endif
    }
}