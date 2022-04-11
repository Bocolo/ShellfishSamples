using TMPro;
using UnityEngine;
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



    private void SetSearchNameSelection()
    {
        searchNameSelection =searchInput.text;
    }



    private void SetSearchLimitSelection()
    {
        if (!searchLimit.text.Equals(""))
        {
            searchLimitSelection = int.Parse(searchLimit.text);
        }
    }
    public void SetSearchValues()
    {
        SetSearchFieldValue();
        SetSearchNameSelection();
        SetSearchLimitSelection();
    }
    private void SetSearchFieldValue()
    {
        /*   Debug.Log("Search Drop value : " + searchDropdown.value);
           switch (searchDropdown.value)*/
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

    /// <summary>
    /// fix all of this too
    /// </summary>
    /// <param name="dropdownValue"></param>
    public void SetSeachFieldTest(int dropdownValue)
    {

        Debug.Log("DDV: " + dropdownValue);
        SetSearchFieldValue(dropdownValue);
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

#endif
}
