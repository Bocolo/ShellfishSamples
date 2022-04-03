using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using System;
using TMPro;
using Submit.UI;
using Firebase.Auth;
using Firebase.Extensions;
using Submit.UI;

public class SubmitSampleData : MonoBehaviour
{
    [SerializeField] private string _samplePath = "sample_test/one_sample";
    [SerializeField] private SubmitCanvasManager canvasManager;
    private string _nameString = null;
    private string _companyString = null;
    private string _commentsString = null;
    private string _speciesString = null;
    private string _icesString = null;
    private string _locationString = null;

    private string date;
    private string day;
    private string month;
    private string year;
    private SampleDAO sampleDAO;
    private UserDAO userDAO;

    private String missingValues = "";

    public void UploadData()
    {
        SetValues();
        if (!IsValuesMissing() && IsDateValid())
        {
            userDAO = new UserDAO();
            sampleDAO = new SampleDAO();
            var sampleData = NewSample();
            sampleDAO.AddSample(sampleData);
            SaveData.Instance.AddToSubmittedSamples(sampleData);
            SaveData.Instance.SaveSubmittedSamples();
            canvasManager.OnSubmitClearFields();

            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                sampleDAO.AddSampleToUserCollection(auth.CurrentUser, sampleData);
                userDAO.UpdateUserSampleCount(auth.CurrentUser); //has to be a better way to do this
            }
            canvasManager.DisplayPopUP("\n\nSuccessfully Submitted Sample");// this is wrinfg -- can onlt reeally be successful is only
        }

    }



    public void SubmitStoredData()
    {
        try
        {
            sampleDAO = new SampleDAO();
            userDAO = new UserDAO();
            var firestore = FirebaseFirestore.DefaultInstance;
            List<SampleData> storedSamples = SaveData.Instance.GetUserStoredSamples();
            FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
            int counter = 0;
            for (int i = 0; i < storedSamples.Count; i++)
            {
                sampleDAO.AddSample(storedSamples[i]);
                SaveData.Instance.AddToSubmittedSamples(storedSamples[i]);
                counter++;
                if (user != null)
                {
                    sampleDAO.AddSampleToUserCollection(user, storedSamples[i]);
                }
            }
            userDAO.UpdateUserSampleCount(user, counter);
            SaveData.Instance.ClearStoredSamples();
            SaveData.Instance.SaveStoredSamples();
            SaveData.Instance.SaveSubmittedSamples();
        }
        catch (Exception e)
        {
            Debug.LogError(e + " submitting stored data error");
        }
    }
    public void StoreSample()
    {
        SetValues();
        if (!IsValuesMissing() && IsDateValid())
        {
            var sampleData = NewSample();
            SaveData.Instance.AddToStoredSamples(sampleData);
            canvasManager.OnSubmitClearFields();
            canvasManager.DisplayPopUP("\n\nSuccessfully Stored Sample");

        }
        SaveData.Instance.SaveStoredSamples();

    }

    private SampleData NewSample()
    {
        SampleData sample = new SampleData
        {
            Species = _speciesString,
            IcesRectangleNo = _icesString,
            Company = _companyString,
            Date = date,//_sampleDateString,
            Name = _nameString,
            ProductionWeekNo = int.Parse(canvasManager._productionWk.options[canvasManager._productionWk.value].text),
            SampleLocationName = _locationString,
            Comment = _commentsString

        };
        return sample;
    }
    private bool IsValuesMissing()
    {
        missingValues = "";
        //FIX THIS MESS WITH A BOOLEAN
        if ((_nameString == null) || (_companyString == null) ||
            (_speciesString == null) || (canvasManager._productionWk.value == 0) ||
            ((canvasManager.DayDrop.value == 0) || (canvasManager.MonthDrop.value == 0) || (canvasManager.YearDrop.value == 0)) ||
            (((_icesString == null) && (_locationString == null)) || ((_icesString != null) && (_locationString != null))))
        {
            missingValues += "<b>Incorrect Input Format: </b>\n\n";
            if (_nameString == null)
            {
                missingValues += "Please enter a name\n";
            }
            if (_companyString == null)
            {
                missingValues += "Please enter a company name\n";
            }
            if (_speciesString == null)
            {
                missingValues += "Please enter the shellfish species\n";
            }
            if (((_icesString == null) && (_locationString == null)) || ((_icesString != null) && (_locationString != null)))
            {
                missingValues += "You must enter <i>either</i> a Sample Location Date or an Ices Rectangle No.\n";
            }

            if (canvasManager._productionWk.value == 0)
            {
                missingValues += "Please enter the production week\n";
            }
            if (!IsDateValid())
            {
                missingValues += "Please enter a valid date\n";
            }
            canvasManager.DisplayPopUP(missingValues);
            return true;
        }
        else
        {
            return false;
        }
    }
    private void SetValues()
    {
        _nameString = null;
        _companyString = null;
        // _sampleDateString = null;
        _commentsString = null;
        _speciesString = null;
        _icesString = null;
        _locationString = null;
        date = null;
        if (canvasManager._name.text != "")
        {
            _nameString = canvasManager._name.text;
        }
        if (canvasManager._company.text != "")
        {
            _companyString = canvasManager._company.text;
        }
        if (canvasManager._comments.text != null)
        {
            _commentsString = canvasManager._comments.text;
        }

        if (canvasManager._species.value != 0)
        {
            _speciesString = canvasManager._species.options[canvasManager._species.value].text;
        }

        if (canvasManager._iceRectangle.value != 0)
        {
            _icesString = canvasManager._iceRectangle.options[canvasManager._iceRectangle.value].text;
        }
        if (canvasManager._sampleLocationName.value != 0)
        {
            _locationString = canvasManager._sampleLocationName.options[canvasManager._sampleLocationName.value].text;
        }
    }
    private bool IsDateValid()
    {
        day = canvasManager.DayDrop.options[canvasManager.DayDrop.value].text;
        month = canvasManager.MonthDrop.options[canvasManager.MonthDrop.value].text;
        year = canvasManager.YearDrop.options[canvasManager.YearDrop.value].text;
        date = year + "-" + month + "-" + day;
        try
        {
            var datetime = DateTime.Parse(date);
            DateTime local = DateTime.Now;
            int result = DateTime.Compare(datetime, local);
            if (result > 0)
            {
                return false;
            }

            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("Date Check failed");
            return false;
        }


    }

}
/*
 *     private void UpdateUserFirestoreSampleCount(FirebaseUser user, FirebaseFirestore firestore, int numberOfSamples)
    {
        DocumentReference docRef = firestore.Collection("Users").Document(user.Email);

        int previouslyStoredSampleCount = 0;
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot documentSnapshot = task.Result;
            previouslyStoredSampleCount = documentSnapshot.GetValue<int>("SubmittedSamplesCount");
            previouslyStoredSampleCount += numberOfSamples;
            docRef.SetAsync(new Dictionary<string, int> { { "SubmittedSamplesCount", previouslyStoredSampleCount } }, SetOptions.MergeAll);
            UserData userData = SaveData.Instance.LoadUserProfile();
            userData.SubmittedSamplesCount = previouslyStoredSampleCount;
            SaveData.Instance.SaveUserProfile(userData);
            Debug.Log("_TESTING--- Fs DB Count update-number samples");
        });

    }
 * 
 * 
     private void UpdateUserFirestoreSampleCount(FirebaseUser user, FirebaseFirestore firestore)
    {
        DocumentReference docRef = firestore.Collection("Users").Document(user.Email);

        int previouslyStoredSampleCount = 0;
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot documentSnapshot = task.Result;
            previouslyStoredSampleCount = documentSnapshot.GetValue<int>("SubmittedSamplesCount");
            previouslyStoredSampleCount += 1;
            docRef.SetAsync(new Dictionary<string, int> { { "SubmittedSamplesCount", previouslyStoredSampleCount } }, SetOptions.MergeAll);
            UserData userData = SaveData.Instance.LoadUserProfile();
            userData.SubmittedSamplesCount = previouslyStoredSampleCount;
            SaveData.Instance.SaveUserProfile(userData);
            Debug.Log("_TESTING--- Fs DB Count update");
        });

    }
 
 
 */

