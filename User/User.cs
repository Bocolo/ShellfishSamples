
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
    //[FirestoreProperty]
    //public int submittedSampleCount { get; set; }
    //[FirestoreProperty]
    //public string storedSampleCount { get; set; }
}
//public class User : MonoBehaviour
//{
//    private string name;
//    private string company;
//    private string date;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
