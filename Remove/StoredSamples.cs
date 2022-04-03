using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.Assertions;
using System;

public class StoredSamples : MonoBehaviour
{
    //https://www.youtube.com/watch?v=b5h1bVGhuRk&t=276s
    [SerializeField] private string _samplePath = "sample_test/one_sample";
    [SerializeField] private GameObject _bluePanelPrefab;
    [SerializeField] private GameObject _redPanelPrefab;
    [SerializeField] private Transform _contentParent;
    //private GameObject _contentCopy;

    //private ListenerRegistration _listenerRegistration;

    //remove at some point
    private Dictionary<string, object> collection;
    private List<SampleData> collectionSamples;

    //this works prints to name_text _ automatically or with getdat button
    //Allows cached data and auto retrieval
    private void Start()
    {
  
      //  ShowSubmittedSample();
    }
 
    public void SubmitStoredData()
    {
        try
        {
            //will submit when internet reconnects, app is opened -- but requires - firebase asccess first
            var firestore = FirebaseFirestore.DefaultInstance;
            List<SampleData> storedSamples = SaveData.Instance.GetUserStoredSamples();

            for (int i = 0; i < storedSamples.Count; i++)
            {
                firestore.Collection("Samples").Document(i.ToString()).SetAsync(storedSamples[i]);
                Debug.Log("Successfully saved stored sample " + i);
                SaveData.Instance.AddToSubmittedSamples(storedSamples[i]);
                Debug.Log("Successfully added stored sample to submitted samples list:   " + i);

            }
            //  SubmitSampleData.savedSamples.Clear();
            SaveData.Instance.ClearStoredSamples();
            Debug.Log("Cleared saved samples list ");
            SaveData.Instance.SaveStoredSamples();
            Debug.Log("Saved empty data to stored list");
            SaveData.Instance.SaveSubmittedSamples();
            Debug.Log("Saved data to submitte lise");

        }
        catch (Exception e)
        {
            Debug.LogError(e + " submitting stored data error");
        }
    }

    //gotta remove this~??
    public void ShowSubmittedSample()
    {
        SaveData.Instance.LoadStoredSamples();//MAYBE???
        Debug.Log("STARTTT _________");
        AddTextAndPrefab(SaveData.Instance.GetUserSubmittedSamples()); //THIS WAS STORED?>?
        Debug.Log("TTEESTT");
       // SetSampleData.savedSamples;
    }

    public void AddTextAndPrefab(List<SampleData> sampleList)
    {
        Debug.Log("__ int stored tect prefabe");
        //clear9ingh any previously loaded panels
        foreach(Transform child in _contentParent.transform)
        {
            Destroy(child.gameObject);
        }
        //_contentParent.transform.
        for (int i = 0; i < sampleList.Count; i++)
        {
            Debug.Log("__ int stored tect prefabe--loop");

            GameObject panel;
            if (i % 2 == 0)
            {
                panel = Instantiate(_bluePanelPrefab);
                Debug.Log("__ int stored tect prefabe--loop if");

            }
            else
            {
                Debug.Log("__ int stored tect prefabe--loop else");

                panel = Instantiate(_redPanelPrefab);
            }
            panel.transform.SetParent(_contentParent.transform);
            GameObject panelChild = panel.transform.GetChild(0).gameObject;
            Text panelText = panel.transform.GetChild(0).gameObject.GetComponent<Text>();
            panel.transform.localScale = new Vector3(1, 1, 1);
            panelText.text = SampleDataToString(sampleList[i], false);
            Debug.Log("__ int stored tect prefabe--last");

        }
    }
  