/**
 * rules_version = '2';
service cloud.firestore {
  match /databases/{database}/documents {
    match /{document=**} {
      allow read, write: if
          request.auth != null;
    }
  }
}
 */
//DocumentReference docRef = firestore.Collection("Users").Document(auth.CurrentUser.Email);
//int previouslyStoredSampleCount =0;
//docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
//{
//    DocumentSnapshot documentSnapshot = task.Result;
//    Debug.Log(task.Result);
//    previouslyStoredSampleCount = documentSnapshot.GetValue<int>("SubmittedSamplesCount");
//    Debug.Log(previouslyStoredSampleCount + " ______>>" + auth.CurrentUser.Email);
//    //.ContainsField("SubmittedSamplesCount").ToString();//get("SubmittedSamplesCount");
//    Debug.Log("____ SS count ___ before");
//    previouslyStoredSampleCount += 1;
//    docRef.SetAsync(new Dictionary<string, int> { { "SubmittedSamplesCount", previouslyStoredSampleCount } }, SetOptions.MergeAll);
//    // docRef.UpdateAsync("SubmittedSamplesCount", (firestore.Collection("Users").Document(auth.CurrentUser.Email)).);
//    UserData userData = SaveData.Instance.LoadUserProfile();
//    userData.SubmittedSamplesCount = previouslyStoredSampleCount;
//    SaveData.Instance.SaveUserProfile(userData);
//    //__NEED TO BE STOREING THE NEWLY UPDATED STORED HERE
//    Debug.Log("____ SS count ___ after");
//});

