using TMPro;
using UnityEngine;
namespace App.Samples.UI
{
    /// <summary>
    /// Manages the DatabaseSearch page
    /// </summary>
    public class SearchSampleUI : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _searchDropdown;
        [SerializeField] private TMP_InputField _searchInput;
        [SerializeField] private TMP_InputField _searchLimit;
        private SearchLogic _searchLogic;

        public string SearchFieldSelection { get; private set; } = "";
        public string SearchNameSelection { get; private set; } = "";
        public int SearchLimitSelection { get; private set; } = 100;
        /// <summary>
        /// creates the search logic object on awake
        /// </summary>
        void Awake()
        {
            _searchLogic = new SearchLogic();
        }
        /// <summary>
        /// Sets the search values: name, field and limit
        /// </summary>
        public void SetSearchValues()
        {
            SearchFieldSelection = _searchLogic.GetSearchField(_searchDropdown.value);
            SearchNameSelection = _searchInput.text;
            SearchLimitSelection = _searchLogic.GetSearchLimit(_searchLimit.text);
        }

    }
}