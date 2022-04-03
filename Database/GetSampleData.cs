using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.Assertions;
using System;
using TMPro;
using Firebase.Firestore;
using Firebase.Auth;
using Firebase.Extensions;
public class GetSampleData : MonoBehaviour
{
    //https://www.youtube.com/watch?v=b5h1bVGhuRk&t=276s
    [SerializeField] private string _samplePath = "sample_test/one_sample";
    [SerializeField] private GameObject _bluePanelPrefab;
    [SerializeField] private GameObject _redPanelPrefab;
    [SerializeField] private Transform _contentParent;

    [SerializeField] private TMP_Dropdown searchDropdown;
    [SerializeField] private TMP_InputField searchInput;
    [SerializeField] private TMP_InputField searchLimit;
    [SerializeField] private String searchFieldSelection = "";
    [SerializeField] private String searchNameSelection = "";
    private int searchLimitSelection = 0;
    private List<SampleData> collectionSamples = new List<SampleData>();
    private SampleDAO sampleDAO;


    private void SetSearchFieldValue()
    {
        Debug.Log("Search Drop value : " + searchDropdown.value);
        switch (searchDropdown.value)
        {
            case 0:
                searchFieldSelection = "";
                break;
            case 1:
                searchFieldSelection = "Name";
                break;
            case 2:
                searchFieldSelection = "Company";
                break;
            case 3:
                searchFieldSelection = "Species";
                break;
            case 4:
                searchFieldSelection = "ProductionWeekNo";
                break;
            case 5:
                searchFieldSelection = "Date";
                break;
        }
    }
    public async void RetrieveCollectionBySearch()
    {
        SetSearchFieldValue();
        sampleDAO = new SampleDAO();
        searchNameSelection = searchInput.text;
        if (!searchLimit.text.Equals(""))
        {
            searchLimitSelection = int.Parse(searchLimit.text);
        }
        collectionSamples = await sampleDAO.GetSamplesBySearch(searchFieldSelection, searchNameSelection, searchLimitSelection);
        AddTextAndPrefab(collectionSamples);

    }
    public void ShowStoredSamples()
    {
        SaveData.Instance.LoadStoredSamples();//MAYBE???
        AddTextAndPrefab(SaveData.Instance.GetUserStoredSamples());
    }
    public void ShowAllDeviceSubmittedSamples()
    {
        SaveData.Instance.LoadSubmittedSamples();//MAYBE???
        AddTextAndPrefab(SaveData.Instance.GetUserSubmittedSamples());
    }
    public async void RetrieveAllUserSubmittedSamples(GameObject popUp)
    {
        sampleDAO = new SampleDAO();
        var firestore = FirebaseFirestore.DefaultInstance;
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        collectionSamples.Clear();// dont think i gptta do this anymore
        if (auth.CurrentUser != null)
        {
            collectionSamples = await sampleDAO.GetAllUserSubmittedSamples(auth.CurrentUser);
            AddTextAndPrefab(collectionSamples);

        }
        else
        {
            popUp.SetActive(true);
        }


    }
    private void AddTextAndPrefab(SampleData sample)
    {
        //maybe add isBluee bool
        GameObject panel;
        panel = Instantiate(_bluePanelPrefab);

        panel = Instantiate(_redPanelPrefab);
        panel.transform.SetParent(_contentParent.transform);
        //      blue.transform.parent = _contentParent.transform;
        GameObject panelChild = panel.transform.GetChild(0).gameObject;
        //   Text panelText = panelChild.GetComponent<Text>();

        Text panelText = panel.transform.GetChild(0).gameObject.GetComponent<Text>();

        //_contenParent.AddComponent
        panel.transform.localScale = new Vector3(1, 1, 1);

        panelText.text = SampleDataToString(sample, false);
    }
    private void AddTextAndPrefab(List<SampleData> sampleList)
    {
        foreach (Transform child in _contentParent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < sampleList.Count; i++)
        {

            GameObject panel;
            if (i % 2 == 0)
            {
                panel = Instantiate(_bluePanelPrefab);

            }
            else
            {
                panel = Instantiate(_redPanelPrefab);
            }
            panel.transform.SetParent(_contentParent.transform);
            GameObject panelChild = panel.transform.GetChild(0).gameObject;
            Text panelText = panel.transform.GetChild(0).gameObject.GetComponent<Text>();
            panel.transform.localScale = new Vector3(1, 1, 1);
            panelText.text = SampleDataToString(sampleList[i], false);
        }
    }
    private String SampleDataToString(SampleData sample, bool isRestricted)
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


}
/// <summary>
/// 
/// 
/// 
/// 
/// 
/// 
/// 
/// 
/// </summary>
//useless now because of the search retrieve optioon
/*   
 *   


    //Return sample data with ONLY the location thats filled


    public void CollectionToText(List<SampleData> sampleDataList)
    {
        foreach (Transform t in _contentParent)
        {
            Destroy(t.gameObject);
        }
        AddTextAndPrefab(sampleDataList);
    }
 *   
 *   public void CollectionToText()
   {
       foreach (Transform t in _contentParent)
       {
           Destroy(t.gameObject);
       }
       //  firestore.Collection("Samples").Document().SetAsync(sampleData);
       var firestore = FirebaseFirestore.DefaultInstance;
       firestore.Collection("Samples").GetSnapshotAsync().ContinueWithOnMainThread(task =>
       {
           Assert.IsNull(task.Exception);

           *//* Debug.Log(task.Result.Documents.ToString());
            task.Result*//*
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
                   //  samplesString += (SampleDataToStringRestricted(sample) + "\n\n");
                   //    AddTextAndPrefab(sample, counter);
               }
               catch (Exception e)
               {
                   Debug.Log(e + " CAUGHT");
                   counter2++;
               }
           }
           AddTextAndPrefab(collectionSamples);
           //  _text.text = samplesString;
           //  SetDataText(collectionSamples);
           Debug.Log("Try : " + counter + ".  Caught : " + counter2);
       });
   }
   public void CollectionToText(String collectionName)
   {
       foreach (Transform t in _contentParent)
       {
           Destroy(t.gameObject);
       }
       //  firestore.Collection("Samples").Document().SetAsync(sampleData);
       var firestore = FirebaseFirestore.DefaultInstance;

       firestore.Collection(collectionName).GetSnapshotAsync().ContinueWithOnMainThread(task =>
       {
           Assert.IsNull(task.Exception);
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
                   //  samplesString += (SampleDataToStringRestricted(sample) + "\n\n");                }
                   //   AddTextAndPrefab(sample, counter);
               }
               catch (Exception e)
               {
                   Debug.Log(e + " CAUGHT");
                   counter2++;
               }
           }
           AddTextAndPrefab(collectionSamples);
           //   _text.text = samplesString;
           //  SetDataText(collectionSamples);
       });

   }
   //not needed
   public SampleData GetData(String _path)
   {
       var firestore = FirebaseFirestore.DefaultInstance;
       firestore.Document(_path).GetSnapshotAsync().ContinueWithOnMainThread(task =>
       {
           Assert.IsNull(task.Exception);
           var sampleData = task.Result.ConvertTo<SampleData>();
           //  var limitedSampleData = task.Result.ConvertTo<LimitedSampleData>();
           // SetDataText(sampleData);
           return sampleData;
       });
       //FIX FALLOUT FROM THIS __ DONT WANT RETURN empty__ CANT RETURN NULL
       return new SampleData();
   }*/

