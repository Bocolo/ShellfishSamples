using App.Samples.UI;
using Samples.Data;
using System;
using UnityEngine;

namespace App.Samples.Validation
{
    /// <summary>
    /// This class is for validation of samples from the _canvasManager inputs
    /// </summary>
    public class SampleValidator : MonoBehaviour
    {

        private string _name = null;
        private string _company = null;
        private string _comments = null;
        private string _species = null;
        private string _icesRectangle = null;
        private string _location = null;
        private string _date = null;
        private SubmitCanvasManager _canvasManager;
        private SampleLogic _sampleLogic;


        /// <summary>
        /// Called on awake.  Sets the submitcanvasManager
        /// </summary>
        private void Awake()
        {
            _canvasManager = GetComponent<SubmitCanvasManager>();
            _sampleLogic = new SampleLogic();
        }
        #region "Sample generator"
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
        #endregion
        #region "Sample Value Validation"
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
            missing = _sampleLogic.MissingCompany(missing, _company);
            missing = _sampleLogic.MissingName(missing, _name);
            missing = _sampleLogic.MissingSpecies(missing, _species);
            missing = _sampleLogic.MissingOrDualLocation(missing, _icesRectangle, _location);
            missing = _sampleLogic.MissingDate(missing, _date);
            missing = _sampleLogic.MissingProductionWeek(missing, _canvasManager.ProductionWk.value);
            if (!missing.Equals(""))
            {
                missing = ("<b>Incorrect Input Format: </b>\n\n" + missing);
            }
            return missing;
        }
        #endregion
        #region "Setting values from canvas"
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
                this._date = _sampleLogic.GetDate(
                    _canvasManager.DayDrop.options[_canvasManager.DayDrop.value].text,
         _canvasManager.MonthDrop.options[_canvasManager.MonthDrop.value].text,
         _canvasManager.YearDrop.options[_canvasManager.YearDrop.value].text);
            }
            else
            {
                this._date = null;
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
        #endregion
    }
}