//UpdateUserFirestoreData(auth.CurrentUser);



//private void Awake()
//{
//    // SetCanvasInputsToSmall(true);
//    // SetNameAndCompanyFromProfile();
//}

//private void SetNameAndCompanyFromProfile()
//{
//    UserData userData = SaveData.Instance.LoadUserProfile();
//    _name.text = userData.Name;
//    _company.text = userData.Company;
//}
//public void SwitchCanvas()
//{
//    if (SmallCanvas.activeInHierarchy)
//    //  if (SmallCanvas.isActiveAndEnabled)
//    {
//        _pop_up.gameObject.SetActive(false);

//        SmallCanvas.SetActive(false);
//        LargeCanvas.SetActive(true);
//        SetCanvasInputsToSmall(false);
//        SetNameAndCompanyFromProfile();
//        ///
//        /// I WANT TO BE ABLE TO STRANSFER INPUT ACROSS LARGE AND SMALL CANVASES- DO SO HERE
//        ///
//    }
//    else
//    {
//        _pop_up.gameObject.SetActive(false);
//        SmallCanvas.SetActive(true);
//        LargeCanvas.SetActive(false);
//        SetCanvasInputsToSmall(true);
//        SetNameAndCompanyFromProfile();

//    }
//}
//private void SetCanvasInputsToSmall(bool isSmall)
//{
//    if (isSmall)
//    {
//        _name = _name_sml;
//        _company = _company_sml;
//        _productionWk = _productionWk_sml;
//        _species = _species_sml;
//        DayDrop = DayDrop_sml;
//        MonthDrop = MonthDrop_sml;
//        YearDrop = YearDrop_sml;
//        _iceRectangle = _iceRectangle_sml;
//        _sampleLocationName = _sampleLocationName_sml;
//        canvasManager._comments = _comments_sml;
//        _submitButton = _submitButton_sml;
//        _pop_up = _pop_up_sml;