/*
 * 
 * IMPORTANT --------------------
 *             /*CollectionReference userSampleCollection = firestore.Collection("Users").Document(auth.CurrentUser.Email).Collection("UserSamples");
            userSampleCollection.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                Assert.IsNull(task.Exception);
                QuerySnapshot collectionSnapshot = task.Result;
                int counter = 0;
                int counter2 = 0;
                foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
                {
                    try
                    {
                        SampleData sample = documentSnapshot.ConvertTo<SampleData>();
                        counter++;
                        collectionSamples.Add(sample);
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e + " CAUGHT");
                        counter2++;
                    }
                }
                AddTextAndPrefab(collectionSamples);
                Debug.Log("Try : " + counter + ".  Caught : " + counter2);
            });
*
        /*   Debug.Log(searchFieldSelection + " _____ " + searchNameSelection);

           var firestore = FirebaseFirestore.DefaultInstance;

           collectionSamples.Clear();
           CollectionReference samplesReference = firestore.Collection("Samples");
           Query testQuery;
           if (searchFieldSelection.Equals("ProductionWeekNo"))
           {
                testQuery = samplesReference.WhereEqualTo(searchFieldSelection,int.Parse(searchNameSelection));

           }else if(searchNameSelection.Equals("") && searchFieldSelection.Equals("")){
               testQuery = firestore.Collection("Samples");
           }
           else {
               testQuery = samplesReference.WhereEqualTo(searchFieldSelection, searchNameSelection);//.Limit(0);
           }
           if (searchLimitSelection > 0)
           {
               testQuery = testQuery.Limit(searchLimitSelection);
           }
           //   Query testQuery = samplesReference.WhereEqualTo("Name", username); //better option
           // firestore.Collection("Samples").GetSnapshotAsync().ContinueWithOnMainThread(task =>
           testQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
           {
               Assert.IsNull(task.Exception);
               QuerySnapshot collectionSnapshot = task.Result;
               int counter = 0;
               int counter2 = 0;
              // String samplesString = "";
               foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
               {
                   try
                   {
                       SampleData sample = documentSnapshot.ConvertTo<SampleData>();
                       counter++;
                       collectionSamples.Add(sample);
                   }
                   catch (Exception e)
                   {
                       Debug.Log(e + " CAUGHT");
                       counter2++;
                   }
               }
               AddTextAndPrefab(collectionSamples);
               Debug.Log("Try : " + counter + ".  Caught : " + counter2);
           });
*
* 
 * 
 * 
 * 
 * 
 * 
 *    public void GetAndSetUserData(String username)
    {
        foreach (Transform t in _contentParent)
        {
            Destroy(t.gameObject);
        }
        //  firestore.Collection("Samples").Document().SetAsync(sampleData);
        var firestore = FirebaseFirestore.DefaultInstance;

        collectionSamples.Clear();
         CollectionReference samplesReference = firestore.Collection("Samples");
        //Query testQuery2 = samplesReference.wher;
         Query testQuery = samplesReference.WhereEqualTo("Name",username); //better option
        testQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>


       // firestore.Collection("Samples").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);
            /* Debug.Log(task.Result.Documents.ToString());
             task.Result
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
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
public void getAndOrderLimitData()
{
    //SLOW
    foreach (Transform t in _contentParent)
    {
        Destroy(t.gameObject);
    }
    // _contentParent = _contentCopy;
    var firestore = FirebaseFirestore.DefaultInstance;
    //  CollectionReference samplesReference =firestore.Collection("SamplesFull");
    CollectionReference samplesReference = firestore.Collection("Samples");
    //Query testQuery2 = samplesReference.wher;
    Query testQuery = samplesReference.OrderBy("Name").Limit(3);
    collectionSamples.Clear();
    testQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
    {
        Assert.IsNull(task.Exception);//WHY
         Debug.Log(task.Result.Documents.ToString());
         task.Result
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

                //      AddTextAndPrefab(sample, counter);


            }
            catch (Exception e)
            {
                Debug.Log(e + " CAUGHT");
                counter2++;
            }
        }
        AddTextAndPrefab(collectionSamples);
        Debug.Log("Try : " + counter + ".  Caught : " + counter2);
    });
}*/
/*   private void OnDestroy()
    {
       // _listenerRegistration.Stop();
    }
    private GameObject _contentCopy;
    private ListenerRegistration _listenerRegistration;
    //remove at some point
    private Dictionary<string, object> collection;
 
 */
