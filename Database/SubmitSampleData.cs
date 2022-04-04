using Firebase.Auth;
using Firebase.Firestore;
using Submit.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

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
        if (!IsValuesMissing())
        {
            userDAO = new UserDAO();
            sampleDAO = new SampleDAO();
            var sample = NewSample();
            sampleDAO.AddSample(sample);
            SaveData.Instance.AddToSubmittedSamples(sample);
            SaveData.Instance.SaveSubmittedSamples();
            canvasManager.OnSubmitClearFields();

            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                sampleDAO.AddSampleToUserCollection(auth.CurrentUser, sample);
                userDAO.UpdateUserSampleCount(auth.CurrentUser); 
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
            List<Sample> storedSamples = SaveData.Instance.GetUserStoredSamples();
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
        if (!IsValuesMissing())
        {
            var sample = NewSample();
            SaveData.Instance.AddToStoredSamples(sample);
            canvasManager.OnSubmitClearFields();
            canvasManager.DisplayPopUP("\n\nSuccessfully Stored Sample");

        }
        SaveData.Instance.SaveStoredSamples();

    }

    private Sample NewSample()
    {
        Sample sample = new Sample
        {
            Species = _speciesString,
            IcesRectangleNo = _icesString,
            Company = _companyString,
            Date = date,
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
        
        if (!missingValues.Equals(""))
        {
            missingValues = ( "<b>Incorrect Input Format: </b>\n\n" +missingValues);
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
