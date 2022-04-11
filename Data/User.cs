using Firebase.Firestore;
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