/*    public void AddTextAndPrefab(SampleData sample, int counter)
    {

        GameObject panel;
        if (counter % 2 == 0)
        {
            panel = Instantiate(_bluePanelPrefab);

        }
        else
        {
            panel = Instantiate(_redPanelPrefab);
        }
        panel.transform.SetParent(_contentParent.transform);
        //      blue.transform.parent = _contentParent.transform;
        GameObject panelChild = panel.transform.GetChild(0).gameObject;
        //   Text panelText = panelChild.GetComponent<Text>();

        Text panelText = panel.transform.GetChild(0).gameObject.GetComponent<Text>();

        //_contenParent.AddComponent
        panel.transform.localScale = new Vector3(1, 1, 1);

        panelText.text = SampleDataToString(sample, false);
    }*/

/*      public void RetrieveCollectionData()
    {
        //  firestore.Collection("Samples").Document().SetAsync(sampleData);
        var firestore = FirebaseFirestore.DefaultInstance;

        firestore.Collection("Samples").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);
            /*Assert.IsNull(task.Exception);
            Debug.Log(task.Result.Documents.ToString());
            task.Result
//https://firebase.google.com/docs/firestore/query-data/get-data#unity_5
QuerySnapshot collectionSnapshot = task.Result;
int counter = 0;
foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
{
    Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
    Dictionary<string, object> city = documentSnapshot.ToDictionary();
    collection = documentSnapshot.ToDictionary();
    String full = "";
    foreach (KeyValuePair<string, object> pair in city)
    {
        // Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));

        full += (String.Format("{0}: {1}\n", pair.Key, pair.Value));
    }
    Debug.Log(full);
    // Newline to separate entries
    Debug.Log("");
}
        });

    }
 *  
 *  
    public String SampleDataToString(SampleData sample)
    {
        if (sample.SampleLocationName == null)
        {
            Debug.Log("Location is null");

            return ("Name: " +sample.Name+ "\nCompany: " + sample.Company + "\nSpecies: " + sample.Species
           + $"\nICEs Rectangle: {sample.IcesRectangleNo}"
           + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date +"\nComment: "+sample.Comment);
        }
        else
        {
            Debug.Log("ICES rectangle is null");
            return ("Name: " + sample.Name + "\nCompany: " + sample.Company + "\nSpecies: " + sample.Species
            + "\nLocation: " + sample.SampleLocationName + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date
            + "\nComment: " + sample.Comment);
        }
    }
 *  
 *         //SettingText
            //_text.text = total;
            // SetDataText(collectionSamples);
            //   _text.text = samplesString;

            /*
       Query query = citiesRef
      .WhereGreaterThan("Population", 2500000)
      .OrderBy("Population")
      .Limit(2);
          
//   GameObject t = _redPanel.transform.GetChild(0).gameObject;
// Text tt = t.GetComponent<Text>();
//  tt.text = "__________________ PURPLE PINK ________________";
*
*  
 *  
 *  
 *      
    public void GetAndSetDataText()
    {
        SetDataText(GetData(_samplePath));
    }
    public void GetAndSetDataText(String _path)
    {
        SetDataText(GetData(_path));
    }
    
    public void SetDataText(SampleData sampleData)
    {
    //    _text.text = SampleDataToStringRestricted(sampleData);
    }
 
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *    Debug.Log("in Get data - loop prepare");
        int counter = 0;
        foreach (SampleData sample in sampleList)
        {
            counter++;
            Debug.Log("in Get data - BEFORE " + counter);
            AddTextAndPrefab(sample, counter);
        }
        Debug.Log("TEXT SET: " + counter);

 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  collectionSamples.Add(sample);
  GameObject blue = Instantiate(_bluePanelPrefab);
  blue.transform.SetParent( _contentParent.transform);
//      blue.transform.parent = _contentParent.transform;
  GameObject bluechild = blue.transform.GetChild(0).gameObject; 
  Text blueText = bluechild.GetComponent<Text>();

  //_contenParent.AddComponent
  blue.transform.localScale = new Vector3(1, 1, 1);

  blueText.text = SampleDataToString(sample);
 // samplesString += (SampleDataToString(sample) + "\n\n");
 // Debug.Log("Ordered and Limited " + counter);
*/



