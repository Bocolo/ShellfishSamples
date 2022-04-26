using Firebase.Auth;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
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
            UsersStoredSamples = LoadSamples(_storedSampleLocation);
            UsersSubmittedSamples = LoadSamples(_submittedSampleLocation);
        }
        #region "Samples"
        /// <summary>
        ///  Loads the stored samples from local storage and saves to the UsersStoredSamples lisr
        ///  returns stored samples
        /// </summary>
        /// <returns></returns>
        public List<Sample> LoadAndGetStoredSamples()
        {
            UsersStoredSamples = LoadSamples(_storedSampleLocation);
            return this.UsersStoredSamples;
        }
        /// <summary>
        /// Loads the submitted samples from local storage and saves to the UsersSubmittedSamples list
        ///  returns stubmitted samples
        /// </summary>
        /// <returns></returns>
        public List<Sample> LoadAndGetSubmittedSamples()
        {
            UsersSubmittedSamples = LoadSamples(_submittedSampleLocation);
            return this.UsersSubmittedSamples;
        }
        /// <summary>
        /// add a sample to the submitted samples list and saves the list to local storage
        /// </summary>
        /// <param name="sample">sample to save</param>
        public void AddAndSaveSubmittedSample(Sample sample)
        {
            AddToSubmittedSamples(sample);
            SaveSubmittedSamples();
        }
        /// <summary>
        /// add a sample to the stored samples list and saves the list to local storage
        /// </summary>
        /// <param name="sample">sample to save</param>
        public void AddAndSaveStoredSample(Sample sample)
        {
            AddToStoredSamples(sample);
            SaveStoredSamples();
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
        /// clears the stored samples and saves to local storage
        /// save submitted samples to local storage
        /// </summary>
        public void UpdateSubmittedStoredSamples()
        {
            ClearStoredSamplesList();
            SaveStoredSamples();
            SaveSubmittedSamples();
        }
        /// <summary>
        /// clears the submitted samples list and saves it to local storage
        /// </summary>
        public void DeleteSubmittedSamplesFromDevice()
        {
            ClearSubmittedSamplesList();
            SaveSubmittedSamples();
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
        /// <summary>
        /// saves the UsersSubmittedSamples to local storage
        /// </summary>
        private void SaveSubmittedSamples()
        {
            Debug.Log("Submitted asamples count " + UsersSubmittedSamples.Count);
            SaveSamples(_submittedSampleLocation, UsersSubmittedSamples);
        }
        /// <summary>
        /// saves the UsersStoredSamples list to local storage
        /// </summary>
        private void SaveStoredSamples()
        {
            SaveSamples(_storedSampleLocation, UsersStoredSamples);
        }
        #endregion
        #region "Profile"
        /// <summary>
        /// Saves the passed user to local storage
        /// </summary>
        /// <param name="user">user to save</param>
        public void SaveUserProfile(User user)
        {
            SaveUser(_userLocation, user);
        }
        /// <summary>
        /// aves the passed user to local storage
        /// if the firebase user is not null, adds the user to the firestore Users collection
        /// </summary>
        /// <param name="user"></param>
        /// <param name="firebaseUser"></param>
        public void SaveUserProfile(User user, FirebaseUser firebaseUser)
        {
            SaveUser(_userLocation, user);
            if (firebaseUser != null)
            {
                var firestore = FirebaseFirestore.DefaultInstance;
                firestore.Collection("Users").Document(user.Email).SetAsync(user);                      ///userdao? //set or updates
            }
        }
        /// <summary>
        /// Loads and return a USer from local storage file
        /// </summary>
        /// <returns></returns>
        public User LoadUserProfile()
        {
            string filepath = Application.persistentDataPath + _userLocation;
                                                                                                //string filepath = Application.persistentDataPath + "/save.dat";
            using (FileStream file = File.Open(filepath, FileMode.Open))
            {
                object loadedData = new BinaryFormatter().Deserialize(file);
                User userData = (User)loadedData;
                return userData;
            }
        }
        #endregion
        #region "Generic"
        /// <summary>
        /// loads and returns a list of samples from the passed filename
        /// </summary>
        /// <param name="filename">location to load sammples from </param>
        /// <returns></returns>
        private List<Sample> LoadSamples(String filename)                                   //?? would this updatethisinstancesampels
        {
            string filepath = Application.persistentDataPath + filename;
            try
            {
                using (FileStream file = File.Open(filepath, FileMode.Open))
                {
                    object loadedData = new BinaryFormatter().Deserialize(file);
                    List<Sample> saveData = (List<Sample>)loadedData;
                    return saveData;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("LoadSamples Error: " + e.StackTrace);
                return new List<Sample>(); 
            }
        }
        /// <summary>
        /// saves a list of samples to local storage at the passed filename location
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="samples"></param>
        private void SaveSamples(String filename, List<Sample> samples)
        {
            string filepath = Application.persistentDataPath + filename;
            using (FileStream file = File.Create(filepath))
            {
                new BinaryFormatter().Serialize(file, samples);
            }
        }
        /// <summary>
        /// saves the passedd User to local storage. the save location is the passed filename
        /// </summary>
        /// <param name="filename">location to save</param>
        /// <param name="user">user to save</param>
        private void SaveUser(String filename, User user)
        {
            string filepath = Application.persistentDataPath + filename;
            using (FileStream file = File.Create(filepath))
            {
                new BinaryFormatter().Serialize(file, user);
            }
        }
        #endregion
    }
}
/*     string filepath = Application.persistentDataPath + _userLocation;
            using (FileStream file = File.Create(filepath))
            {
                new BinaryFormatter().Serialize(file, user);
            }*/
/*    string filepath = Application.persistentDataPath + _userLocation;
        using (FileStream file = File.Create(filepath))
        {
            new BinaryFormatter().Serialize(file, user);
        }*/
/*
/// <summary>
/// ////////
/// </summary>
/// <returns></returns>
public List<Sample> AllSamples { get; private set; } = new List<Sample>();
/// 
public void SaveFullData()
{
    string filepath = Application.persistentDataPath + "/allSamples.dat";
    using (FileStream file = File.Create(filepath))
    {
        new BinaryFormatter().Serialize(file, AllSamples);
        Debug.Log("saved");
    }
    //    SaveSamples(_submittedSampleLocation, usersSubmittedSamples);
}
public void LoadFullData()
{
    string filepath = Application.persistentDataPath + "/allSamples.dat";
    //string filepath = Application.persistentDataPath + "/save.dat";
    try
    {
        using (FileStream file = File.Open(filepath, FileMode.Open))
        {
            object loadedData = new BinaryFormatter().Deserialize(file);
            List<Sample> saveData = (List<Sample>)loadedData;
            AllSamples = saveData;
        }
    }
    catch (Exception e)
    {
    }
    //   usersStoredSamples = LoadSamples(_storedSampleLocation);
}
public void AddToFullSamples(Sample sample)
{
    AllSamples.Add(sample);
}
//////////
///*/