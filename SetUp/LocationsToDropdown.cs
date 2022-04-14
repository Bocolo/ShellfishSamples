using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace App.Setup
{//names file


    // Scroll Video for review
    //https://www.google.com/search?q=unity+add+scroll+inpout+page&rlz=1C1CHZN_enIE969IE969&sxsrf=APq-WBvFEfjM_-fX0mbZZFIZab7gxdrl0w%3A1644015553917&ei=wa_9Yb-sN_qGhbIP5o-HwAw&oq=unity+add+scroll+inpout+page&gs_lcp=ChNtb2JpbGUtZ3dzLXdpei1zZXJwEAMyBwghEAoQoAEyBwghEAoQoAE6BwgAEEcQsAM6BAgeEAo6BQghEKABOggIIRAWEB0QHjoGCAAQFhAeOgQIIRAVSgQIQRgAUN8fWMNLYLtNaAFwAXgAgAG-BIgBhBuSAQswLjMuNy4yLjAuMZgBAKABAcgBCMABAQ&sclient=mobile-gws-wiz-serp#kpvalbx=_NbD9YbPlCofVgQa68oGADQ29
    //This script is only used once on 4 dropdown UI's to populate the options list
    // it is then disabled
    public class LocationsToDropdown : MonoBehaviour
    {
        //variable - SerializeField variables can be accessed in the Unity Editor and set from there
        [SerializeField] private string _fileLocation;
        [SerializeField]private bool _isWeek = false;
        private Dropdown _dropdown;
        private TMP_Dropdown _tmpDropdown;
        //Simple script to populate dropdown Options using a text file (when is week is false)
        void Start()
        {
            //access the dropdown UI component from the gameObject
            _tmpDropdown = GetComponent<TMP_Dropdown>();
            PopulateDropdown(_isWeek);
            /*
             * FOR ORDINARY DROP DOWN
                    //access the dropdown UI component from the gameObject
                    dropdown = GetComponent<Dropdown>();
                    if (isWeek == false)
                    {
                        SetDropdownOptions();
                    }
                    else
                    {
                        //Populate dropdown with 1-52 instead for production week 
                        dropdown.options.Clear();
                        dropdown.options.Add(new Dropdown.OptionData("- Select -"));
                        for (int i = 1; i <= 52; i++)
                        {
                            dropdown.options.Add(new Dropdown.OptionData(i.ToString()));
                        }
                    }*/
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