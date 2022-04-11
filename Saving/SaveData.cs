using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Firebase.Auth;
using Firebase.Firestore;
using System;
public class SaveData: MonoBehaviour
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
    private List<Sample> usersSubmittedSamples = new List<Sample>();
    private List<Sample> usersStoredSamples = new List<Sample>();
  //  private List<Sample> usersStoredSamples = new List<Sample>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        //we need to check teh file path exists first
        string filepath = Application.persistentDataPath + "/submittedSamplesSave.dat";
        if (System.IO.File.Exists(filepath))
        {
            LoadSubmittedSamples();
            LoadStoredSamples();
        }
    }
    //Could seperate into own scripts
    public void AddToSubmittedSamples(Sample sample)
    {
        usersSubmittedSamples.Add(sample);
    }
    public List<Sample> GetUserSubmittedSamples()
    {
        return this.usersSubmittedSamples;
    }
    public void AddToStoredSamples(Sample sample)
    {
        usersStoredSamples.Add(sample);
    }
    public List<Sample> GetUserStoredSamples()
    {
        return this.usersStoredSamples;
    }
    public void ClearStoredSamplesList()
    {
        usersStoredSamples.Clear();
    }
    public void ClearSubmittedSamplesList()
    {
        usersSubmittedSamples.Clear();
    }
    public void LoadSubmittedSamples()
    {
        string filepath = Application.persistentDataPath + "/submittedSamplesSave.dat";
        //string filepath = Application.persistentDataPath + "/save.dat";
        try { 
        using (FileStream file = File.Open(filepath, FileMode.Open))
        {
            object loadedData = new BinaryFormatter().Deserialize(file);
            List<Sample> saveData = (List<Sample>)loadedData;
            usersSubmittedSamples = saveData;
                //abbove could be more direct
        }
        }catch(Exception e)
        {
        }
    }
    /// <summary>
    /// Consider a delete stored / submitted samples from deivce
    /// </summary>
    public void SaveSubmittedSamples()
    {
        string filepath = Application.persistentDataPath + "/submittedSamplesSave.dat";
        using (FileStream file = File.Create(filepath))
        {
            new BinaryFormatter().Serialize(file, usersSubmittedSamples);
        }
    }
    public void LoadStoredSamples()
    {
        string filepath = Application.persistentDataPath + "/storedSamplesSave.dat";
        //string filepath = Application.persistentDataPath + "/save.dat";
        try { 
        using (FileStream file = File.Open(filepath, FileMode.Open))
        {
            object loadedData = new BinaryFormatter().Deserialize(file);
            List<Sample> saveData = (List<Sample>)loadedData;
            usersStoredSamples = saveData;
        }
    }catch(Exception e)
        {
        }
    }
    public void SaveStoredSamples()
    {
        string filepath = Application.persistentDataPath + "/storedSamplesSave.dat";
        using (FileStream file = File.Create(filepath))
        {
            new BinaryFormatter().Serialize(file, usersStoredSamples);
        }
    }
    public void SaveUserProfile(User user)
    {
        string filepath = Application.persistentDataPath + "/userSave.dat";
        using (FileStream file = File.Create(filepath))
        {
            new BinaryFormatter().Serialize(file, user);
        }
    }
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
            firestore.Collection("Users").Document(user.Email).SetAsync(user);
        }
        else
        {
        }
    }
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
}
