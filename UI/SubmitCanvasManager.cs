using Save.Manager;
using System;
using TMPro;
using UnityEngine;

using UI.Popup;
namespace UI.Submit
{
    public class SubmitCanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject SmallCanvas;
        [SerializeField] private GameObject LargeCanvas;
        public TMP_InputField Name { get; private set; }
        public TMP_InputField Company { get; private set; }
        public TMP_InputField Comments { get; private set; }
        public PopUp MissingValuesPopUp { get; private set; }
        public TMP_Dropdown ProductionWk { get; private set; }
        public TMP_Dropdown Species { get; private set; }
        public TMP_Dropdown DayDrop { get; private set; }
        public TMP_Dropdown MonthDrop { get; private set; }
        public TMP_Dropdown YearDrop { get; private set; }
        public TMP_Dropdown IceRectangle { get; private set; }
        public TMP_Dropdown SampleLocationName { get; private set; }
        //SMALL CANVAS
        [SerializeField] private TMP_InputField _name_sml;
        [SerializeField] private TMP_InputField _company_sml;
        [SerializeField] private PopUp _missingValuesPopUp_sml;
        [SerializeField] private TMP_Dropdown _productionWk_sml;
        [SerializeField] private TMP_Dropdown _species_sml;
        [SerializeField] private TMP_Dropdown DayDrop_sml;
        [SerializeField] private TMP_Dropdown MonthDrop_sml;
        [SerializeField] private TMP_Dropdown YearDrop_sml;
        [SerializeField] private TMP_Dropdown _iceRectangle_sml;
        [SerializeField] private TMP_Dropdown _sampleLocationName_sml;
        [SerializeField] private TMP_InputField _comments_sml;
        //LARGE CANVAS
        [SerializeField] private TMP_InputField _name_lrg;
        [SerializeField] private TMP_InputField _company_lrg;
        [SerializeField] private PopUp _missingValuesPopUp_lrg;
        [SerializeField] private TMP_Dropdown _productionWk_lrg;
        [SerializeField] private TMP_Dropdown _species_lrg;
        [SerializeField] private TMP_Dropdown DayDrop_lrg;
        [SerializeField] private TMP_Dropdown MonthDrop_lrg;
        [SerializeField] private TMP_Dropdown YearDrop_lrg;
        [SerializeField] private TMP_Dropdown _iceRectangle_lrg;
        [SerializeField] private TMP_Dropdown _sampleLocationName_lrg;
        [SerializeField] private TMP_InputField _comments_lrg;

        [SerializeField] private PopUp submissionPopUp_sml;//{ get; private set; }
        [SerializeField] private PopUp submissionPopUp_lrg;//{ get; private set; }
        public PopUp SubmissionPopUp { get; private set; }
        private void Awake()
        {
            if (UserPrefs.GetPreferredCanvas().Equals("Small"))
            {
                SetCanvasSmall(true);
                Debug.Log("Seeting small");
            }
            else if (UserPrefs.GetPreferredCanvas().Equals("Large"))
            {
                SetCanvasSmall(false);
                Debug.Log("Seeting large");

            }
            else
            {
                SetCanvasSmall(false);
                Debug.Log("Seeting");

            }
            SetNameAndCompanyFromProfile();
        }
        public void MissingValuePopup(String missingValues)
        {
            MissingValuesPopUp.SetPopUpText(missingValues);
        }

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
        public void CompleteSubmission()
        {
            OnSubmitResetFields();
            // DisplaySubmissionPopUp();
            SubmissionPopUp.SuccessfulSubmission();

        }
        public void CompleteStore()
        {
            OnSubmitResetFields();
            //    DisplayStoredPopUp();
            SubmissionPopUp.SuccessfulStorage();

        }

        public void SwitchCanvas()
        {
            if (SmallCanvas.activeInHierarchy)
            {
                SetCanvasSmall(false);
                UserPrefs.SetPreferredCanvas("Large");
            }
            else
            {
                SetCanvasSmall(true);
                UserPrefs.SetPreferredCanvas("Small");

            }
          //  SetNameAndCompanyFromProfile();
        }
        private void SetNameAndCompanyFromProfile()
        {
            User user = SaveData.Instance.LoadUserProfile();
            Name.text = user.Name;
            Company.text = user.Company;
        }

        private void SwitchInputFields(bool isSmall)
        {
            if (isSmall)
            {
                Name = _name_sml;
                Company = _company_sml;
                ProductionWk = _productionWk_sml;
                Species = _species_sml;
                DayDrop = DayDrop_sml;
                MonthDrop = MonthDrop_sml;
                YearDrop = YearDrop_sml;
                IceRectangle = _iceRectangle_sml;
                SampleLocationName = _sampleLocationName_sml;
                Comments = _comments_sml;
                MissingValuesPopUp = _missingValuesPopUp_sml;
                SubmissionPopUp = submissionPopUp_sml;
            }
            else
            {
                Name = _name_lrg;
                Company = _company_lrg;
                ProductionWk = _productionWk_lrg;
                Species = _species_lrg;
                DayDrop = DayDrop_lrg;
                MonthDrop = MonthDrop_lrg;
                YearDrop = YearDrop_lrg;
                IceRectangle = _iceRectangle_lrg;
                SampleLocationName = _sampleLocationName_lrg;
                Comments = _comments_lrg;
                MissingValuesPopUp = _missingValuesPopUp_lrg;
                SubmissionPopUp = submissionPopUp_lrg;

            }
        }

        private void SwicthInputValues(bool isSmall)
        {
            if (!isSmall)
            {
                Name.text = _name_sml.text;
                Company.text = _company_sml.text;
                ProductionWk.value = _productionWk_sml.value;
                Species.value = _species_sml.value;
                DayDrop.value = DayDrop_sml.value;
                MonthDrop.value = MonthDrop_sml.value;
                YearDrop.value = YearDrop_sml.value;
                IceRectangle.value = _iceRectangle_sml.value;
                SampleLocationName.value = _sampleLocationName_sml.value;
                Comments.text = _comments_sml.text;
               
            }
            else
            {
                Name.text = _name_lrg.text;
                Company.text = _company_lrg.text;
                ProductionWk.value = _productionWk_lrg.value;
                Species.value = _species_lrg.value;
                DayDrop.value = DayDrop_lrg.value;
                MonthDrop.value = MonthDrop_lrg.value;
                YearDrop.value = YearDrop_lrg.value;
                IceRectangle.value = _iceRectangle_lrg.value;
                SampleLocationName.value = _sampleLocationName_lrg.value;
                Comments.text = _comments_lrg.text;


            }
        }
        private void SetCanvasSmall(bool isSmall)
        {
            LargeCanvas.SetActive(!isSmall);
            SmallCanvas.SetActive(isSmall);


            SwitchInputFields(isSmall);
            SwicthInputValues(isSmall);
        }
    }
}
