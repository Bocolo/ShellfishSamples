using System;
using TMPro;
using UnityEngine;
namespace UI.Retrieve
{
    /// <summary>
    /// Manages the DatabaseSearch page
    /// </summary>
    public class SearchSampleUI : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _searchDropdown;
        [SerializeField] private TMP_InputField _searchInput;
        [SerializeField] private TMP_InputField _searchLimit;
        public string SearchFieldSelection { get; private set; } = "";
        public string SearchNameSelection { get; private set; } = "";
        public int SearchLimitSelection { get; private set; } = 0;
        /// <summary>
        /// Sets the search values: name, field and limit
        /// </summary>
        public void SetSearchValues()
        {
            SetSearchFieldValue(_searchDropdown.value);
            SearchNameSelection = _searchInput.text;
//            SetSearchNameSelection();
            SetSearchLimitSelection();
        }
        /// <summary>
        /// sets the SearchLimitSelection to search limit input
        /// </summary>
        private void SetSearchLimitSelection()
        {
            if (!_searchLimit.text.Equals(""))
            {
                try {
                    SearchLimitSelection = int.Parse(_searchLimit.text);
                }
                catch(FormatException e)
                {
                    Debug.LogError("SetSearchLimitSelection: Format Exception: " + e.StackTrace);
                    SearchLimitSelection = 0;
                }
            }
        }
        /// <summary>
        /// Sets the SearchFieldSelection based on the passed dropdownValues
        /// </summary>
        /// <param name="dropdownValue"></param>
        private void SetSearchFieldValue(int dropdownValue)
        {
            switch (dropdownValue)
            {
                case 0:
                    SearchFieldSelection = "";
                    break;
                case 1:
                    SearchFieldSelection = "Name";
                    break;
                case 2:
                    SearchFieldSelection = "Company";
                    break;
                case 3:
                    SearchFieldSelection = "Species";
                    break;
                case 4:
                    SearchFieldSelection = "ProductionWeekNo";
                    break;
                case 5:
                    SearchFieldSelection = "Date";
                    break;
            }
        }
#if UNITY_INCLUDE_TESTS
        public void SetUpTestVariables()
        {
            GameObject go1 = new GameObject();
            GameObject go3 = new GameObject();
            GameObject go4 = new GameObject();
            _searchDropdown = go1.AddComponent<TMP_Dropdown>();
            _searchInput = go3.AddComponent<TMP_InputField>();
            _searchLimit = go4.AddComponent<TMP_InputField>();
        }
        public string GetSearchInputText()
        {
            return _searchInput.text;
        }
        public void SetSearchInputText(string text)
        {
            _searchInput.text = text;
        }
        public string GetSearchLimitText()
        {
            return _searchLimit.text;
        }
        public void SetSearchLimitText(string text)
        {
            _searchLimit.text = text;
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