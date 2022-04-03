using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
[FirestoreData]//StackOVerflow solution -works:)

public struct LimitedSampleData 
{
    [FirestoreProperty]
    public string Species { get; set; }
    [FirestoreProperty]
    public int ProductionWeekNo { get; set; }
    [FirestoreProperty]
    public string IcesRectangleNo { get; set; }
  
    [FirestoreProperty]
    public string Date { get; set; }
   
    [FirestoreProperty]
    public string SampleLocationName { get; set; }
}
