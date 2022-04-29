using Firebase.Firestore;
namespace Users.Data
{
    /// <summary>
    /// The User struct which lists the User properties
    /// these are set as firestore properties in order to upload 
    /// the firestore data to the firestore
    /// </summary>
    [FirestoreData]
    [System.Serializable]
    public struct User
    {
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public string Company { get; set; }
        [FirestoreProperty]
        public string Email { get; set; }
        [FirestoreProperty]
        public int SubmittedSamplesCount { get; set; }
    }
}