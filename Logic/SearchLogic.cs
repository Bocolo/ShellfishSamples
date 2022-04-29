using System;
using UnityEngine;
namespace App.Samples
{
    public class SearchLogic
    {
        /// <summary>
        /// returns the SearchField string based on the passed dropdownValues
        /// </summary>
        /// <param name="dropdownValue"></param>
        public string GetSearchField(int dropdownValue)
        {
            switch (dropdownValue)
            {
                case 0:
                    return "";
                case 1:
                    return "Name";
                case 2:
                    return "Company";
                case 3:
                    return "Species";
                case 4:
                    return "ProductionWeekNo";
                case 5:
                    return "Date";
                default:
                    return "";
            }
        }

        /// <summary>
        /// parses the passed string and return its int value
        /// defaults to 100 if string cannot be parsed
        /// </summary>
        /// <param name="limit"> string representing the search limit</param>
        /// <returns></returns>
        public int GetSearchLimit(string limit)
        {
            try
            {
                return int.Parse(limit);
            }
            catch (FormatException e)
            {
                Debug.Log("GetSearchLimit: Format Exception: " + e.StackTrace);
                return 100;
            }
        }

    }

}