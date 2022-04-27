using System;
using UnityEngine;
namespace UI.Submit
{
    /// <summary>
    /// This class is for validation of samples from the _canvasManager inputs
    /// </summary>
    public class SampleValidator : MonoBehaviour
    {
        private SubmitCanvasManager _canvasManager;
        private string _name = null;
        private string _company = null;
        private string _comments = null;
        private string _species = null;
        private string _icesRectangle = null;
        private string _location = null;
        private string _date = null;
    
        /// <summary>
        /// Called on awake.  Sets the submitcanvasManager
        /// </summary>
        private void Awake()
        {
            _canvasManager = GetComponent<SubmitCanvasManager>();
        }
 
        /// <summary>
        /// Creates and returns a new samples using the submit canvas manager inputs
        /// </summary>
        /// <returns></returns>
        public Sample NewSample()
        {
            Sample sample = new Sample
            {
                Species = _species,
                IcesRectangleNo = _icesRectangle,
                Company = _company,
                Date = _date,
                Name = _name,
                ProductionWeekNo = int.Parse(_canvasManager.ProductionWk.options[_canvasManager.ProductionWk.value].text),
                SampleLocationName = _location,
                Comment = _comments
            };
            return sample;
        }
        /// <summary>
        /// checks submit canvas manager inputs and returns a bool indicating if values are valid
        /// </summary>
        /// <returns>bool representing validity of inputs</returns>
        public bool ValidateValues()
        {
            SetValues();
            return IsValuesComplete();
        }
        /// <summary>
        /// Checks if the local _date field strings is a valid data
        /// </summary>
        /// <returns>bool of date validity</returns>
        public bool IsDateValid()
        {
            try
            {
                DateTime datetime = DateTime.Parse(_date);
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
                Debug.Log("Date Check failed: "+e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// Checks passed date  string is a valid data
        /// </summary>
        /// <returns>bool of date validity</returns>
        public bool IsDateValid(String date)
        {
            try
            {
                DateTime datetime = DateTime.Parse(date);
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
                Debug.Log("Date Check failed: " + e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// Sets the local string values to the canvas manager inputs
        /// </summary>
        private void SetValues()
        {
            SetNameToCanvas();
            SetCompanyToCanvas();
            SetCommentToCanvas();
            SetSpeciesToCanvas();
            SetIcesRectangleToCanvas();
            SetLocationToCanvas();
            SetDateToCanvas();
        }
        /// <summary>
        /// Checks missing values and returns a bool to notify is missing values present
        /// if missing values present activate a pop with missing value details
        /// </summary>
        /// <returns></returns>
        private bool IsValuesComplete()
        {
            String missingValues = MissingValues();
            if (!missingValues.Equals(""))
            {
                _canvasManager.MissingValuePopup(missingValues);
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Checks each local string field for missing values and
        /// populates a string with the details of the missing values
        /// </summary>
        /// <returns>the missing values string</returns>
        private String MissingValues()
        {
            String missing = "";
            missing = MissingCompany(missing);
            missing = MissingName(missing);
            missing = MissingSpecies(missing);
            missing = MissingOrDualLocation(missing);
            missing = MissingDate(missing);
            missing = MissingProductionWeek(missing);
            if (!missing.Equals(""))
            {
                missing = ("<b>Incorrect Input Format: </b>\n\n" + missing);
            }
            return missing;
        }
        /// <summary>
        /// If the _date field is not a valid date, modifies the missing values string before
        /// returing the string
        /// </summary>
        /// <param name="missingValues">the string to modify</param>
        /// <returns></returns>
        private String MissingDate(String missingValues)
        {
            if (!IsDateValid())
            {
                missingValues += "Please enter a valid date\n";
            }
            return missingValues;
        }
        /// <summary>
        /// if name field is null, modifies the  passed missing value string
        /// 
        /// returns the missing value string
        /// </summary>
        /// <param name="missingValues"></param>
        /// <returns></returns>
        private String MissingName(String missingValues)
        {
            if (_name == null)
            {
                missingValues += "Please enter a name\n";
            }
            return missingValues;
        }
        /// <summary>
        /// if company field is null, modifies the  passed missing value string
        /// 
        /// returns the missing value string
        /// </summary>
        /// <param name="missingValues"></param>
        /// <returns></returns>
        private String MissingCompany(String missingValues)
        {
            if (_company == null)
            {
                missingValues += "Please enter a company name\n";
            }
            return missingValues;
        }
        /// <summary>
        /// if specied field is null, modifies the  passed missing value string
        /// 
        /// returns the missing value string
        /// </summary>
        /// <param name="missingValues"></param>
        /// <returns></returns>
        private String MissingSpecies(String missingValues)
        {
            if (_species == null)
            {
                missingValues += "Please enter the shellfish species\n";
            }
            return missingValues;
        }
        /// <summary>
        /// if no ices string and location string are both null 
        /// or
        /// if both are not null
        ///modifies the  passed missing value string with error details
        /// 
        /// returns the missing value string
        /// </summary>
        /// <param name="missingValues"></param>
        /// <returns></returns>
        private String MissingOrDualLocation(String missingValues)
        {
            if (((_icesRectangle == null) && (_location == null)) || ((_icesRectangle != null) && (_location != null)))
            {
                missingValues += "You must enter <i>either</i> a Sample Location Date or an Ices Rectangle No.\n";
            }
            return missingValues;
        }
        /// <summary>
        /// if _canvasManager.ProductionWk.value is not set, modifies the  passed missing value string
        /// 
        /// returns the missing value string
        /// </summary>
        /// <param name="missingValues"></param>
        /// <returns></returns>
        private String MissingProductionWeek(String missingValues)
        {
            if (_canvasManager.ProductionWk.value == 0)
            {
                missingValues += "Please enter the production week\n";
            }
            return missingValues;
        }
        /// <summary>
        /// Sets the _date string with details of passed params
        /// </summary>
        /// <param name="day">string representing day</param>
        /// <param name="month">string representing month</param>
        /// <param name="year">string representing year</param>
        private void SetDate(String day, String month, String year)
        {
            _date = year + "-" + month + "-" + day;
        }
        /// <summary>
        /// Sets the name to canvas input if value isnt empty
        /// or sets the name to null
        /// </summary>
        private void SetNameToCanvas()
        {
            if (_canvasManager.Name.text != "")
            {
                this._name = (_canvasManager.Name.text);
            }
            else
            {
                this._name = (null);
            }
        }
        /// <summary>
        /// Sets the Company to canvas input if value isnt empty
        /// or sets the Company to null
        /// </summary>
        private void SetCompanyToCanvas()
        {
            if (_canvasManager.Company.text != "")
            {
                this._company = (_canvasManager.Company.text);
            }
            else
            {
                this._company = (null);
            }
        }
        /// <summary>
        /// Sets the Comment to canvas input if value isnt empty
        /// or sets the Comment to null
        /// </summary>
        private void SetCommentToCanvas()
        {
            if (_canvasManager.Comments.text != null)
            {
                this._comments = (_canvasManager.Comments.text);
            }
            else
            {
                this._comments = (null);
            }
        }
        /// <summary>
        /// Sets the Species to canvas input if value isnt empty
        /// or sets the Species to null
        /// </summary>
        private void SetSpeciesToCanvas()
        {
            if (_canvasManager.Species.value != 0)
            {
                this._species = (_canvasManager.Species.options[_canvasManager.Species.value].text);
            }
            else
            {
                this._species = (null);
            }
        }
        /// <summary>
        /// Sets the IceRectangle to canvas input if value isnt empty
        /// or sets the IceRectangle to null
        /// </summary>
        private void SetIcesRectangleToCanvas()
        {
            if (_canvasManager.IceRectangle.value != 0)
            {
                this._icesRectangle = (_canvasManager.IceRectangle.options[_canvasManager.IceRectangle.value].text);
            }
            else
            {
                this._icesRectangle = (null);
            }
        }
        /// <summary>
        /// Sets the Date to the day, month and year  canvas inputs if values arent empty
        /// or sets the _date to null
        /// </summary>
        private void SetDateToCanvas()
        {
            if ((_canvasManager.DayDrop.value != 0)
                && (_canvasManager.MonthDrop.value != 0)
                && (_canvasManager.YearDrop.value != 0))
            {
                SetDate(_canvasManager.DayDrop.options[_canvasManager.DayDrop.value].text,
         _canvasManager.MonthDrop.options[_canvasManager.MonthDrop.value].text,
         _canvasManager.YearDrop.options[_canvasManager.YearDrop.value].text);
            }
            else
            {
                this._date=null;
            }
        }
        /// <summary>
        /// Sets the SampleLocationName to canvas input if value isnt empty
        /// or sets the SampleLocationName to null
        /// </summary>
        private void SetLocationToCanvas()
        {
            if (_canvasManager.SampleLocationName.value != 0)
            {
                this._location = (_canvasManager.SampleLocationName.options[_canvasManager.SampleLocationName.value].text);
            }
            else
            {
                this._location = (null);
            }
        }
    }
}
/* "";
missingValues += MissingCompany(missingValues);
missingValues += MissingName(missingValues);
missingValues += MissingSpecies(missingValues);
missingValues += MissingOrDualLocation(missingValues);
missingValues += MissingDate(missingValues);
missingValues += MissingProductionWeek(missingValues);*/
/*     if (_nameString == null)
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
     }*/