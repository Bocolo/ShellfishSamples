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
        //private static SaveData _instance;
        public static SaveData Instance { get; private set; }
        public List<Sample> UsersSubmittedSamples { get; private set; } = new List<Sample>();
        public List<Sample> UsersStoredSamples { get; private set; } = new List<Sample>();
        //  private List<Sample> usersStoredSamples = new List<Sample>();

        /// <summary>
        /// Sets instance on awake
        /// 
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
               // DontDestroyOnLoad(this.gameObject);//this needs to be fixed? m\ybe dont need this
            }
            //we need to check teh file path exists first
            //Loads submitted and stored samples on awake if the file path exists
                  string filepath = Application.persistentDataPath + "/submittedSamplesSave.dat";///////////////what
                  if (File.Exists(filepath))
                  {
                      LoadSubmittedSamples();
                      LoadStoredSamples();
                  }///review the add and save methods
        }







        /// <summary>
        /// Loads and returns stored samples
        /// </summary>
        /// <returns></returns>
        public List<Sample> LoadAndGetStoredSamples()
        {
            LoadStoredSamples();
            return this.UsersStoredSamples;
        }
        /// <summary>
        /// Loads and returns stubmitted samples
        /// </summary>
        /// <returns></returns>
        public List<Sample> LoadAndGetSubmittedSamples()
        {
            LoadSubmittedSamples();
            return this.UsersSubmittedSamples;
        }
     /// <summary>
     /// Saves the passed user to local storage
     /// </summary>
     /// <param name="user">user to save</param>
        public void SaveUserProfile(User user)
        {
            string filepath = Application.persistentDataPath + "/userSave.dat";
            using (FileStream file = File.Create(filepath))
            {
                new BinaryFormatter().Serialize(file, user);
            }
        }
        /// <summary>
        /// aves the passed user to local storage
        /// if the firebase user is not null, adds the user to the firestore Users collection
        /// </summary>
        /// <param name="user"></param>
        /// <param name="firebaseUser"></param>
        public void SaveUserProfile(User user, FirebaseUser firebaseUser)
        {
            string filepath = Application.persistentDataPath + "/userSave.dat";
            using (FileStream file = File.Create(filepath))
            {
                new BinaryFormatter().Serialize(file, user);
            }
            if (firebaseUser != null)
            {
                var firestore = FirebaseFirestore.DefaultInstance;
                firestore.Collection("Users").Document(user.Email).SetAsync(user);///userdao? //set or updates
            }
           
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
        /// Loads and return a USer from local storage file
        /// </summary>
        /// <returns></returns>
        public User LoadUserProfile()
        {
            string filepath = Application.persistentDataPath + "/userSave.dat";
            //string filepath = Application.persistentDataPath + "/save.dat";
            using (FileStream file = File.Open(filepath, FileMode.Open))
            {
                object loadedData = new BinaryFormatter().Deserialize(file);
                User userData = (User)loadedData;
                return userData;
            }
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
        /// clears the stored samples and saves to local storage
        /// save submitted samples to local storage
        /// </summary>
        public void UpdateSubmittedStoredSamples()
        {
            ClearStoredSamplesList();
            SaveStoredSamples();
            SaveSubmittedSamples();
        }
        //Could seperate into own scripts
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


        /// <summary>
        /// Loads the submitted samples from local storage and saves to the UsersSubmittedSamples lisr
        /// </summary>
        private void LoadSubmittedSamples()
        {
            string filepath = Application.persistentDataPath + "/submittedSamplesSave.dat";
            //string filepath = Application.persistentDataPath + "/save.dat";
            try
            {
                using (FileStream file = File.Open(filepath, FileMode.Open))
                {
                    object loadedData = new BinaryFormatter().Deserialize(file);
                    List<Sample> saveData = (List<Sample>)loadedData;
                    UsersSubmittedSamples = saveData;
                    //abbove could be more direct
                }
            }
            catch (Exception e)
            {
                Debug.LogError("LoadSubmittedSamples: " + e.StackTrace);
            }
            //    usersSubmittedSamples = LoadSamples("/submittedSamplesSave.dat");
        }
        /// <summary>
        /// saves the UsersSubmittedSamples to local storage
        /// </summary>
        private void SaveSubmittedSamples()
        {
            string filepath = Application.persistentDataPath + "/submittedSamplesSave.dat";
            using (FileStream file = File.Create(filepath))
            {
                new BinaryFormatter().Serialize(file, UsersSubmittedSamples);
            }
            //    SaveSamples("/submittedSamplesSave.dat", usersSubmittedSamples);
        }
        /// <summary>
        /// Loads the stored samples from local storage and saves to the UsersStoredSamples lisr
        /// </summary>
        private void LoadStoredSamples()
        {
            string filepath = Application.persistentDataPath + "/storedSamplesSave.dat";
            //string filepath = Application.persistentDataPath + "/save.dat";
            try
            {
                using (FileStream file = File.Open(filepath, FileMode.Open))
                {
                    object loadedData = new BinaryFormatter().Deserialize(file);
                    List<Sample> saveData = (List<Sample>)loadedData;
                    UsersStoredSamples = saveData;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("LoadStoredSamples: " + e.StackTrace);

            }
            //   usersStoredSamples = LoadSamples("/storedSamplesSave.dat");
        }





        /// <summary>
        /// loads and returns a list of samples from the passed filename
        /// </summary>
        /// <param name="filename">location to load sammples from </param>
        /// <returns></returns>
        private List<Sample> LoadSamples(String filename)//?? would this updatethisinstancesampels
        {
            string filepath = Application.persistentDataPath + filename;
            //string filepath = Application.persistentDataPath + "/save.dat";
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
                Debug.LogError("LoadSamples: " + e.StackTrace);

                return null;
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
        /// saves the UsersStoredSamples list to local storage
        /// </summary>
        private void SaveStoredSamples()
        {
            string filepath = Application.persistentDataPath + "/storedSamplesSave.dat";
            using (FileStream file = File.Create(filepath))
            {
                new BinaryFormatter().Serialize(file, UsersStoredSamples);
            }
            //   SaveSamples("/storedSamplesSave.dat", usersStoredSamples);
        }
        /// <summary>
        /// saves the passedd User to local storage. the save location is the passed filename
        /// </summary>
        /// <param name="filename">location to save</param>
        /// <param name="user">user to save</param>
        private void SaveProfile(String filename, User user)
        {
            string filepath = Application.persistentDataPath + filename;
            using (FileStream file = File.Create(filepath))
            {
                new BinaryFormatter().Serialize(file, user);
            }
        }
    }
}
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
    //    SaveSamples("/submittedSamplesSave.dat", usersSubmittedSamples);
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
    //   usersStoredSamples = LoadSamples("/storedSamplesSave.dat");
}
public void AddToFullSamples(Sample sample)
{
    AllSamples.Add(sample);
}
//////////
///*/