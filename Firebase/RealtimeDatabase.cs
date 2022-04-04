/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json;


public class RealtimeDatabase : MonoBehaviour
{

    DatabaseReference reference;
    [SerializeField] InputField nname;
    [SerializeField] InputField sampleDate;
    [SerializeField] Dropdown productionWk;
    [SerializeField] Dropdown iceRectangle;
    [SerializeField] Dropdown sampleLocationName;
    [SerializeField] Dropdown species;
    [SerializeField] InputField comments;
    [SerializeField] InputField company;

    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        //  MyDict();
       // JsonTest();
    }

    /// <summary>
    /// 
    /// </summary>
    /// 
    *//*
    public void SaveData()
    {
        Sample sample = new Sample();
        sample.species = species.options[species.value].text;
       // sample.productionWeekNo = productionWk.;
        sample.icesRectangleNo = iceRectangle.options[iceRectangle.value].text;
        sample.company = company.text;
        sample.date = sampleDate.text;
        sample.name = name.text;
        string json = JsonUtility.ToJson(sample);
        reference.Child("Sample").SetRawJsonValueAsync(json).ContinueWith(task =>{ 
            if(task.IsCompleted)
            {
                Debug.Log("It worked");
            }
            else
            {
                Debug.Log("we failed");
            }
        });

    }*//*
    public void SaveData()
    {
        Sample sample = new Sample();
        sample.species = "test fish";
        sample.productionWeekNo = 34;
        sample.icesRectangleNo = "34-_bE";
        sample.company = "compnay";
        sample.date = "2022-03-12";
        sample.name = "bob billy";
        string json = JsonUtility.ToJson(sample);
        for (int i = 0; i < 4; i++) {
            reference.Child("Sample" + i).SetRawJsonValueAsync(json).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("It worked");
                }
                else
                {
                    Debug.Log("we failed");
                }
            });
        }
            ;

    }
    public void JsonTest()
    {
        string data = "[{\"Species\":\"fish\",\"Production Week No\": \"34\"}]";

        string json = JsonUtility.ToJson(data);
        //JsonObjectAttribute jj;
        reference.Child("data").SetRawJsonValueAsync(json).ContinueWith(task => {
            if (task.IsCompleted)
            {
                Debug.Log("It worked22");
            }
            else
            {
                Debug.Log("we failed22");
            }
        });
    }
    public void MyDict()
    {
        Dictionary<string, object> myD = new Dictionary<string, object>();
        myD["Species"] = "fish";
        myD["Production Week No"] = 45;
        myD["Company"] = "a compant";
        Debug.Log(Serialize(myD));

    }
    public  string Serialize(Dictionary<string, object> data )
    {
        var serializer = new DataContractJsonSerializer(data.GetType());
        var ms = new MemoryStream();
        serializer.WriteObject(ms, data);

        return Encoding.UTF8.GetString(ms.ToArray());
    }
    public void SaveData2()
    {
        Sample sample = new Sample();
        
        sample.species = "test fish";
     //   sample.productionWeek = 34;
       // sample.ices = "34-_bE";
        string json = JsonUtility.ToJson(sample);
        reference.Child("Sample").SetRawJsonValueAsync(json).ContinueWith(task => {
            if (task.IsCompleted)
            {
                Debug.Log("It worked");
            }
            else
            {
                Debug.Log("we failed");
            }
        });

    }
    //  reference.Child("Sample").Child(sample.species).SetRawJsonValueAsync(json).ContinueWith(task =>{ 
    //  reference.Child("Sample").Child(nametoRead.text).GetValueAsync().ContinueWith(task 
    
   
    this read is for the realtime database, not firestore
   accesses whatever child string is passed
   //if snapshot.child(eg. ices) not a recognised field name, the rest 
   of the if block will not execute.
    *//*
    public void ReadData()
    {
           reference.Child("Sample0").GetValueAsync().ContinueWith(task =>
        //running test different path
    //    reference.Child("sample_test/one_sample").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log(" read Success");
                DataSnapshot snapshot = task.Result;
                Debug.Log(" read Success_________________" +snapshot);
            //    Debug.Log(" read Success_________________" + snapshot.Child());
                // data.text = snapshot.Child("S")
             //   Debug.Log(snapshot.Child("Species").Value.ToString());
               
                //changed to reflect mew patj --- test
                  Debug.Log(snapshot.Child("species").Value.ToString());
                Debug.Log(snapshot.ToString() + "test1");
               // Debug.Log(snapshot.Child("ices").Value.ToString() + "  __");
                Debug.Log(snapshot.ToString()+ "test");
                Debug.Log(snapshot);
               
                Debug.Log("Test");
            }
            else
            {
                Debug.Log(" read failed");
            }
        });
    }

}
*/