//    }
//    else
//    {
//        _name = _name_lrg;
//        _company = _company_lrg;
//        _productionWk = _productionWk_lrg;
//        _species = _species_lrg;
//        DayDrop = DayDrop_lrg;
//        MonthDrop = MonthDrop_lrg;
//        YearDrop = YearDrop_lrg;
//        _iceRectangle = _iceRectangle_lrg;
//        _sampleLocationName = _sampleLocationName_lrg;
//        _comments = _comments_lrg;
//        _submitButton = _submitButton_lrg;
//        _pop_up = _pop_up_lrg;

//    }
//}



//private void DisplayPopUP(String missingValues)
//{
//    //Instantiate();
//    canvasManager._pop_up.text = missingValues;
//    canvasManager._pop_up.gameObject.SetActive(true);
//}
//public void HidePopUp()
//{
//    canvasManager._pop_up.gameObject.SetActive(false);
//}





























































/*  if (_sampleDate.text != "")
     {
         _sampleDateString = _sampleDate.text;
     }*/
/// <summary>
/// not perfect --- what about when you change date  on phone????
/// </summary>
/// <returns></returns>

//|| (_sampleDateString == null)
/*  if (_sampleDateString == null)
  {
      Debug.Log("You must input sample date");
  }*/
//if ((_icesString != null) && (_locationString != null))
//{
//    Debug.Log("You must fill one or other but nor both");
//    missingValues += "You must enter <i>either</i> a Sample Location Date or an Ices Rectangle No.\n";
//}

//var firestore = FirebaseFirestore.DefaultInstance;
//firestore.Document(_samplePath).SetAsync(sampleData); //,SetOptions.MergeAll);
//firestore.Collection("Samples").Document().SetAsync(sampleData);//you work for random ID generartion

//firestore.Document(_samplePath).SetAsync(sampleData); overrides document



//private void SetNameAndCompanyFromProfile()
//{
//    UserData userData = SaveData.Instance.LoadUserProfile();
//    _name.text =userData.Name;
//    _company.text = userData.Company;
//}



/* var sampleData = new SampleData
           {
               Species = _speciesString,
               IcesRectangleNo = _icesString,
               //_iceRectangle.options[_iceRectangle.value].text,
               Company = _companyString,
               Date = date,//_sampleDateString,
               Name = _nameString,
               ProductionWeekNo = int.Parse(_productionWk.options[_productionWk.value].text),
               SampleLocationName = _locationString,
               Comment = _commentsString

           };*/

