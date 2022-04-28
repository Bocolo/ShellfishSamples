using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Samples.Logic { 
    public class SampleDetailsLogic 
    {
        public string SampleWithIcesToString(Sample sample)
        {
            return ("Name: " + sample.Name + "\nCompany: " + sample.Company + "\nSpecies: " + sample.Species
                 + $"\nICEs Rectangle: {sample.IcesRectangleNo}"
                 + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date + "\nComment: " + sample.Comment);
        }
        public string SampleWithLocationToString(Sample sample)
        {
            return ("Name: " + sample.Name + "\nCompany: " + sample.Company + "\nSpecies: " + sample.Species
                   + "\nLocation: " + sample.SampleLocationName + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date
                   + "\nComment: " + sample.Comment);
        }
        public string RestrictedSampleWithIcesToString(Sample sample)
        {
            return ("\nSpecies: " + sample.Species
      + $"\nICEs Rectangle: {sample.IcesRectangleNo}"
      + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date);
        }
        public string RestrictedSampleWithLocationToString(Sample sample)
        {
            return ("\nSpecies: " + sample.Species
              + "\nLocation: " + sample.SampleLocationName + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date);
        }
        /// <summary>
        /// sets and returns the string of the sample details
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public string SampleToString(Sample sample)
        {
            if (sample.SampleLocationName == null)
            {
                return SampleWithIcesToString(sample);
            }
            else
            {
                return SampleWithLocationToString(sample);
            }
        }
        /// <summary>
        /// sets and  returns the string of the sample details, restricting some information
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public string RestrictedSampleToString(Sample sample)
        {
            if (sample.SampleLocationName == null)
            {
                return RestrictedSampleWithIcesToString(sample);
            }
            else
            {
                return RestrictedSampleWithLocationToString(sample);
            }
        }
        /// <summary>
        /// Checks passed date  string is a valid data
        /// </summary>
        /// <returns>bool of date validity</returns>
        public bool IsDateValid(string date)
        {
            try
            {
                DateTime datetime = DateTime.Parse(date);
                DateTime local = DateTime.Now;
                int result = DateTime.Compare(datetime, local);
                if (result > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.Log("Date Check failed: " + e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// Sets the _date string with details of passed params
        /// </summary>
        /// <param name="day">string representing day</param>
        /// <param name="month">string representing month</param>
        /// <param name="year">string representing year</param>
        public string GetDate(string day, string month, string year)
        {
            return (year + "-" + month + "-" + day);
        }



        /// <summary>
        /// If the _date field is not a valid date, modifies the missing values string before
        /// returing the string
        /// </summary>
        /// <param name="missingValues">the string to modify</param>
        /// <returns></returns>
        public string MissingDate(string missingValues, string date)
        {
            if (!IsDateValid(date))
            {
                missingValues += "Please enter a valid date\n";
            }
            return missingValues;
        }
        /// <summary>
        /// if name field is null, modifies the  passed missing value string
        /// 
        /// returns the missing value string
        /// </summary>
        /// <param name="missingValues"></param>
        /// <returns></returns>
        public string MissingName(string missingValues, string name)
        {
            if (name == null)
            {
                missingValues += "Please enter a name\n";
            }
            return missingValues;
        }
        /// <summary>
        /// if company field is null, modifies the  passed missing value string
        /// 
        /// returns the missing value string
        /// </summary>
        /// <param name="missingValues"></param>
        /// <returns></returns>
        public string MissingCompany(string missingValues, string company)
        {
            if (company == null)
            {
                missingValues += "Please enter a company name\n";
            }
            return missingValues;
        }
        /// <summary>
        /// if specied field is null, modifies the  passed missing value string
        /// 
        /// returns the missing value string
        /// </summary>
        /// <param name="missingValues"></param>
        /// <returns></returns>
        public string MissingSpecies(string missingValues, string species)
        {
            if (species == null)
            {
                missingValues += "Please enter the shellfish species\n";
            }
            return missingValues;
        }
        /// <summary>
        /// if no ices string and location string are both null 
        /// or
        /// if both are not null
        ///modifies the  passed missing value string with error details
        /// 
        /// returns the missing value string
        /// </summary>
        /// <param name="missingValues"></param>
        /// <returns></returns>
        public string MissingOrDualLocation(string missingValues, string icesRectangle, string location)
        {
            if (((icesRectangle == null) && (location == null)) || ((icesRectangle != null) && (location != null)))
            {
                missingValues += "You must enter <i>either</i> a Sample Location Date or an Ices Rectangle No.\n";
            }
            return missingValues;
        }
        /// <summary>
        /// if _canvasManager.ProductionWk.value is not set, modifies the  passed missing value string
        /// 
        /// returns the missing value string
        /// </summary>
        /// <param name="missingValues"></param>
        /// <returns></returns>
        public string MissingProductionWeek(string missingValues, int productionWeek)
        {
            if (productionWeek == 0)
            {
                missingValues += "Please enter the production week\n";
            }
            return missingValues;
        }
    
    }
}