/*    public List<SampleData> GetDataList()
    {
        return null;
    }*/
///

///
//
///
///
///ds
///
/*
 
     public void CollectionToText(String name)
    {

        //  firestore.Collection("Samples").Document().SetAsync(sampleData);
        var firestore = FirebaseFirestore.DefaultInstance;
        collectionSamples.Clear();
        firestore.Collection("Samples").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);
             Debug.Log(task.Result.Documents.ToString());
             task.Result
//https://firebase.google.com/docs/firestore/query-data/get-data#unity_5
QuerySnapshot collectionSnapshot = task.Result;
int counter = 0;
int counter2 = 0;
String total = "";
//when using full data set going to have to do limited loop
//may create specieal method - e.g. have permission accedd full
//no permission access
foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
{
    //prob dont need tryu block here
    try
    {

        SampleData sample = documentSnapshot.ConvertTo<SampleData>();
        if (sample.Name == name)
        {
            Debug.Log("Name match");
            counter++;
            collectionSamples.Add(sample);
        }
    }
    catch (Exception e)
    {
        Debug.Log(e + " CAUGHT");
        counter2++;
    }

}
//SettingText
//_text.text = total;
GetData(collectionSamples);
Debug.Log("Try : " + counter + ".  Caught : " + counter2);

        });

    }*/




//FROM GET DATA
// _text.text = SampleDataToStringRestricted(sampleData);
/*   if (sampleData.SampleLocationName==null)
   {
       Debug.Log("Location is null");

       _text.text = "\nSpecies: " + sampleData.Species
  + $"\nICEs Rectangle: {sampleData.IcesRectangleNo}"
  + "\nWeek: " + sampleData.ProductionWeekNo + "\nDate: " + sampleData.Date;
   }
   else
   {
       Debug.Log("ICES rectangle is null");
       _text.text = "\nSpecies: " + sampleData.Species
       + "\nLocation: " + sampleData.SampleLocationName + "\nWeek: " + sampleData.ProductionWeekNo  + "\nDate: " + sampleData.Date;
   }*/












