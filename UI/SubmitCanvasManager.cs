
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Submit.UI
{
    public class SubmitCanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject SmallCanvas;
        [SerializeField] private GameObject LargeCanvas;

        public TMP_InputField _name;
        public TMP_InputField _company;
        public TMP_InputField _comments;
        public TMP_InputField _pop_up;
        public TMP_Dropdown _productionWk;
        public TMP_Dropdown _species;
        public TMP_Dropdown DayDrop;
        public TMP_Dropdown MonthDrop;
        public TMP_Dropdown YearDrop;
        public TMP_Dropdown _iceRectangle;
        public TMP_Dropdown _sampleLocationName;
   
        public Button _submitButton;


        //SMALL CANVAS
        [SerializeField] private TMP_InputField _name_sml;
        [SerializeField] private TMP_InputField _company_sml;

        [SerializeField] private TMP_InputField _pop_up_sml;
        [SerializeField] private TMP_Dropdown _productionWk_sml;
        [SerializeField] private TMP_Dropdown _species_sml;
        [SerializeField] private TMP_Dropdown DayDrop_sml;
        [SerializeField] private TMP_Dropdown MonthDrop_sml;
        [SerializeField] private TMP_Dropdown YearDrop_sml;
        [SerializeField] private TMP_Dropdown _iceRectangle_sml;
        [SerializeField] private TMP_Dropdown _sampleLocationName_sml;
        [SerializeField] private TMP_InputField _comments_sml;

        [SerializeField] private Button _submitButton_sml;


        //LARGE CANVAS
        [SerializeField] private TMP_InputField _name_lrg;
        [SerializeField] private TMP_InputField _company_lrg;

        [SerializeField] private TMP_InputField _pop_up_lrg;
        [SerializeField] private TMP_Dropdown _productionWk_lrg;
        [SerializeField] private TMP_Dropdown _species_lrg;
        [SerializeField] private TMP_Dropdown DayDrop_lrg;
        [SerializeField] private TMP_Dropdown MonthDrop_lrg;
        [SerializeField] private TMP_Dropdown YearDrop_lrg;
        [SerializeField] private TMP_Dropdown _iceRectangle_lrg;
        [SerializeField] private TMP_Dropdown _sampleLocationName_lrg;
        [SerializeField] private TMP_InputField _comments_lrg;

        [SerializeField] private Button _submitButton_lrg;
        private static bool isSmallCanvas = false;
        private void Awake()
        {
           
            SetCanvas(isSmallCanvas);
            //try catch for testing
            try { 
            SetNameAndCompanyFromProfile();
            }catch(Exception e)
            {

            }


        }
        public void SetNameAndCompanyFromProfile()
        {
            User user = SaveData.Instance.LoadUserProfile();
            _name.text = user.Name;
            _company.text = user.Company;
        }
        public void SwitchCanvas()
        {
            if(SmallCanvas.activeInHierarchy)
            {
           
                isSmallCanvas = false;
                SetCanvas(isSmallCanvas);
                SetNameAndCompanyFromProfile();
               ///
               /// I WANT TO BE ABLE TO STRANSFER INPUT ACROSS LARGE AND SMALL CANVASES- DO SO HERE
               ///
            }
            else
            {
            
                isSmallCanvas = true;
                SetCanvas(isSmallCanvas);
                SetNameAndCompanyFromProfile();

            }
        }
        private void SetCanvas(bool isSmall)
        {
            SmallCanvas.SetActive(isSmall);
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

            }
        }
    
      
        public void DisplayPopUP(String missingValues)
        {
            _pop_up.text = missingValues;
            _pop_up.gameObject.SetActive(true);
        }
        public void HidePopUp()
        {
            _pop_up.gameObject.SetActive(false);
        }
        public void OnSubmitClearFields()
        {
            _comments.text = "";
            _species.value = 0;
            _iceRectangle.value = 0;
            _sampleLocationName.value = 0;
            _productionWk.value = 0;
            DayDrop.value = 0;
            MonthDrop.value = 0;
            YearDrop.value = 0;
            SetNameAndCompanyFromProfile();

        }
    }
  
}
