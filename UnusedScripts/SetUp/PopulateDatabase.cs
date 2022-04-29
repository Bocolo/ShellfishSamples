using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
namespace App.Setup
{
    public class PopulateDatabase
    {
        [SerializeField] private string _fileLocation;//"C:\\Users\\brona\\OneDrive\\Documents\\Bronagh_programming\\College_work\\IndustrialProject\\SQL\\FD_SQL_Test_c.csv"
        public List<Data> SaveToList()
        {
            List<Data> values = File.ReadAllLines(_fileLocation)
                .Skip(1)
                .Select(v => Data.ExtractFromFile(v))
                .ToList();
            return values;
        }
        public class Data
        {
            public string Species;
            public int ProductionWeekNo;
            public string IcesRectangleNo;
            public string Company;
            public string Date;
            public string Name;
            public string SampleLocationName;
            public static Data ExtractFromFile(string line)
            {
                string[] values = line.Split(';');
                Data data = new Data();
                data.Species = values[1];
                data.SampleLocationName = values[2];
                data.IcesRectangleNo = values[3];
                data.ProductionWeekNo = int.Parse(values[4]);
                data.Company = values[5];
                data.Name = values[6];
                data.Date = values[7];
                return data;
            }
        }
    }
}