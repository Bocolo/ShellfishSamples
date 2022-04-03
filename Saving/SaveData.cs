using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Firebase.Auth;
using Firebase.Firestore;
public class SaveData: MonoBehaviour
{
    /*
     FIX INSTANCE BS -= SMLLA LARG PRIVATE PUBLIC
     */
    //private static SaveData _instance;
    public static SaveData Instance { get; private set; }
    private List<SampleData> usersSubmittedSamples = new List<SampleData>();
    private List<SampleData> usersStoredSamples = new List<SampleData>();

  //  private List<SampleData> usersStoredSamples = new List<SampleData>();
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
    public void AddToSubmittedSamples(SampleData sample)
    {
        usersSubmittedSamples.Add(sample);
    }
    public List<SampleData> GetUserSubmittedSamples()
    {
        return this.usersSubmittedSamples;
    }
    public void AddToStoredSamples(SampleData sample)
    {
        usersStoredSamples.Add(sample);
    }
    public List<SampleData> GetUserStoredSamples()
    {
        return this.usersStoredSamples;
    }



    public void ClearStoredSamples()
    {
        usersStoredSamples.Clear();
    }
    public void LoadSubmittedSamples()
    {
        Debug.Log("Load has been called");
        string filepath = Application.persistentDataPath + "/submittedSamplesSave.dat";
        //string filepath = Application.persistentDataPath + "/save.dat";

        using (FileStream file = File.Open(filepath, FileMode.Open))
        {
            Debug.Log("Loading");

            object loadedData = new BinaryFormatter().Deserialize(file);
            Debug.Log(loadedData +" obj");
            List<SampleData> saveData = (List<SampleData>)loadedData;
            Debug.Log(saveData +" --->save submitted data");
            for(int i=0; i < saveData.Count; i++)
            {
                Debug.Log(saveData[i].Species + " ___ " +i );
               // saveData[i].Species = "I changed you";
            }
            usersSubmittedSamples = saveData;
            Debug.Log("Saved data worked " + usersSubmittedSamples.Count); 
        }
    }
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

        using (FileStream file = File.Open(filepath, FileMode.Open))
        {
            Debug.Log("Loading");

            object loadedData = new BinaryFormatter().Deserialize(file);

            Debug.Log(loadedData + " obj");
            List<SampleData> saveData = (List<SampleData>)loadedData;
            Debug.Log(saveData + " --->s avedata");
            for (int i = 0; i < saveData.Count; i++)
            {
                Debug.Log(saveData[i].Species + " ___ " + i);
                // saveData[i].Species = "I changed you";
            }
            usersStoredSamples = saveData;
            Debug.Log("Saved data worked " + usersStoredSamples.Count);
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

    public void SaveUserProfile(UserData userProfile)
    {
        Debug.Log("user Profile saved");

        string filepath = Application.persistentDataPath + "/userProfileSave.dat";
        using (FileStream file = File.Create(filepath))
        {
            Debug.Log("user savinge");
            new BinaryFormatter().Serialize(file, userProfile);
            Debug.Log("user saved");

        }
    }
    public void SaveUserProfile(UserData userProfile, FirebaseUser firebaseUser)
    {
        Debug.Log("user Profile FIREBASE saved");

        string filepath = Application.persistentDataPath + "/userProfileSave.dat";
        using (FileStream file = File.Create(filepath))
        {
            Debug.Log("user savinge");
            new BinaryFormatter().Serialize(file, userProfile);
            Debug.Log("user saved");

        }
        if (firebaseUser != null)
        {
            var firestore = FirebaseFirestore.DefaultInstance;
            firestore.Collection("Users").Document(userProfile.Email).SetAsync(userProfile);
            Debug.Log("Successfully added user details to database");
        }
        else
        {
            Debug.Log("User not signed in - databsase users not updated");
        }
    }
    public UserData LoadUserProfile()
    {
        Debug.Log("Load user has been called");
        string filepath = Application.persistentDataPath + "/userProfileSave.dat";

        //string filepath = Application.persistentDataPath + "/save.dat";

        using (FileStream file = File.Open(filepath, FileMode.Open))
        {
            Debug.Log("Loading");

            object loadedData = new BinaryFormatter().Deserialize(file);
            Debug.Log(loadedData + " obj");
            UserData saveData = (UserData)loadedData;
            Debug.Log(saveData + " --->s ave user data");

            return saveData;
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
    private UserData userProfileData;
    public void LoadProfile()
    {
        userProfileData = SaveData.Instance.LoadUserProfile();
        _userName.text = userProfileData.Name;
        _company.text = userProfileData.Company;
    }

    public void CreateProfile()
    {
        UserData user = new UserData
        {
            Name = _userNameInput.text,
            Company = _companyInput.text,
        };
        SaveData.Instance.SaveUserProfile(user);
    }
}
*/