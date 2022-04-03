using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Submit.UI { 
    public class SubmitCanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject SmallCanvas;
        [SerializeField] private GameObject LargeCanvas;

        public TMP_InputField _name;
        public TMP_InputField _company;
        public TMP_InputField _comments;
        public TMP_InputField _pop_up;


        //[SerializeField] public TMP_InputField _sampleDate;
        public TMP_Dropdown _productionWk;
        public TMP_Dropdown _species;
           //date fields
        public TMP_Dropdown DayDrop;
        public TMP_Dropdown MonthDrop;
        public TMP_Dropdown YearDrop;
        //either but not both
        public TMP_Dropdown _iceRectangle;
        public TMP_Dropdown _sampleLocationName;
        //Not Required
   
        public Button _submitButton;


        //SMALL CANVAS
        [SerializeField] private TMP_InputField _name_sml;
        [SerializeField] private TMP_InputField _company_sml;

        [SerializeField] private TMP_InputField _pop_up_sml;
        //[SerializeField] private TMP_InputField _sampleDate;
        [SerializeField] private TMP_Dropdown _productionWk_sml;
        [SerializeField] private TMP_Dropdown _species_sml;
        //date fields
        [SerializeField] private TMP_Dropdown DayDrop_sml;
        [SerializeField] private TMP_Dropdown MonthDrop_sml;
        [SerializeField] private TMP_Dropdown YearDrop_sml;
        //either but not both
        [SerializeField] private TMP_Dropdown _iceRectangle_sml;
        [SerializeField] private TMP_Dropdown _sampleLocationName_sml;
        //Not Required
        [SerializeField] private TMP_InputField _comments_sml;

        [SerializeField] private Button _submitButton_sml;


        //LARGE CANVAS
        [SerializeField] private TMP_InputField _name_lrg;
        [SerializeField] private TMP_InputField _company_lrg;

        [SerializeField] private TMP_InputField _pop_up_lrg;
        //[SerializeField] private TMP_InputField _sampleDate;
        [SerializeField] private TMP_Dropdown _productionWk_lrg;
        [SerializeField] private TMP_Dropdown _species_lrg;
        //date fields
        [SerializeField] private TMP_Dropdown DayDrop_lrg;
        [SerializeField] private TMP_Dropdown MonthDrop_lrg;
        [SerializeField] private TMP_Dropdown YearDrop_lrg;
        //either but not both
        [SerializeField] private TMP_Dropdown _iceRectangle_lrg;
        [SerializeField] private TMP_Dropdown _sampleLocationName_lrg;
        //Not Required
        [SerializeField] private TMP_InputField _comments_lrg;

        [SerializeField] private Button _submitButton_lrg;
      //  private  CanvasValues canvasValues;
        private static bool isSmallCanvas = false;
        private void Awake()
        {
            Debug.Log("Canvas Data Awake");
            //canvasValues = new CanvasValues
            //{
            //    Name_text = "",
            //    Company_text = "",
            //    ProductionWk_value = 0,//int.Parse(_productionWk.options[_productionWk.value].text),//_productionWk.value,
            //    Species_value = 0,//int.Parse(_species.options[_species.value].text),
            //    Day_value = 0,//int.Parse(DayDrop.options[DayDrop.value].text),
            //    Month_value = 0,//int.Parse(MonthDrop.options[MonthDrop.value].text),
            //    Year_value = 0,//int.Parse(YearDrop.options[YearDrop.value].text),
            //    Rectangle_value = 0,//int.Parse(_iceRectangle.options[_iceRectangle.value].text),
            //    LocationName_value = 0,//int.Parse(_sampleLocationName.options[_sampleLocationName.value].text),
            //    Comment_text = "",//_comments.text,};
            //};
            SetCanvas(isSmallCanvas);
            SetNameAndCompanyFromProfile();
        
        }
        public void SetNameAndCompanyFromProfile()
        {
            UserData userData = SaveData.Instance.LoadUserProfile();
            _name.text = userData.Name;
            _company.text = userData.Company;
        }
        public void SwitchCanvas()
        {
            if(SmallCanvas.activeInHierarchy)
          //  if (SmallCanvas.isActiveAndEnabled)
            {
               // _pop_up.gameObject.SetActive(false);

                //SmallCanvas.SetActive(false);
                //LargeCanvas.SetActive(true);
                isSmallCanvas = false;
                SetCanvas(isSmallCanvas);
                SetNameAndCompanyFromProfile();
               ///
               /// I WANT TO BE ABLE TO STRANSFER INPUT ACROSS LARGE AND SMALL CANVASES- DO SO HERE
               ///
            }
            else
            {
               // _pop_up.gameObject.SetActive(false);
                //SmallCanvas.SetActive(true);
                //LargeCanvas.SetActive(false);
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
                //if (_name != null) {
                //    canvasValues = StoreCanvasValues();

                //}
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
                //if (_name != null)
                //{

                //    SetCanvasValues(canvasValues);
                //}
            }
            else
            {
                //if (_name != null)
                //{
                //    canvasValues = StoreCanvasValues();

                //}
               // canvasValues = StoreCanvasValues();
                //_name_lrg.text = _name.text;
                //_company_lrg.text = _company.text;
                //_productionWk_lrg.value = _productionWk.value;
                //_species_lrg.value = _species.value;
                //DayDrop_lrg.value = DayDrop.value;
                //MonthDrop_lrg.value = MonthDrop.value;
                //YearDrop_lrg.value = YearDrop.value;
                //_iceRectangle_lrg.value = _iceRectangle.value;
                //_sampleLocationName_lrg.value = _sampleLocationName.value;
                //_comments_lrg.text = _comments.text;

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

                //if (_name != null)
                //{

                //    SetCanvasValues(canvasValues);
                //}
               // SetCanvasValues(canvasValues);

            }
        }
    
        //private CanvasValues StoreCanvasValues()
        //{
        //    CanvasValues cv = new()
        //    {
        //        Name_text = _name.text,
        //        Company_text = _company.text,
        //        ProductionWk_value =/* int.Parse(_productionWk.options[_productionWk.value].text),*/_productionWk.value,
        //        Species_value =_species.value,// int.Parse(_species.options[_species.value].text),
        //        Day_value =DayDrop.value,// int.Parse(DayDrop.options[DayDrop.value].text),
        //        Month_value = MonthDrop.value,//int.Parse(MonthDrop.options[MonthDrop.value].text),
        //        Year_value =YearDrop.value,// int.Parse(YearDrop.options[YearDrop.value].text),
        //        Rectangle_value =  _iceRectangle.value,
        //        LocationName_value = _sampleLocationName.value,
        //        Comment_text = _comments.text,
        //       // ProductionWeekNo = int.Parse(canvasManager._productionWk.options[canvasManager._productionWk.value].text),

        //    };
        //    return cv;
        //}
        //private void SetCanvasValues(CanvasValues canvasValues)
        //{
        //    _name.text = canvasValues.Name_text;
        //    _company.text = canvasValues.Company_text;
        //    _productionWk.value = canvasValues.ProductionWk_value;
        //    _species.value = canvasValues.Species_value;
        //    DayDrop.value = canvasValues.Day_value;
        //    MonthDrop.value = canvasValues.Month_value;
        //    YearDrop.value = canvasValues.Year_value;
        //    _iceRectangle.value = canvasValues.Rectangle_value;
        //    _sampleLocationName.value = canvasValues.LocationName_value;
        //    _comments.text = canvasValues.Comment_text;
        //}
        public void DisplayPopUP(String missingValues)
        {
            //Instantiate();
            _pop_up.text = missingValues;
            _pop_up.gameObject.SetActive(true);
        }
        public void HidePopUp()
        {
            _pop_up.gameObject.SetActive(false);
        }
        public void OnSubmitClearFields()
        {
            //_name.text = "";
            //_company.text = "";
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
    // struct CanvasValues
    //{
    //    public string Name_text { get; set; }
    //    public string Company_text { get; set; }
    //    public int ProductionWk_value { get; set; }
    //    public int Species_value { get; set; }
    //    public int Day_value { get; set; }
    //    public int Month_value { get; set; }
    //    public int Year_value { get; set; }

    //    public int Rectangle_value { get; set; }
    //    public int LocationName_value { get; set; }
    //    public string Comment_text { get; set; }
    //}
}

//if (Instance != null && Instance != this)
//{

//Destroy(this);
//    Debug.Log("Canvas Data Awake ifff");

//}
//else
//{
//    Instance = this;
//    DontDestroyOnLoad(this.gameObject);
//        Debug.Log("Canvas Data Awakee else");
//    Transform[] children = gameObject.GetComponentsInChildren<Transform>();
//    foreach(Transform t in children)
//    {
//        DontDestroyOnLoad(t.gameObject);
//    }

//}
/*
 
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
 
 
 
   _name = _name_sml;
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
 
 
 
 
 
 
 
 
 
 */