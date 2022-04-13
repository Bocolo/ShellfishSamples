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
            SetCanvasSmall(false);
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
            }
            else
            {
                SetCanvasSmall(true);
            }
            SetNameAndCompanyFromProfile();
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
        private void SetCanvasSmall(bool isSmall)
        {
            LargeCanvas.SetActive(!isSmall);
            SmallCanvas.SetActive(isSmall);


            SwitchInputFields(isSmall);

        }

#if UNITY_INCLUDE_TESTS
        public void SetSmallCanvasTest()
        {
        }
        public void SetUp()
        {
            GameObject go1 = new GameObject();
            GameObject go2 = new GameObject();
            GameObject go3 = new GameObject();
            GameObject go4 = new GameObject();
            GameObject go5 = new GameObject();
            GameObject go6 = new GameObject();
            GameObject go7 = new GameObject();
            GameObject go8 = new GameObject();
            GameObject go9 = new GameObject();
            GameObject go10 = new GameObject();


         //   MissingValuesPopUp = this.gameObject.AddComponent<TMP_InputField>();
            Comments = go1.AddComponent<TMP_InputField>();
            Name = go2.AddComponent<TMP_InputField>();
            Company = go3.AddComponent<TMP_InputField>();

            IceRectangle = go4.AddComponent<TMP_Dropdown>();
            DayDrop = go5.AddComponent<TMP_Dropdown>();
            MonthDrop = go6.AddComponent<TMP_Dropdown>();
            Species = go7.AddComponent<TMP_Dropdown>();
            ProductionWk = go8.AddComponent<TMP_Dropdown>();
            SampleLocationName = go9.AddComponent<TMP_Dropdown>();
            YearDrop = go10.AddComponent<TMP_Dropdown>();
        }
        public GameObject GetSmallCanvas()
        {
            return this.SmallCanvas;
        }
        public GameObject GetLargeCanvas()
        {
            return this.LargeCanvas;
        }
#endif
    }
}

/* if (isSmall)
 {

     _name = _name_sml;
     _company = _company_sml;
     _productionWk = _productionWk_sml;
     _species = _species_sml;
     DayDrop = DayDrop_sml;
     MonthDrop = MonthDrop_sml;
     YearDrop = YearDrop_sml;
     _iceRectangle = _iceRectangle_sml;
     _sampleLocationName = _sampleLocationName_sml;
     _comments = _comments_sml;
     _submitButton = _submitButton_sml;
     _pop_up = _pop_up_sml;
 }*/
/*   SmallCanvas.SetActive(isSmall);
   LargeCanvas.SetActive(!isSmall);
   if (isSmall)
   {
       _name = _name_sml;
       _company = _company_sml;
       _productionWk = _productionWk_sml;
       _species = _species_sml;
       DayDrop = DayDrop_sml;
       MonthDrop = MonthDrop_sml;
       YearDrop = YearDrop_sml;
       _iceRectangle = _iceRectangle_sml;
       _sampleLocationName = _sampleLocationName_sml;
       _comments = _comments_sml;
       _submitButton = _submitButton_sml;
       _pop_up = _pop_up_sml;
   }
   else
   {
       _name = _name_lrg;
       _company = _company_lrg;
       _productionWk = _productionWk_lrg;
       _species = _species_lrg;
       DayDrop = DayDrop_lrg;
       MonthDrop = MonthDrop_lrg;
       YearDrop = YearDrop_lrg;
       _iceRectangle = _iceRectangle_lrg;
       _sampleLocationName = _sampleLocationName_lrg;
       _comments = _comments_lrg;
       _submitButton = _submitButton_lrg;
       _pop_up = _pop_up_lrg;
   }*/
/*   if (!isSmall)
         {


             _name = _name_lrg;
             _company = _company_lrg;
             _productionWk = _productionWk_lrg;
             _species = _species_lrg;
             DayDrop = DayDrop_lrg;
             MonthDrop = MonthDrop_lrg;
             YearDrop = YearDrop_lrg;
             _iceRectangle = _iceRectangle_lrg;
             _sampleLocationName = _sampleLocationName_lrg;
             _comments = _comments_lrg;
             _submitButton = _submitButton_lrg;
             _pop_up = _pop_up_lrg;
         }*/