using Submit.UI;
using System;
using UnityEngine;
public class SubmitSampleUI : MonoBehaviour
{
  private SubmitCanvasManager canvasManager;
    private string _nameString = null;
    private string _companyString = null;
    private string _commentsString = null;
    private string _speciesString = null;
    private string _icesString = null;
    private string _locationString = null;
    private string date =null;
    /// <summary>
    /// /SERPERATE ALL THIS, ALL STRING GET AND SET IN OWN CLASS
    /// 
    /// SAMPLEDETAILS
    /// </summary>
    private string missingValues = "";
    private void Awake()
    {
        canvasManager = GetComponent<SubmitCanvasManager>();
    }
    public void CompleteSubmission()
    {
        canvasManager.OnSubmitClearFields();
        canvasManager.DisplayPopUP("\n\nSuccessfully Submitted Sample");// this is wrinfg -- can onlt reeally be successful is only
    }
    public Sample NewSample()
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
    public bool IsValuesMissing()
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
    public void SetValues()
    {
  //cOULS DO IF ELSES-- SET NUL IN THE ELSES
        SetCompany(null);
        SetComment(null);
        SetIcesRectangle(null);
        SetSpecies(null);
        SetLocation(null);
        SetDate(null);
        if (canvasManager._name.text != "")
        {
            SetName( canvasManager._name.text);
        }
        else
        {
            SetName(null);
        }
        if (canvasManager._company.text != "")
        {
            SetCompany(canvasManager._company.text);
        }
        else
        {
            SetCompany(null);
        }
        if (canvasManager._comments.text != null)
        {
           SetComment( canvasManager._comments.text);
        }
        else
        {
            SetComment(null);
        }
        if (canvasManager._species.value != 0)
        {
            SetSpecies( canvasManager._species.options[canvasManager._species.value].text);
        }
        else
        {
            SetSpecies(null);
        }
        if (canvasManager._iceRectangle.value != 0)
        {
            SetIcesRectangle( canvasManager._iceRectangle.options[canvasManager._iceRectangle.value].text);
        }
        else
        {
            SetIcesRectangle(null);
        }
        if (canvasManager._sampleLocationName.value != 0)
        {
            SetLocation(  canvasManager._sampleLocationName.options[canvasManager._sampleLocationName.value].text);
        }
        else
        {
            SetLocation(null);
        }
        SetDate(null);
    }
    public bool IsDateValid()
    {
        SetDate(canvasManager.DayDrop.options[canvasManager.DayDrop.value].text,
           canvasManager.MonthDrop.options[canvasManager.MonthDrop.value].text,
           canvasManager.YearDrop.options[canvasManager.YearDrop.value].text);
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
    public bool IsDateValid(String date)
    {
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
    private void SetName(String name)
    {
        this._nameString = name;
    }
    private void SetCompany(String company)
    {
        this._companyString = company;
    }
    private void SetComment(String comment)
    {
        this._commentsString = comment;
    }
    private void SetSpecies(String species)
    {
        this._speciesString = species;
    }
    private void SetIcesRectangle(String icesRectangle)
    {
        this._icesString = icesRectangle;
    }
    private void SetLocation(String location)
    {
        this._locationString = location;
    }
    private void SetDate(String day,String month, String year)
    {
        date = year + "-" + month + "-" + day;
    }
    private void SetDate(String date)
    {
        this.date = date;
    }
}