///COLLECTION TO TEXT METHOD - OPTIONS
///
/// 
/// 
/*This section goes through each doc in collection
and print them to the supplied txt boz.. 
            However, may be much easier to use above solution
            getting sample data array and for printing.
             */
/*   Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
   Dictionary<string, object> city = documentSnapshot.ToDictionary();
   collection = documentSnapshot.ToDictionary();
   String full = "";
   foreach (KeyValuePair<string, object> pair in city)
   {
       //printing without name and company details
       //figure out why reverses if/else doesnt work : i.e ! pair.ley...
       if((pair.Key== "Name") || (pair.Key=="Company"))
       {
           Debug.Log("***********************************************************************************************************************");
       }
       else
       {
           full += (String.Format("{0}: {1}\n", pair.Key, pair.Value));
       }
   }
   total += (full + "\n");*/
/// 
/// 
/// 
/// 
/// 
/// 
/// //prevcious get data string
//  String sampleString = "";
/*  if (sample.SampleLocationName == null)
  {
      Debug.Log("Location is null");

      sampleString = "\nSpecies: " + sample.Species
 + $"\nICEs Rectangle: {sample.IcesRectangleNo}"
 + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date;
  }
  else
  {
      sampleString ="\nSpecies: " + sample.Species
      + "\nLocation: " + sample.SampleLocationName + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date;
  }  samples += (sampleString + "\n\n\n");*/

/*String temp = "";
String total = "";
Debug.Log("Trying for loop : ");
int counter = 0;
foreach (KeyValuePair<string, object> pair in collection)
{
    Debug.Log("in foreach ");
    // Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
    if (counter < 6)
    {
        temp += (String.Format("{0}: {1}\n", pair.Key, pair.Value));
    }
    else
    {
        counter = 0;
        temp += (String.Format("{0}: {1}\n\n\n\n", pair.Key, pair.Value));
    }
    total += (temp );
    Debug.Log(temp);
    temp = "";


}
Debug.Log(total);
_text.text = total;*/


///COLLECTION TO TEXT METHOD - OPTIONS






/*  _text.text = $"Name: {sampleData.Name}" + $"\nICEs:{sampleData.IcesRectangleNo}"
         + "\nWeek:" +sampleData.ProductionWeekNo +"\nSpecies:"+sampleData.Species 
         +"\nLocation:"+ sampleData.SampleLocationName +"\nDate: "+sampleData.Date
         +  $"\n\n\n\n\nICEs:{limitedSampleData.IcesRectangleNo}"
         + "\nWeek:" + limitedSampleData.ProductionWeekNo + "\nSpecies:" + limitedSampleData.Species
         + "\nLocation:" + limitedSampleData.SampleLocationName + "\nDate: " + limitedSampleData.Date;*/
/*      firestore.Document(_samplePath).GetSnapshotAsync().ContinueWithOnMainThread(task =>
      {
          Assert.IsNull(task.Exception);
          var sampleData = task.Result.ConvertTo<SampleData>();
          var limitedSampleData = task.Result.ConvertTo<LimitedSampleData>();
          if (sampleData.SampleLocationName == null)
          {
              Debug.Log("Location is null");

              _text.text = "\nSpecies: " + sampleData.Species
         + $"\nICEs: {sampleData.IcesRectangleNo}"
         + "\nWeek: " + sampleData.ProductionWeekNo + "\nDate: " + sampleData.Date;


          }
          else
          {
              _text.text = "\nSpecies: " + sampleData.Species
         + "\nWeek: " + sampleData.ProductionWeekNo + "\nLocation: " + sampleData.SampleLocationName + "\nDate: " + sampleData.Date;
          }

            _text.text = $"Name: {sampleData.Name}" + $"\nICEs:{sampleData.IcesRectangleNo}"
            + "\nWeek:" +sampleData.ProductionWeekNo +"\nSpecies:"+sampleData.Species 
            +"\nLocation:"+ sampleData.SampleLocationName +"\nDate: "+sampleData.Date
            +  $"\n\n\n\n\nICEs:{limitedSampleData.IcesRectangleNo}"
            + "\nWeek:" + limitedSampleData.ProductionWeekNo + "\nSpecies:" + limitedSampleData.Species
            + "\nLocation:" + limitedSampleData.SampleLocationName + "\nDate: " + limitedSampleData.Date;
      });*/
/*  else if(sampleData.SampleLocationName =="")
            {
                Debug.Log("Location is empty string");
            }*/