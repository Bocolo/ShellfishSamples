using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
public class SampleDAO
{
    private FirebaseFirestore firestore;
    private string _samplePath = "sample_test/one_sample";
    private string _collectionPath = "Samples";
    public SampleDAO()
    {
        firestore = FirebaseFirestore.DefaultInstance;
    }
    public void AddSample(Sample sample)
    {
      //  firestore.Document(_samplePath).SetAsync(sample); //,SetOptions.MergeAll);
        firestore.Collection(_collectionPath).Document().SetAsync(sample);
    }
    public void AddSampleToUserCollection(FirebaseUser currentUser, Sample sample)
    {
        //should this be in user dao??
        firestore.Collection("Users").Document(currentUser.Email).Collection("UserSamples").Document().SetAsync(sample);
    }
    public async Task<Sample> GetSample(string _path)
    {
        Sample sample= new Sample();
        var firestore = FirebaseFirestore.DefaultInstance;
        await  firestore.Document(_path).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);
             sample = task.Result.ConvertTo<Sample>();
        });
        return sample;
    }
    public async Task<List<Sample>> GetAllUserSubmittedSamples(FirebaseUser currentuser)
    {
        List<Sample> collectionSamples = new List<Sample>();
        CollectionReference userSampleCollection = firestore.Collection("Users").Document(currentuser.Email).Collection("UserSamples");
       await userSampleCollection.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);
            QuerySnapshot collectionSnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
            {
                try
                {
                    Sample sample = documentSnapshot.ConvertTo<Sample>();
                    collectionSamples.Add(sample);
                }
                catch (Exception e)
                {
                    Debug.Log(e + " CAUGHT");
                }
            }
        });
        return collectionSamples;
    }
    public async Task< List<Sample>> GetSamplesBySearch(string searchField, string searchName, int searchLimit)
    {
        List<Sample> collectionSamples = new List<Sample>();
        CollectionReference samplesReference = firestore.Collection("Samples");
        Query testQuery= samplesReference;
        if (searchField.Equals("ProductionWeekNo"))
        {
            testQuery = samplesReference.WhereEqualTo(searchField, int.Parse(searchName));
        }
        else if ((!searchName.Equals("")) && (!searchField.Equals("")))
        {
            testQuery = samplesReference.WhereEqualTo(searchField, searchName);
        }
        if (searchLimit > 0)
        {
            testQuery = testQuery.Limit(searchLimit);
        }
        await testQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);
            QuerySnapshot collectionSnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
            {
                try
                {
                    Sample sample = documentSnapshot.ConvertTo<Sample>();
                    collectionSamples.Add(sample);
                }
                catch (Exception e)
                {
                    Debug.Log(e.StackTrace);
                }
            }
        });
        return collectionSamples;
    }
    public void UpdateSample()
    {
    }
    public void DeleteSample()
    {
    }
    public void GetAllSamples()
    {
    }
    public void AddStoredSamples(List<Sample> storedSamples)
    {
    }
}