/*new SampleData
{
Species = _speciesString,
IcesRectangleNo = _icesString,
//_iceRectangle.options[_iceRectangle.value].text,
Company = _companyString,
Date = date,//_sampleDateString,
Name = _nameString,
ProductionWeekNo = int.Parse(_productionWk.options[_productionWk.value].text),
SampleLocationName = _locationString,
Comment = _commentsString

};*/
/*  
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
 *  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using System;
using TMPro;
using Submit.UI;

public class SubmitSampleData : MonoBehaviour
{
    //Small Canvas Game Objects
    [SerializeField] private string _samplePath = "sample_test/one_sample";
    //Required Fields
    //[SerializeField] private  TMP_InputField _name;
    //[SerializeField] private TMP_InputField _company;
    ////[SerializeField] private TMP_InputField _sampleDate;
    //[SerializeField] private TMP_Dropdown _productionWk;
    //[SerializeField] private TMP_Dropdown _species;
    ////date fields
    //[SerializeField] private TMP_Dropdown DayDrop;
    //[SerializeField] private TMP_Dropdown MonthDrop;
    //[SerializeField] private TMP_Dropdown YearDrop;
    ////either but not both
    //[SerializeField] private TMP_Dropdown _iceRectangle;
    //[SerializeField] private TMP_Dropdown _sampleLocationName;
    ////Not Required
    //[SerializeField] private TMP_InputField _comments;

    //[SerializeField] private Button _submitButton;


    //private TMP_InputField _name;
    //private TMP_InputField _company;
    ////[SerializeField] private TMP_InputField _sampleDate;
    //private TMP_Dropdown _productionWk;
    //private TMP_Dropdown _species;
    ////date fields
    //private TMP_Dropdown DayDrop;
    //private TMP_Dropdown MonthDrop;
    //private TMP_Dropdown YearDrop;
    ////either but not both
    //private TMP_Dropdown _iceRectangle;
    //private TMP_Dropdown _sampleLocationName;
    ////Not Required
    //private TMP_InputField _comments;
    //private Button _submitButton;


    ////SMALL
    //[SerializeField] private TMP_InputField _name_sml;
    //[SerializeField] private TMP_InputField _company_sml;
    ////[SerializeField] private TMP_InputField _sampleDate;
    //[SerializeField] private TMP_Dropdown _productionWk_sml;
    //[SerializeField] private TMP_Dropdown _species_sml;
    ////date fields
    //[SerializeField] private TMP_Dropdown DayDrop_sml;
    //[SerializeField] private TMP_Dropdown MonthDrop_sml;
    //[SerializeField] private TMP_Dropdown YearDrop_sml;
    ////either but not both
    //[SerializeField] private TMP_Dropdown _iceRectangle_sml;
    //[SerializeField] private TMP_Dropdown _sampleLocationName_sml;
    ////Not Required
    //[SerializeField] private TMP_InputField _comments_sml;

    //[SerializeField] private Button _submitButton_sml;


    ////LARGE
    //[SerializeField] private TMP_InputField _name_lrg;
    //[SerializeField] private TMP_InputField _company_lrg;
    ////[SerializeField] private TMP_InputField _sampleDate;
    //[SerializeField] private TMP_Dropdown _productionWk_lrg;
    //[SerializeField] private TMP_Dropdown _species_lrg;
    ////date fields
    //[SerializeField] private TMP_Dropdown DayDrop_lrg;
    //[SerializeField] private TMP_Dropdown MonthDrop_lrg;
    //[SerializeField] private TMP_Dropdown YearDrop_lrg;
    ////either but not both
    //[SerializeField] private TMP_Dropdown _iceRectangle_lrg;
    //[SerializeField] private TMP_Dropdown _sampleLocationName_lrg;
    ////Not Required
    //[SerializeField] private TMP_InputField _comments_lrg;

    //[SerializeField] private Button _submitButton_lrg;


    private string _nameString = null;
    private string _companyString = null;
  //  private string _sampleDateString = null;
    private string _commentsString = null;
    private string _speciesString = null;
    //  private int _productionWkString = NaN;
    private string _icesString = null;
    private string _locationString = null;

    private string date;
    private string day;
    private string month;
    private string year;
    public static List<SampleData> savedSamples = new List<SampleData>();

    void Start()
    {
        //savedSamples = new List<SampleData>();

        _submitButton.onClick.AddListener(() =>
        {
            //inelegant solution to ensure values are coorect
            //inelegant solution to set empty calue to null 
            //still need to resolve production week
            SetValues();
            if (!IsValuesMissing() && IsDateValid())
            {
                var sampleData = new SampleData
                {
                    Species = _speciesString,
                    IcesRectangleNo = _icesString,
                    //_iceRectangle.options[_iceRectangle.value].text,
                    Company = _companyString,
                    Date = date,//_sampleDateString,
                    Name = _nameString,
                    ProductionWeekNo = int.Parse(_productionWk.options[_productionWk.value].text),
                    SampleLocationName = _locationString,
                    Comment = _commentsString

                };
                var firestore = FirebaseFirestore.DefaultInstance;
                firestore.Document(_samplePath).SetAsync(sampleData); //,SetOptions.MergeAll);
                firestore.Collection("Samples").Document().SetAsync(sampleData);//you work for random ID generartion
                Debug.Log("Success");

                //firestore.Document(_samplePath).SetAsync(sampleData); overrides document
            }


        });

    }
  
    public void SubmitStoredData()
    {
        try {
            var firestore = FirebaseFirestore.DefaultInstance;
            for (int i = 0; i < savedSamples.Count; i++)
            {
                firestore.Collection("Samples").Document().SetAsync(savedSamples[i]);
                Debug.Log("Successfully saved stored sample "+ i);

            }
            savedSamples.Clear();
            Debug.Log("Cleared saved samples list ");
            SaveData.Instance.Save();
            Debug.Log("Saved empty data to stored list");

        }
        catch (Exception e)
        {
            Debug.LogError(e + " submitting stored data error");
        }
    }
    public void StoreSample()
    {
        //array list -- convert and add to arrray

        //inelegant solution to ensure values are coorect
        //inelegant solution to set empty calue to null 
        //still need to resolve production week
        SetValues();
        if (!IsValuesMissing() && IsDateValid())
        {
            Debug.Log("Creating the storeable sample");
            var sampleData = new SampleData
            {
                Species = _speciesString,
                IcesRectangleNo = _icesString,
                //_iceRectangle.options[_iceRectangle.value].text,
                Company = _companyString,
                Date = date,//_sampleDateString,
                Name = _nameString,
                ProductionWeekNo = int.Parse(_productionWk.options[_productionWk.value].text),
                SampleLocationName = _locationString,
                Comment = _commentsString

            };
            Debug.Log("about to add it");
            savedSamples.Add(sampleData);
            Debug.Log("added to thge list");

            //var firestore = FirebaseFirestore.DefaultInstance;
            //firestore.Document(_samplePath).SetAsync(sampleData); //,SetOptions.MergeAll);
            //firestore.Collection("Samples").Document().SetAsync(sampleData);//you work for random ID generartion

            //firestore.Document(_samplePath).SetAsync(sampleData); overrides document
        }
     SaveData.Instance.Save();

    }
    private bool IsValuesMissing()
    {
        if ((_nameString == null) || (_companyString == null) ||
            (_speciesString == null) || (_productionWk.value == 0) ||
            ((DayDrop.value == 0) || (MonthDrop.value == 0) || (YearDrop.value == 0)) ||
            (((_icesString == null) && (_locationString == null)) || ((_icesString != null) && (_locationString != null))))
        {
            //adding isdate valid instead of date drop?>
            Debug.Log("not filled correctly");
            if (_nameString == null)
            {
                Debug.Log("Name not filled");
            }
            if (_companyString == null)
            {
                Debug.Log("Company not filled");
            }
            if (_speciesString == null)
            {
                Debug.Log("Species not filled");
            }
            if ((_icesString == null) && (_locationString == null))
            {
                Debug.Log("You must fill eithre ices rectangle or locations");

            }
            if ((_icesString != null) && (_locationString != null))
            {
                Debug.Log("You must fill one or other but nor both");
            }
            if (_productionWk.value == 0)
            {
                Debug.Log("You must input production week");

            }
            if ((DayDrop.value == 0) || (MonthDrop.value == 0) || (YearDrop.value == 0))
            {
                Debug.Log("Date is not filled");
            }
            //|| (_sampleDateString == null)
            /*  if (_sampleDateString == null)
              {
                  Debug.Log("You must input sample date");
              }
return true;
        }
        else
{
    return false;
}
    }
    private void SetValues()
{
    _nameString = null;
    _companyString = null;
    // _sampleDateString = null;
    _commentsString = null;
    _speciesString = null;
    _icesString = null;
    _locationString = null;
    date = null;
    if (_name.text != "")
    {
        _nameString = _name.text;
    }
    if (_company.text != "")
    {
        _companyString = _company.text;
    }
    if (_comments.text != null)
    {
        _commentsString = _comments.text;
    }
    /*  if (_sampleDate.text != "")
      {
          _sampleDateString = _sampleDate.text;
      }
    if (_species.value != 0)
    {
        _speciesString = _species.options[_species.value].text;
    }

    if (_iceRectangle.value != 0)
    {
        _icesString = _iceRectangle.options[_iceRectangle.value].text;
    }
    if (_sampleLocationName.value != 0)
    {
        _locationString = _sampleLocationName.options[_sampleLocationName.value].text;
    }
}

/// <summary>
/// not perfect --- what about when you change date  on phone????
/// </summary>
/// <returns></returns>
private bool IsDateValid()
{
    day = DayDrop.options[DayDrop.value].text;
    month = MonthDrop.options[MonthDrop.value].text;
    year = YearDrop.options[YearDrop.value].text;
    date = year + "-" + month + "-" + day;
    try
    {
        var datetime = DateTime.Parse(date);
        Debug.Log(datetime);
        DateTime local = DateTime.Now;
        // Debug.Log(datetime + "  but todays date is "+local);
        int result = DateTime.Compare(datetime, local);
        if (result > 0)
        {
            Debug.Log(datetime + " is later than " + local + ". Please enter valid date");
            return false;
        }

        return true;
    }
    catch (Exception e)
    {
        Debug.Log(e);
        Debug.Log("Date Check failed");
        return false;
    }


}
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
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  if (_name.text == "") {
           Debug.Log("Name is emptyy");
           }
       //temporary way to ensure null instead of  '-none-' value from input form
       if (_iceRectangle.value != 0)
       {
           _icesString = _iceRectangle.options[_iceRectangle.value].text;
       }
       if(_sampleLocationName.value != 0)
       {
           _locationString = _sampleLocationName.options[_sampleLocationName.value].text;
       }*/