    public void GetAndSetUserData(String username)
    {
        foreach (Transform t in _contentParent)
        {
            Destroy(t.gameObject);
        }
        //  firestore.Collection("Samples").Document().SetAsync(sampleData);
        var firestore = FirebaseFirestore.DefaultInstance;

        collectionSamples.Clear();
        CollectionReference samplesReference = firestore.Collection("Samples");
        //Query testQuery2 = samplesReferenve.wher;
        Query testQuery = samplesReference.WhereEqualTo("Name", username); //better option
        testQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>


        // firestore.Collection("Samples").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);
            /* Debug.Log(task.Result.Documents.ToString());
             task.Result*/
            //https://firebase.google.com/docs/firestore/query-data/get-data#unity_5
            QuerySnapshot collectionSnapshot = task.Result;
            int counter = 0;
            int counter2 = 0;
            String samplesString = "";
            //when using full data set going to have to do limited loop
            //may create specieal method - e.g. have permission accedd full
            //no permission access
            foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
            {
                //prob dont need tryu block here
                try
                {
                    SampleData sample = documentSnapshot.ConvertTo<SampleData>();



                    counter++;
                    collectionSamples.Add(sample);
                    //   AddTextAndPrefab(sample, counter);

                    //       samplesString += (SampleDataToStringRestricted(sample) + "\n\n");
                    //      Debug.Log("Name match-- " + counter);

                }
                catch (Exception e)
                {
                    Debug.Log(e + " CAUGHT");
                    counter2++;
                }
            }
            AddTextAndPrefab(collectionSamples);
            //SettingText
            //_text.text = total;
            // SetDataText(collectionSamples);
            //  _text.text = samplesString;
            Debug.Log("Try : " + counter + ".  Caught : " + counter2);
        });
    }

    //Return sample data with ONLY the location thats filled


    public String SampleDataToString(SampleData sample, bool isRestricted)
    {
        if (isRestricted)
        {
            if (sample.SampleLocationName == null)
            {
                Debug.Log("Location is null restricted");

                return ("\nSpecies: " + sample.Species
               + $"\nICEs Rectangle: {sample.IcesRectangleNo}"
               + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date);
            }
            else
            {
                Debug.Log("ICES rectangle is null restriced");
                return ("\nSpecies: " + sample.Species
                + "\nLocation: " + sample.SampleLocationName + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date);
            }
        }
        else
        {
            if (sample.SampleLocationName == null)
            {
                Debug.Log("Location is null");

                return ("Name: " + sample.Name + "\nCompany: " + sample.Company + "\nSpecies: " + sample.Species
               + $"\nICEs Rectangle: {sample.IcesRectangleNo}"
               + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date + "\nComment: " + sample.Comment);
            }
            else
            {
                Debug.Log("ICES rectangle is null");
                return ("Name: " + sample.Name + "\nCompany: " + sample.Company + "\nSpecies: " + sample.Species
                + "\nLocation: " + sample.SampleLocationName + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date
                + "\nComment: " + sample.Comment);
            }
        }
    }
    //void saveAll()
    //{



}





//public SampleData GetData(String _path)
//{
//    var firestore = FirebaseFirestore.DefaultInstance;
//    firestore.Document(_path).GetSnapshotAsync().ContinueWithOnMainThread(task =>
//    {
//        Assert.IsNull(task.Exception);
//        var sampleData = task.Result.ConvertTo<SampleData>();
//        //  var limitedSampleData = task.Result.ConvertTo<LimitedSampleData>();
//        // SetDataText(sampleData);
//        return sampleData;
//    });
//    //FIX FALLOUT FROM THIS __ DONT WANT RETURN empty__ CANT RETURN NULL
//    return new SampleData();
//}
//public void getAndOrderLimitData()
//{
//    //SLOW
//    foreach (Transform t in _contentParent)
//    {
//        Destroy(t.gameObject);
//    }
//    // _contentParent = _contentCopy;
//    var firestore = FirebaseFirestore.DefaultInstance;
//    //  CollectionReference samplesReferenve =firestore.Collection("SamplesFull");
//    CollectionReference samplesReferenve = firestore.Collection("Samples");
//    //Query testQuery2 = samplesReferenve.wher;
//    Query testQuery = samplesReferenve.OrderBy("Name").Limit(3);
//    collectionSamples.Clear();
//    testQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
//    {
//        Assert.IsNull(task.Exception);//WHY
//        /* Debug.Log(task.Result.Documents.ToString());
//         task.Result*/
//        //https://firebase.google.com/docs/firestore/query-data/get-data#unity_5
//        QuerySnapshot collectionSnapshot = task.Result;
//        int counter = 0;
//        int counter2 = 0;
//        String samplesString = "";
//        //when using full data set going to have to do limited loop
//        //may create specieal method - e.g. have permission accedd full
//        //no permission access
//        foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
//        {
//            //prob dont need tryu block here
//            try
//            {
//                SampleData sample = documentSnapshot.ConvertTo<SampleData>();

//                counter++;
//                collectionSamples.Add(sample);

//                //      AddTextAndPrefab(sample, counter);


//            }
//            catch (Exception e)
//            {
//                Debug.Log(e + " CAUGHT");
//                counter2++;
//            }
//        }
//        AddTextAndPrefab(collectionSamples);
//        Debug.Log("Try : " + counter + ".  Caught : " + counter2);
//    });
//}

//public void AddTextAndPrefab(SampleData sample)
//{
//    //maybe add isBluee bool
//    GameObject panel;
//    panel = Instantiate(_bluePanelPrefab);

//    panel = Instantiate(_redPanelPrefab);
//    panel.transform.SetParent(_contentParent.transform);
//    //      blue.transform.parent = _contentParent.transform;
//    GameObject panelChild = panel.transform.GetChild(0).gameObject;
//    //   Text panelText = panelChild.GetComponent<Text>();

