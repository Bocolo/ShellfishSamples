using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using System;


public class SetSampleData : MonoBehaviour
{
    [SerializeField] private string _samplePath = "sample_test/one_sample";
    //Required Fields
    [SerializeField] private InputField _name;
    [SerializeField] private InputField _company;
    [SerializeField] private InputField _sampleDate;
    [SerializeField] private Dropdown _productionWk;
    [SerializeField] private Dropdown _species;
    //date fields
    [SerializeField] private Dropdown DayDrop;
    [SerializeField] private Dropdown MonthDrop;
    [SerializeField] private Dropdown YearDrop;
    //either but not both
    [SerializeField] private Dropdown _iceRectangle;
    [SerializeField] private Dropdown _sampleLocationName;
    //Not Required
    [SerializeField] private InputField _comments;
  
    [SerializeField] private Button _submitButton;

    private string _nameString = null;
    private string _companyString = null;
    private string _sampleDateString = null;
    private string _commentsString = null;
    private string _speciesString = null;
    //  private int _productionWkString = NaN;
    private string _icesString = null;
    private string _locationString = null;

    private string date;
    private string day;
    private string month;
    private string year;

  


    void Start()
    {
        _submitButton.onClick.AddListener(() =>
        {
            //inelegant solution to ensure values are coorect
            //inelegant solution to set empty calue to null 
            //still need to resolve production week
            SetValues();
            if(!IsValuesMissing() && IsDateValid())
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
                    SampleLocationName = _locationString
                };
                var firestore = FirebaseFirestore.DefaultInstance;
                firestore.Document(_samplePath).SetAsync(sampleData); //,SetOptions.MergeAll);
                firestore.Collection("Samples").Document().SetAsync(sampleData);//you work for random ID generartion

                //firestore.Document(_samplePath).SetAsync(sampleData); overrides document
            }


        });
        
    }
    private bool IsValuesMissing()
    {
        if ((_nameString == null) || (_companyString == null) || 
            (_speciesString == null) || (_productionWk.value == 0)  ||
            ((DayDrop.value==0)||(MonthDrop.value==0)||(YearDrop.value==0))||
            (((_icesString == null) && (_locationString == null)) || ((_icesString != null) && (_locationString != null)))){
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
                  }*/
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
        _sampleDateString = null;
        _commentsString = null;
        _speciesString = null;
        _icesString = null;
        _locationString = null;
        date = null;
        if(_name.text != "")
        {
            _nameString = _name.text;
        }
        if (_company.text != "")
        {
            _companyString = _company.text;
        }
        if (_sampleDate.text != "")
        {
            _sampleDateString = _sampleDate.text;
        }
        if (_species.value!= 0)
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
                Debug.Log(datetime+" is later than "+ local +". Please enter valid date");
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

/*  if (_name.text == "") {
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