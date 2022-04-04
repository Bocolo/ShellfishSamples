using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Firebase.Firestore;
public class PopulateDatabase : MonoBehaviour
{
   
    [SerializeField] private string FileLocation;
    List<Data> temp;
    int count = 0;
    private void Start()
    {
        temp = SaveToList();
        Debug.Log(temp[0].Species);
 
    }

    List<Data> SaveToList()
    {
        List<Data> values = File.ReadAllLines("C:\\Users\\brona\\OneDrive\\Documents\\Bronagh_programming\\College_work\\IndustrialProject\\SQL\\FD_SQL_Test_c.csv")
            .Skip(1)
            .Select(v => Data.FromFile(v))
            .ToList();
        return values;
    }
    class Data
    {

        public string Species;
        public int ProductionWeekNo;
        public string IcesRectangleNo;
        public string Company;
        public string Date;
        public string Name;
        public string SampleLocationName;
        
        public static Data FromFile(string line)
        {
            string[] values = line.Split(';');
            Data data = new Data();
            data.Species = values[1];
            data.SampleLocationName =values[2];
            data.IcesRectangleNo = values[3];
            data.ProductionWeekNo = int.Parse(values[4]);
            data.Company = values[5];
            data.Name = values[6];
            data.Date = values[7];
            return data;
        }
      
    }
}
