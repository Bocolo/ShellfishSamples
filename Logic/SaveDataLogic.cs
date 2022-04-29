using Firebase.Auth;
using Firebase.Firestore;
using Samples.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Users.Data;

namespace App.SaveSystem
{

    public class SaveDataLogic
    {
        #region "Save functions"
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
        /// saves the passed user to local storage
        /// if the firebase user is not null, adds the user to the firestore Users collection
        /// </summary>
        /// <param name="user">user to save</param>
        /// <param name="firebaseUser">fire base user to save</param>
        public void SaveUserProfile(string filename, User user, FirebaseUser firebaseUser)
        {
            SaveUser(filename, user);
            if (firebaseUser != null)
            {
                var firestore = FirebaseFirestore.DefaultInstance;
                firestore.Collection("Users").Document(user.Email).SetAsync(user);
            }
        }
        #endregion
        #region "Load functions"
        /// <summary>
        /// Loads and return a User from local storage file
        /// </summary>
        /// <returns>the user loaded from storage</returns>
        public User LoadUserProfile(string filename)
        {
            string filepath = Application.persistentDataPath + filename;
            using (FileStream file = File.Open(filepath, FileMode.Open))
            {
                object loadedData = new BinaryFormatter().Deserialize(file);
                User userData = (User)loadedData;
                return userData;
            }
        }
        /// <summary>
        /// uses the filename location to retrieve
        /// a list of samples from local storage 
        /// </summary>
        /// <param name="filename">name of the file to open</param>
        /// <returns>a list of samples</returns>
        public List<Sample> LoadSamples(String filename)
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
                Debug.Log("LoadSamples Error: " + e.StackTrace);
                return new List<Sample>();
            }
        }
        #endregion
    }
}
