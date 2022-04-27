using Firebase.Auth;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
namespace Save.Logic { 
    public class SaveDataLogic 
    {

        public List<Sample> LoadSamples(String filename)                                   //?? would this updatethisinstancesampels
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
        public void SaveSamples(String filename, List<Sample> samples)
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
        public void SaveUser(String filename, User user)
        {
            string filepath = Application.persistentDataPath + filename;
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
        public void SaveUserProfile(string filename,User user, FirebaseUser firebaseUser)
        {
            SaveUser(filename, user);
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
        public User LoadUserProfile(string filename)
        {
            string filepath = Application.persistentDataPath + filename;
            //string filepath = Application.persistentDataPath + "/save.dat";
            using (FileStream file = File.Open(filepath, FileMode.Open))
            {
                object loadedData = new BinaryFormatter().Deserialize(file);
                User userData = (User)loadedData;
                return userData;
            }
        }
    }
}