//    Text panelText = panel.transform.GetChild(0).gameObject.GetComponent<Text>();

//    //_contenParent.AddComponent
//    panel.transform.localScale = new Vector3(1, 1, 1);

//    panelText.text = SampleDataToString(sample, false);
//}








//}
//public void jsonTest()
//{
//    SampleData sample1 = new SampleData();
//    sample1.Species = "this";
//    sample1.Comment = "that";
//    sample1.ProductionWeekNo = 1;
//    sample1.IcesRectangleNo = "fdf";
//    sample1.Company = "dsad";
//    sample1.Date = "fdsf";
//    sample1.Name = "";
//    sample1.SampleLocationName = "ff";
//    String tt= JsonUtility.ToJson(sample1);
//    //var tt = JsonUtility.ToJson(new SampleData
//    //{
//    //    Species = "ttttt",
//    //    IcesRectangleNo = "ttttt",
//    //    //_iceRectangle.options[_iceRectangle.value].text,
//    //    Company = "ttttt",
//    //    Date = "ttttt",//_sampleDateString,
//    //    Name = "ttttt",
//    //    ProductionWeekNo = 2,
//    //    SampleLocationName = "ttttt",
//    //    Comment = "ttttt"

//    //});
//    Debug.Log((tt));
//    SampleData s2 = JsonUtility.FromJson<SampleData>(tt);
//    Debug.Log(s2.SampleLocationName);
//    Debug.Log(JsonUtility.FromJson<SampleData>(tt) +"____ test json");
//}

//public void CollectionToText()
//{
//    foreach (Transform t in _contentParent)
//    {
//        Destroy(t.gameObject);
//    }
//    //  firestore.Collection("Samples").Document().SetAsync(sampleData);
//    var firestore = FirebaseFirestore.DefaultInstance;
//    firestore.Collection("Samples").GetSnapshotAsync().ContinueWithOnMainThread(task =>
//    {
//        Assert.IsNull(task.Exception);
//        /* Debug.Log(task.Result.Documents.ToString());
//         task.Result*/
//        //https://firebase.google.com/docs/firestore/query-data/get-data#unity_5
//        QuerySnapshot collectionSnapshot = task.Result;
//        int counter = 0;
//        int counter2 = 0;
//        String samplesString = "";
//        //when using full data set going to have to do limited loop
//        //may create specieal method - e.g. have permission accedd full
//        //no permission access
//        foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
//        {
//            //prob dont need tryu block here
//            try
//            {
//                SampleData sample = documentSnapshot.ConvertTo<SampleData>();
//                counter++;
//                collectionSamples.Add(sample);
//                //  samplesString += (SampleDataToStringRestricted(sample) + "\n\n");
//                //    AddTextAndPrefab(sample, counter);
//            }
//            catch (Exception e)
//            {
//                Debug.Log(e + " CAUGHT");
//                counter2++;
//            }
//        }
//        AddTextAndPrefab(collectionSamples);
//        //  _text.text = samplesString;
//        //  SetDataText(collectionSamples);
//        Debug.Log("Try : " + counter + ".  Caught : " + counter2);
//    });
//}
//public void CollectionToText(String collectionName)
//{
//    foreach (Transform t in _contentParent)
//    {
//        Destroy(t.gameObject);
//    }
//    //  firestore.Collection("Samples").Document().SetAsync(sampleData);
//    var firestore = FirebaseFirestore.DefaultInstance;

//    firestore.Collection(collectionName).GetSnapshotAsync().ContinueWithOnMainThread(task =>
//    {
//        Assert.IsNull(task.Exception);
//        //https://firebase.google.com/docs/firestore/query-data/get-data#unity_5
//        QuerySnapshot collectionSnapshot = task.Result;
//        int counter = 0;
//        int counter2 = 0;
//        String samplesString = "";
//        //when using full data set going to have to do limited loop
//        //may create specieal method - e.g. have permission accedd full
//        //no permission access
//        foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
//        {
//            //prob dont need tryu block here
//            try
//            {
//                SampleData sample = documentSnapshot.ConvertTo<SampleData>();

//                counter++;
//                collectionSamples.Add(sample);
//                //  samplesString += (SampleDataToStringRestricted(sample) + "\n\n");                }
//                //   AddTextAndPrefab(sample, counter);
//            }
//            catch (Exception e)
//            {
//                Debug.Log(e + " CAUGHT");
//                counter2++;
//            }
//        }
//        AddTextAndPrefab(collectionSamples);
//        //   _text.text = samplesString;
//        //  SetDataText(collectionSamples);
//    });
//}
//not needed