//        || (_companyString == null) || (_speciesString == null) ||
//     (((_icesString == null) && (_locationString == null)) || ((_icesString != null) && (_locationString != null))))



/*
 * 
 * 
 * 
 * 
 * 
 * 
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;

public class SetSampleData : MonoBehaviour
{
    [SerializeField] private string _samplePath = "sample_test/one_sample";
    //Required Fields
    [SerializeField] private InputField _name;
    [SerializeField] private InputField _company;
    [SerializeField] private InputField _sampleDate;
    [SerializeField] private Dropdown _productionWk;
    [SerializeField] private Dropdown _species;
    //either but not both
    [SerializeField] private Dropdown _iceRectangle;
    [SerializeField] private Dropdown _sampleLocationName;
    //Not Required
    [SerializeField] private InputField _comments;
  
    [SerializeField] private Button _submitButton;

    private string _nameString = null;
    private string _companyString = null;
    private string _sampleString = null;
    private string _commentsString = null;
    private string _speciesString = null;
    //  private int _productionWkString = NaN;
    private string _icesString = null;
    private string _locationString = null;

    void Start()
    {
        _submitButton.onClick.AddListener(() =>
        {
            if (_name.text == null) {
                Debug.Log("Name is emptyy");
                }
            //temporary way to ensure null instead of  '-none-' value from input form
            if (_iceRectangle.value != 0)
            {
                _icesString = _iceRectangle.options[_iceRectangle.value].text;
            }
            if(_sampleLocationName.value != 0)
            {
                _locationString = _sampleLocationName.options[_sampleLocationName.value].text;
            }
            var sampleData = new SampleData
            {
                Species = _species.options[_species.value].text,
                IcesRectangleNo = _icesString,
                //_iceRectangle.options[_iceRectangle.value].text,
                Company = _company.text,
                Date = _sampleDate.text,
                Name = _name.text,
                ProductionWeekNo = int.Parse(_productionWk.options[_productionWk.value].text),
                SampleLocationName = _locationString
            };
            var firestore = FirebaseFirestore.DefaultInstance;
            firestore.Document(_samplePath).SetAsync(sampleData); //,SetOptions.MergeAll);
            firestore.Collection("Samples").Document().SetAsync(sampleData);//you work for random ID generartion

            //firestore.Document(_samplePath).SetAsync(sampleData); overrides document



        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

 * 
 * 
 */

