using App.SaveSystem.Manager;
using App.Settings.Prefrences;
using App.UI;
using System;
using TMPro;
using UnityEngine;
using Users.Data;

namespace App.Samples.UI
{
    /// <summary>
    /// 
    /// Manages the Canvas' on the submission page :
    /// controlling which game objects are active and extracting their input
    /// </summary>
    public class SubmitCanvasManager : MonoBehaviour
    {
        #region "Variables"
        public TMP_InputField Name { get; private set; }
        public TMP_InputField Company { get; private set; }
        public TMP_InputField Comments { get; private set; }
        public PopUp SubmissionPopUp { get; private set; }
        public PopUp MissingValuesPopUp { get; private set; }
        public TMP_Dropdown ProductionWk { get; private set; }
        public TMP_Dropdown Species { get; private set; }
        public TMP_Dropdown DayDrop { get; private set; }
        public TMP_Dropdown MonthDrop { get; private set; }
        public TMP_Dropdown YearDrop { get; private set; }
        public TMP_Dropdown IceRectangle { get; private set; }
        public TMP_Dropdown SampleLocationName { get; private set; }
        
        //SMALL CANVAS
        [SerializeField] public GameObject _smallCanvas;
        [SerializeField] private TMP_InputField _nameSmall;
        [SerializeField] private TMP_InputField _companySmall;
        [SerializeField] private TMP_InputField _commentsSmall;
        [SerializeField] private PopUp _submissionPopUpSmall;
        [SerializeField] private PopUp _missingValuesPopUpSmall;
        [SerializeField] private TMP_Dropdown _productionWkSmall;
        [SerializeField] private TMP_Dropdown _speciesSmall;
        [SerializeField] private TMP_Dropdown _dayDropSmall;
        [SerializeField] private TMP_Dropdown _monthDropSmall;
        [SerializeField] private TMP_Dropdown _yearDropSmall;
        [SerializeField] private TMP_Dropdown _iceRectangleSmall;
        [SerializeField] private TMP_Dropdown _sampleLocationNameSmall;
        //LARGE CANVAS
        [SerializeField] private GameObject _largeCanvas;
        [SerializeField] private TMP_InputField _nameLarge;
        [SerializeField] private TMP_InputField _companyLarge;
        [SerializeField] private PopUp _missingValuesPopUpLarge;
        [SerializeField] private TMP_InputField _commentsLarge;
        [SerializeField] private PopUp _submissionPopUpLarge;
        [SerializeField] private TMP_Dropdown _productionWkLarge;
        [SerializeField] private TMP_Dropdown _speciesLarge;
        [SerializeField] private TMP_Dropdown _dayDropLarge;
        [SerializeField] private TMP_Dropdown _monthDropLarge;
        [SerializeField] private TMP_Dropdown _yearDropLarge;
        [SerializeField] private TMP_Dropdown _iceRectangleLarge;
        [SerializeField] private TMP_Dropdown _sampleLocationNameLarge;


        #endregion
        /// <summary>
        /// On start: Activates the canvas based user prefs
        /// Sets the name and company field with profile details
        /// </summary>
        private void Awake()
        {
            if (UserPrefs.GetPreferredCanvas().Equals("Small"))
            {
                SetCanvasSmall(true);
            }
            else
            {
                SetCanvasSmall(false);
            }
            SetNameAndCompanyFromProfile();
        }
   

