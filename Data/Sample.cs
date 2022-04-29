using Firebase.Firestore;
namespace Samples.Data
{
    /// <summary>
    /// The Sample struct which lists the sample properties
    /// these are set as firestore properties in order to upload 
    /// the firestore data to the firestore
    /// </summary>
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
}