//  [SerializeField] private TMP_InputField _pop_Up;
//Required Fields
//[SerializeField] private  TMP_InputField _name;
//[SerializeField] private TMP_InputField _company;
////[SerializeField] private TMP_InputField _sampleDate;
//[SerializeField] private TMP_Dropdown _productionWk;
//[SerializeField] private TMP_Dropdown _species;
////date fields
//[SerializeField] private TMP_Dropdown DayDrop;
//[SerializeField] private TMP_Dropdown MonthDrop;
//[SerializeField] private TMP_Dropdown YearDrop;
////either but not both
//[SerializeField] private TMP_Dropdown _iceRectangle;
//[SerializeField] private TMP_Dropdown _sampleLocationName;
////Not Required
//[SerializeField] private TMP_InputField _comments;

//[SerializeField] private Button _submitButton;


//private TMP_InputField _name;
//private TMP_InputField _company;
////[SerializeField] private TMP_InputField _sampleDate;
//private TMP_Dropdown _productionWk;
//private TMP_Dropdown _species;
////date fields
//private TMP_Dropdown DayDrop;
//private TMP_Dropdown MonthDrop;
//private TMP_Dropdown YearDrop;
////either but not both
//private TMP_Dropdown _iceRectangle;
//private TMP_Dropdown _sampleLocationName;
////Not Required
//private TMP_InputField _comments;
//private Button _submitButton;


