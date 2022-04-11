
using System;
using UnityEngine;
namespace UI.Submit
{
    public class SubmitSampleUI : MonoBehaviour
    {
        private SubmitCanvasManager canvasManager;
        private string _nameString = null;
        private string _companyString = null;
        private string _commentsString = null;
        private string _speciesString = null;
        private string _icesString = null;
        private string _locationString = null;
        private string date = null;
        /// <summary>
        /// /SERPERATE ALL THIS, ALL STRING GET AND SET IN OWN CLASS
        /// 
        /// SAMPLEDETAILS
        /// </summary>
        //  private string missingValues = "";
        private void Awake()
        {
            canvasManager = GetComponent<SubmitCanvasManager>();
        }
        public void CompleteSubmission()
        {
            canvasManager.OnSubmitClearFields();
            //successfully stored vs submittes
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
        public bool ValidateValues()
        {
            SetValues();
            return IsValuesComplete();
        }



        public bool IsDateValid()
        {
            /*        SetDate(canvasManager.DayDrop.options[canvasManager.DayDrop.value].text,
                       canvasManager.MonthDrop.options[canvasManager.MonthDrop.value].text,
                       canvasManager.YearDrop.options[canvasManager.YearDrop.value].text);*/
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
                Debug.Log(e);
                Debug.Log("Date Check failed");
                return false;
            }
        }
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
                Debug.Log(e);
                Debug.Log("Date Check failed");
                return false;
            }
        }
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
        private bool IsValuesComplete()
        {
            String missingValues = MissingValues();

            if (!missingValues.Equals(""))
            {
                //    missingValues = ("<b>Incorrect Input Format: </b>\n\n" + missingValues);
                canvasManager.DisplayPopUP(missingValues);
                return false;
            }
            else
            {
                return true;
            }
        }

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

        private String MissingDate(String missingValues)
        {
            if (!IsDateValid())
            {
                missingValues += "Please enter a valid date\n";
            }
            return missingValues;
        }
        private String MissingName(String missingValues)
        {
            if (_nameString == null)
            {
                missingValues += "Please enter a name\n";
            }
            return missingValues;
        }
        private String MissingCompany(String missingValues)
        {
            if (_companyString == null)
            {
                missingValues += "Please enter a company name\n";
            }
            return missingValues;
        }
        private String MissingSpecies(String missingValues)
        {
            if (_speciesString == null)
            {
                missingValues += "Please enter the shellfish species\n";
            }
            return missingValues;
        }
        private String MissingOrDualLocation(String missingValues)
        {
            if (((_icesString == null) && (_locationString == null)) || ((_icesString != null) && (_locationString != null)))
            {
                missingValues += "You must enter <i>either</i> a Sample Location Date or an Ices Rectangle No.\n";
            }
            return missingValues;
        }
        private String MissingProductionWeek(String missingValues)
        {
            if (canvasManager._productionWk.value == 0)
            {
                missingValues += "Please enter the production week\n";
            }
            return missingValues;
        }

        private void SetDate(String day, String month, String year)
        {
            date = year + "-" + month + "-" + day;
        }
        private void SetDate(String date)
        {
            this.date = date;
        }
        private void SetNameToCanvas()
        {
            if (canvasManager._name.text != "")
            {
                this._nameString = (canvasManager._name.text);
            }
            else
            {
                this._nameString = (null);
            }
        }
        private void SetCompanyToCanvas()
        {
            if (canvasManager._company.text != "")
            {
                this._companyString = (canvasManager._company.text);
            }
            else
            {
                this._companyString = (null);
            }
        }
        private void SetCommentToCanvas()
        {
            if (canvasManager._comments.text != null)
            {
                this._commentsString = (canvasManager._comments.text);
            }
            else
            {
                this._commentsString = (null);
            }

        }
        private void SetSpeciesToCanvas()
        {
            if (canvasManager._species.value != 0)
            {
                this._speciesString = (canvasManager._species.options[canvasManager._species.value].text);
            }
            else
            {
                this._speciesString = (null);
            }
        }
        private void SetIcesRectangleToCanvas()
        {
            if (canvasManager._iceRectangle.value != 0)
            {
                this._icesString = (canvasManager._iceRectangle.options[canvasManager._iceRectangle.value].text);
            }
            else
            {
                this._icesString = (null);
            }
        }
        private void SetDateToCanvas()
        {
            if ((canvasManager.DayDrop.value != 0)
                && (canvasManager.MonthDrop.value != 0)
                && (canvasManager.YearDrop.value != 0))
            {
                SetDate(canvasManager.DayDrop.options[canvasManager.DayDrop.value].text,
         canvasManager.MonthDrop.options[canvasManager.MonthDrop.value].text,
         canvasManager.YearDrop.options[canvasManager.YearDrop.value].text);
            }
            else
            {
                SetDate(null);
            }
        }
        private void SetLocationToCanvas()
        {
            if (canvasManager._sampleLocationName.value != 0)
            {
                this._locationString = (canvasManager._sampleLocationName.options[canvasManager._sampleLocationName.value].text);
            }
            else
            {
                this._locationString = (null);
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