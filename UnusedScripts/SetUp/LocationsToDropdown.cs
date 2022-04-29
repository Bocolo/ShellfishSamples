using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace App.Setup
{
    /// <summary>
    /// This script was only used once on 4 dropdown UI's to populate the options list
    // it was then disabled
    /// </summary>
    public class LocationsToDropdown : MonoBehaviour
    {
        //variable - SerializeField variables can be accessed in the Unity Editor and set from there
        [SerializeField] private string _fileLocation;
        [SerializeField] private bool _isWeek = false;
        private Dropdown _dropdown;
        private TMP_Dropdown _tmpDropdown;
        //Simple script to populate dropdown Options using a text file (when is week is false)
        void Start()
        {
            //access the dropdown UI component from the gameObject
            _tmpDropdown = GetComponent<TMP_Dropdown>();
            PopulateDropdown(_isWeek);
        }
        private void PopulateDropdown(bool isWeek)
        {
            if (isWeek == false)
            {
                SetTMPDropdownOptions();
            }
            else
            {
                //Populate dropdown with 1-52 instead for production week 
                _tmpDropdown.options.Clear();
                _tmpDropdown.options.Add(new TMP_Dropdown.OptionData("- Select -"));
                for (int i = 1; i <= 52; i++)
                {
                    _tmpDropdown.options.Add(new TMP_Dropdown.OptionData(i.ToString()));
                }
            }
        }
        //populate dropdown options with each line of a text file
        //text file locations are added in the unity Editor
        private void SetDropdownOptions()
        {
            _tmpDropdown.options.Clear();
            string[] lines = System.IO.File.ReadAllLines(_fileLocation);
            foreach (string line in lines)
            {
                _dropdown.options.Add(new Dropdown.OptionData(line));
            }
        }
        /// <summary>
        /// populate tmp dropdowns with values from file
        /// </summary>
        private void SetTMPDropdownOptions()
        {
            _tmpDropdown.options.Clear();
            string[] lines = System.IO.File.ReadAllLines(_fileLocation);
            foreach (string line in lines)
            {
                _tmpDropdown.options.Add(new TMP_Dropdown.OptionData(line));
            }
        }
    }
}