////SMALL
//[SerializeField] private TMP_InputField _name_sml;
//[SerializeField] private TMP_InputField _company_sml;
////[SerializeField] private TMP_InputField _sampleDate;
//[SerializeField] private TMP_Dropdown _productionWk_sml;
//[SerializeField] private TMP_Dropdown _species_sml;
////date fields
//[SerializeField] private TMP_Dropdown DayDrop_sml;
//[SerializeField] private TMP_Dropdown MonthDrop_sml;
//[SerializeField] private TMP_Dropdown YearDrop_sml;
////either but not both
//[SerializeField] private TMP_Dropdown _iceRectangle_sml;
//[SerializeField] private TMP_Dropdown _sampleLocationName_sml;
////Not Required
//[SerializeField] private TMP_InputField _comments_sml;

//[SerializeField] private Button _submitButton_sml;


////LARGE
//[SerializeField] private TMP_InputField _name_lrg;
//[SerializeField] private TMP_InputField _company_lrg;
////[SerializeField] private TMP_InputField _sampleDate;
//[SerializeField] private TMP_Dropdown _productionWk_lrg;
//[SerializeField] private TMP_Dropdown _species_lrg;
////date fields
//[SerializeField] private TMP_Dropdown DayDrop_lrg;
//[SerializeField] private TMP_Dropdown MonthDrop_lrg;
//[SerializeField] private TMP_Dropdown YearDrop_lrg;
////either but not both
//[SerializeField] private TMP_Dropdown _iceRectangle_lrg;
//[SerializeField] private TMP_Dropdown _sampleLocationName_lrg;
////Not Required
//[SerializeField] private TMP_InputField _comments_lrg;

//[SerializeField] private Button _submitButton_lrg;

//void Start()
//{
//savedSamples = new List<SampleData>();

//_submitButton.onClick.AddListener(() =>
//{
//    //inelegant solution to ensure values are coorect
//    //inelegant solution to set empty calue to null 
//    //still need to resolve production week
//    SetValues();
//    if (!IsValuesMissing() && IsDateValid())
//    {
//        var sampleData = new SampleData
//        {
//            Species = _speciesString,
//            IcesRectangleNo = _icesString,
//            //_iceRectangle.options[_iceRectangle.value].text,
//            Company = _companyString,
//            Date = date,//_sampleDateString,
//            Name = _nameString,
//            ProductionWeekNo = int.Parse(_productionWk.options[_productionWk.value].text),
//            SampleLocationName = _locationString,
//            Comment = _commentsString

//        };
//        var firestore = FirebaseFirestore.DefaultInstance;
//        firestore.Document(_samplePath).SetAsync(sampleData); //,SetOptions.MergeAll);
//        firestore.Collection("Samples").Document().SetAsync(sampleData);//you work for random ID generartion
//        Debug.Log("Success");

//        //firestore.Document(_samplePath).SetAsync(sampleData); overrides document
//    }


//});

//  }
