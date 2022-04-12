using System;
using TMPro;
using UnityEngine;
namespace UI.Retrieve
{
    public class SearchSampleUI : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown searchDropdown;
        [SerializeField] private TMP_InputField searchInput;
        [SerializeField] private TMP_InputField searchLimit;
        private string searchFieldSelection = "";
        private string searchNameSelection = "";
        private int searchLimitSelection = 0;
        public string GetSearchFieldSelection()
        {
            return searchFieldSelection;
        }
        public string GetSearchNameSelection()
        {
            return searchNameSelection;
        }
        public int GetSearchLimitSelection()
        {
            return searchLimitSelection;
        }
        public void SetSearchValues()
        {
            SetSearchFieldValue(searchDropdown.value);
            SetSearchNameSelection();
            SetSearchLimitSelection();
        }
        private void SetSearchNameSelection()
        {
            searchNameSelection = searchInput.text;
        }
        private void SetSearchLimitSelection()
        {
            if (!searchLimit.text.Equals(""))
            {
                //try  catch not necessary as the ui prevent letter input
                try {
                    searchLimitSelection = int.Parse(searchLimit.text);
                }
                catch(FormatException e)
                {
                    searchLimitSelection = 0;
                }
               
            }
        }
   
        private void SetSearchFieldValue(int dropdownValue)
        {
            /*   Debug.Log("Search Drop value : " + searchDropdown.value);
               switch (searchDropdown.value)*/
            Debug.Log("Search Drop value : " + dropdownValue);
            switch (dropdownValue)
            {
                case 0:
                    searchFieldSelection = "";
                    break;
                case 1:
                    searchFieldSelection = "Name";
                    break;
                case 2:
                    searchFieldSelection = "Company";
                    break;
                case 3:
                    searchFieldSelection = "Species";
                    break;
                case 4:
                    searchFieldSelection = "ProductionWeekNo";
                    break;
                case 5:
                    searchFieldSelection = "Date";
                    break;
            }
        }
    
#if UNITY_INCLUDE_TESTS
        public void SetUpTestVariables()
        {
            GameObject go1 = new GameObject();
            GameObject go3 = new GameObject();
            GameObject go4 = new GameObject();
            searchDropdown = go1.AddComponent<TMP_Dropdown>();
            searchInput = go3.AddComponent<TMP_InputField>();
            searchLimit = go4.AddComponent<TMP_InputField>();
        }
  
        public string GetSearchInputText()
        {
            return searchInput.text;
        }
        public void SetSearchInputText(string text)
        {
            searchInput.text = text;
        }
       
        public string GetSearchLimitText()
        {
            return searchLimit.text;
        }
        public void SetSearchLimitText(string text)
        {
            searchLimit.text = text;
        }
        /// <summary>
        /// fix all of this too
        /// </summary>
        /// <param name="dropdownValue"></param>
        public void SetSeachFieldTest(int dropdownValue)
        {
            Debug.Log("DDV: " + dropdownValue);
            SetSearchFieldValue(dropdownValue);
        }

#endif
    }
}
/*    private void SetSearchFieldValue()
    {
  //should this method change to accept input 
        Debug.Log("Search Drop value : " + searchDropdown.value);
        switch (searchDropdown.value)
        {
            case 0:
                searchFieldSelection = "";
                break;
            case 1:
                searchFieldSelection = "Name";
                break;
            case 2:
                searchFieldSelection = "Company";
                break;
            case 3:
                searchFieldSelection = "Species";
                break;
            case 4:
                searchFieldSelection = "ProductionWeekNo";
                break;
            case 5:
                searchFieldSelection = "Date";
                break;
        }
    }*/