        #region "canvas field default settings"
        /// <summary>
        /// Resets all of the active canvas fields
        /// sets the name and company fields to profile details
        /// </summary>
        private void OnSubmitResetFields()
        {
            Comments.text = "";
            Species.value = 0;
            IceRectangle.value = 0;
            SampleLocationName.value = 0;
            ProductionWk.value = 0;
            DayDrop.value = 0;
            MonthDrop.value = 0;
            YearDrop.value = 0;
            SetNameAndCompanyFromProfile();
        }
        /// <summary>
        /// Loads the user profile and sets the canvas 
        /// name and compnay to the profile details
        /// </summary>
        private void SetNameAndCompanyFromProfile()
        {
            User user = SaveData.Instance.LoadUserProfile();
            Name.text = user.Name;
            Company.text = user.Company;
        }
        #endregion
        #region "sample submission functions"
        /// <summary>
        ///  Resets all of the active canvas fields
        ///  and activates the submission pop up with SuccessfulSubmission details
        /// </summary>
        public void CompleteSubmission()
        {
            OnSubmitResetFields();
            SubmissionPopUp.SuccessfulSubmission();
        }
        /// <summary>
        ///  Resets all of the active canvas fields
        ///  and activates the submission pop up with SuccessfulStorage details
        /// </summary>
        public void CompleteStore()
        {
            OnSubmitResetFields();
            SubmissionPopUp.SuccessfulStorage();
        }
        /// <summary>
        /// activates the missing value pop up with the passed tect
        /// </summary>
        /// <param name="missingValues">missingValue details</param>
        public void MissingValuePopup(String missingValues)
        {
            MissingValuesPopUp.SetPopUpText(missingValues);
        }
        #endregion
        #region "canvas input functions"
        /// <summary>
        /// Swicthes the active input fields by canvas
        /// bassed on the isSmall bool
        /// </summary>
        /// <param name="isSmall">informs whether new canvas should be small or large</param>
        private void SwitchInputFields(bool isSmall)
        {
            if (isSmall)
            {
                Name = _nameSmall;
                Company = _companySmall;
                ProductionWk = _productionWkSmall;
                Species = _speciesSmall;
                DayDrop = _dayDropSmall;
                MonthDrop = _monthDropSmall;
                YearDrop = _yearDropSmall;
                IceRectangle = _iceRectangleSmall;
                SampleLocationName = _sampleLocationNameSmall;
                Comments = _commentsSmall;
                MissingValuesPopUp = _missingValuesPopUpSmall;
                SubmissionPopUp = _submissionPopUpSmall;
            }
            else
            {
                Name = _nameLarge;
                Company = _companyLarge;
                ProductionWk = _productionWkLarge;
                Species = _speciesLarge;
                DayDrop = _dayDropLarge;
                MonthDrop = _monthDropLarge;
                YearDrop = _yearDropLarge;
                IceRectangle = _iceRectangleLarge;
                SampleLocationName = _sampleLocationNameLarge;
                Comments = _commentsLarge;
                MissingValuesPopUp = _missingValuesPopUpLarge;
                SubmissionPopUp = _submissionPopUpLarge;
            }
        }
        /// <summary>
        /// updates the active input fields to the inactive canvas inputs
        /// based on the isSmall bool
        /// 
        /// when a canvas swicthes, the input details of the previous canvas are 
        /// added to the new canvas. Allowing user to switch between canvas without re-entering
        /// input details
        /// </summary>
        /// <param name="isSmall">informs whether new canvas should be small or large</param>
        private void SwicthInputValues(bool isSmall)
        {
            if (!isSmall)
            {
                Name.text = _nameSmall.text;
                Company.text = _companySmall.text;
                ProductionWk.value = _productionWkSmall.value;
                Species.value = _speciesSmall.value;
                DayDrop.value = _dayDropSmall.value;
                MonthDrop.value = _monthDropSmall.value;
                YearDrop.value = _yearDropSmall.value;
                IceRectangle.value = _iceRectangleSmall.value;
                SampleLocationName.value = _sampleLocationNameSmall.value;
                Comments.text = _commentsSmall.text;
            }
            else
            {
                Name.text = _nameLarge.text;
                Company.text = _companyLarge.text;
                ProductionWk.value = _productionWkLarge.value;
                Species.value = _speciesLarge.value;
                DayDrop.value = _dayDropLarge.value;
                MonthDrop.value = _monthDropLarge.value;
                YearDrop.value = _yearDropLarge.value;
                IceRectangle.value = _iceRectangleLarge.value;
                SampleLocationName.value = _sampleLocationNameLarge.value;
                Comments.text = _commentsLarge.text;
            }
        }
        #endregion
        #region " canvas activator functions"
        /// <summary>
        /// 
        /// Switches the active canvas and updates the canvas user prefs
        /// </summary>
        public void SwitchCanvas()
        {
            if (_smallCanvas.activeInHierarchy)
            {
                SetCanvasSmall(false);
                UserPrefs.SetPreferredCanvas("Large");
            }
            else
            {
                SetCanvasSmall(true);
                UserPrefs.SetPreferredCanvas("Small");
            }
        }
        /// <summary>
        /// Activate the appropriate canvas and updates its values
        /// based on the isSmall bool
        /// </summary>
        /// <param name="isSmall">informs whether canvas should be large or small</param>
        private void SetCanvasSmall(bool isSmall)
        {
            _largeCanvas.SetActive(!isSmall);
            _smallCanvas.SetActive(isSmall);
            SwitchInputFields(isSmall);
            SwicthInputValues(isSmall);
        }
        #endregion
        #region "Canvas getters"
        /// <summary>
        /// small canvas getter
        /// </summary>
        /// <returns>the small canvas game object</returns>
        public GameObject GetSmallCanvas()
        {
            return _smallCanvas;
        }
        /// <summary>
        /// large canvas getter
        /// </summary>
        /// <returns>the large canvas game object</returns>
        public GameObject GetLargeCanvas()
        {
            return _largeCanvas;
        }
        #endregion
    }
}
