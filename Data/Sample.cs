using Firebase.Firestore;

[FirestoreData]
[System.Serializable]
public struct Sample 
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
