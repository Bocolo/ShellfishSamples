using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Search.Logic { 
    public class SearchLogic 
    {
        /// <summary>
        /// Sets the SearchFieldSelection based on the passed dropdownValues
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
        /// sets the SearchLimitSelection to search limit input
        /// </summary>
        public int GetSearchLimit(string limit)
        {
            try
            {
                return int.Parse(limit);
            }
            catch (FormatException e)
            {
                Debug.Log("SetSearchLimitSelection: Format Exception: " + e.StackTrace);
                return 100;
            }
        }

    }
}