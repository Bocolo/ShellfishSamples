using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Firebase.Firestore;
public class PopulateDatabase : MonoBehaviour
{
    /*
     -------WHAT IS NEEDED-----

    1. Read dataset
    2. For each Json sample data creat struct
    3. population collection with each struct 
     
     
     
     */
    [SerializeField] private string FileLocation;
    List<Data> temp;
    int count = 0;
    private void Start()
    {
        temp = SaveToList();
        Debug.Log(temp[0].Species);
        //method to write to the databasae all the data
        //add a debug counter next time, slow to upload them all
     /*   for (int i = 0; i < temp.Count; i++)
        {
            // Debug.Log(count + " COUNT!");
            var sampleData = new SampleData
            {
                Species = temp[i].Species,
                IcesRectangleNo = temp[i].IcesRectangleNo,
                //_iceRectangle.options[_iceRectangle.value].text,
                Company = temp[i].Company,
                Date = temp[i].Date,
                Name = temp[i].Name,
                ProductionWeekNo = temp[i].ProductionWeekNo,
                SampleLocationName = temp[i].SampleLocationName
            };
            var firestore = FirebaseFirestore.DefaultInstance;
            //   firestore.Document(_samplePath).SetAsync(sampleData); //,SetOptions.MergeAll);
            firestore.Collection("SamplesFull").Document().SetAsync(sampleData);//you work for random ID generartion
        }
            //firestore.Document(_samplePath).SetAsync(sampleData); overrides document
        */
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

        public string Species;//{ get; set; }
        public int ProductionWeekNo;//{ get; set; }
        public string IcesRectangleNo;//{ get; set; }
        public string Company;//{ get; set; }
        public string Date;// { get; set; }
        public string Name;//{ get; set; }
        public string SampleLocationName;// { get; set; }
        
        public static Data FromFile(string line)
        {
            string[] values = line.Split(';');
            Data data = new Data();
            data.Species = values[1];
            data.SampleLocationName =values[2];
            data.IcesRectangleNo = values[3];
            data.ProductionWeekNo = int.Parse(values[4]);//Convert.ToInt;
            data.Company = values[5];
            data.Name = values[6];
            data.Date = values[7];
            return data;
        }
      
    }
}
