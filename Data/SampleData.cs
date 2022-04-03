using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
//using System

[FirestoreData]//StackOVerflow solution -works:)
[System.Serializable]//needed?
public struct SampleData 
{

   [FirestoreProperty]
    public string Species { get; set; }
    [FirestoreProperty]
    public int ProductionWeekNo { get; set; }
    [FirestoreProperty]
    public string IcesRectangleNo { get; set; }
    [FirestoreProperty]
    public string Company { get; set; }
    [FirestoreProperty]
    public string Date { get; set; }
    [FirestoreProperty]
    public string Name { get; set; }
    [FirestoreProperty]
    public string SampleLocationName { get; set; }
    [FirestoreProperty]
    public string Comment { get; set; }
   
}
