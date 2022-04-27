using System;
using TMPro;
using UnityEngine;
using Search.Logic;
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
        public int SearchLimitSelection { get; private set; } = 100;
        private SearchLogic searchLogic;
        void Awake()
        {
            searchLogic = new SearchLogic();
        }
        /// <summary>
        /// Sets the search values: name, field and limit
        /// </summary>
        public void SetSearchValues()
        {
            SearchFieldSelection= searchLogic.GetSearchField(_searchDropdown.value);
            SearchNameSelection = _searchInput.text;
            SearchLimitSelection = searchLogic.GetSearchLimit(_searchLimit.text);
        }
  
    }
}