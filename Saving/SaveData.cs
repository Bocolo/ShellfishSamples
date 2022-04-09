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
        Debug.Log("Save Data Awake");
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            Debug.Log("Save Data Awake if");

        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            Debug.Log("Save Data Awake else");

        }
        //we need to check teh file path exists first
        string filepath = Application.persistentDataPath + "/submittedSamplesSave.dat";
        if (System.IO.File.Exists(filepath))
        {
            Debug.Log("File exists");
            LoadSubmittedSamples();
            LoadStoredSamples();
        }
        else
        {
            Debug.Log("File not exists");

        }
   
    }
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
        Debug.Log("Load has been called");
        string filepath = Application.persistentDataPath + "/submittedSamplesSave.dat";
        //string filepath = Application.persistentDataPath + "/save.dat";
        try { 
        using (FileStream file = File.Open(filepath, FileMode.Open))
        {
            Debug.Log("Loading");

            object loadedData = new BinaryFormatter().Deserialize(file);
            Debug.Log(loadedData +" obj");
            List<Sample> saveData = (List<Sample>)loadedData;
            Debug.Log(saveData + " --->save submitted data.--LoadSubmittedSamples-- .Count" + saveData.Count);
            for(int i=0; i < saveData.Count; i++)
            {
                Debug.Log(saveData[i].Species + " ___ " +i );
               // saveData[i].Species = "I changed you";
            }
            usersSubmittedSamples = saveData;
            Debug.Log(".--LoadSubmittedSamples-- .Saved data worked " + usersSubmittedSamples.Count+"\n\n"); 
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
        Debug.Log("Save has been called");

        string filepath = Application.persistentDataPath + "/submittedSamplesSave.dat";
        using (FileStream file = File.Create(filepath))
        {
            Debug.Log("savinge");
            new BinaryFormatter().Serialize(file, usersSubmittedSamples);
            Debug.Log("saved");

        }
    }
    public void LoadStoredSamples()
    {
        Debug.Log("Load has been called");
        string filepath = Application.persistentDataPath + "/storedSamplesSave.dat";
        //string filepath = Application.persistentDataPath + "/save.dat";
        try { 
        using (FileStream file = File.Open(filepath, FileMode.Open))
        {
            Debug.Log("Loading");

            object loadedData = new BinaryFormatter().Deserialize(file);

            Debug.Log(loadedData + " obj");
            List<Sample> saveData = (List<Sample>)loadedData;
            Debug.Log(saveData + " --->savedata is loaded.  count is "+saveData.Count);
            for (int i = 0; i < saveData.Count; i++)
            {
                Debug.Log(saveData[i].Species + " ___ " + i);
                // saveData[i].Species = "I changed you";
            }
            usersStoredSamples = saveData;
            Debug.Log("Saved data worked " + usersStoredSamples.Count);
        }
    }catch(Exception e)
        {

        }
    }
    public void SaveStoredSamples()
    {
        Debug.Log("Save has been called");

        string filepath = Application.persistentDataPath + "/storedSamplesSave.dat";
        using (FileStream file = File.Create(filepath))
        {
            Debug.Log("savinge");
            new BinaryFormatter().Serialize(file, usersStoredSamples);
            Debug.Log("saved");

        }
    }

    public void SaveUserProfile(User user)
    {
        Debug.Log("user Profile saved");

        string filepath = Application.persistentDataPath + "/userSave.dat";
        using (FileStream file = File.Create(filepath))
        {
            Debug.Log("user savinge");
            new BinaryFormatter().Serialize(file, user);
            Debug.Log("user saved");

        }
    }
    public void SaveUserProfile(User user, FirebaseUser firebaseUser)
    {
        Debug.Log("user Profile FIREBASE saved");

        string filepath = Application.persistentDataPath + "/userSave.dat";
        using (FileStream file = File.Create(filepath))
        {
            Debug.Log("user savinge");
            new BinaryFormatter().Serialize(file, user);
            Debug.Log("user saved");

        }
        if (firebaseUser != null)
        {
            var firestore = FirebaseFirestore.DefaultInstance;
            firestore.Collection("Users").Document(user.Email).SetAsync(user);
            Debug.Log("Successfully added user details to database");
        }
        else
        {
            Debug.Log("User not signed in - databsase users not updated");
        }
    }
    public User LoadUserProfile()
    {
        Debug.Log("Load user has been called");
        string filepath = Application.persistentDataPath + "/userSave.dat";

        //string filepath = Application.persistentDataPath + "/save.dat";

        using (FileStream file = File.Open(filepath, FileMode.Open))
        {
            Debug.Log("Loading User");

            object loadedData = new BinaryFormatter().Deserialize(file);
            Debug.Log(loadedData + " User");
            User userData = (User)loadedData;
            Debug.Log(userData + " ---> user data");

            return userData;
        }
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UserProfile : MonoBehaviour
{
    [SerializeField] private TMP_Text _userName;
    [SerializeField] private TMP_Text _company;
    [SerializeField] private TMP_Text _dateJoined;
    [SerializeField] private TMP_InputField _userNameInput;
    [SerializeField] private TMP_InputField _companyInput;
    private User user;
    public void LoadProfile()
    {
        user = SaveData.Instance.LoadUserProfile();
        _userName.text = user.Name;
        _company.text = user.Company;
    }

    public void CreateProfile()
    {
        User user = new User
        {
            Name = _userNameInput.text,
            Company = _companyInput.text,
        };
        SaveData.Instance.SaveUserProfile(user);
    }
}
*/