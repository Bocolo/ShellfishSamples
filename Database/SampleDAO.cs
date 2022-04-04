using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.Assertions;
using System;
using TMPro;
using Firebase.Firestore;
using Firebase.Auth;
using System.Threading.Tasks;
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
        firestore.Collection(_collectionPath).Document().SetAsync(sample);//you work for random ID generartion
    }
    public void AddStoredSamples(List<Sample> storedSamples)
    {

    }
    public void AddSampleToUserCollection(FirebaseUser currentUser, Sample sample)
    {
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

    public void GetAllSamples()
    {

    }
    public async Task<List<Sample>> GetAllUserSubmittedSamples(FirebaseUser currentuser)
    {
        List<Sample> collectionSamples = new List<Sample>();
        CollectionReference userSampleCollection = firestore.Collection("Users").Document(currentuser.Email).Collection("UserSamples");
       await userSampleCollection.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);
            QuerySnapshot collectionSnapshot = task.Result;
            int counter = 0;
            int counter2 = 0;
            foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
            {
                try
                {
                    Sample sample = documentSnapshot.ConvertTo<Sample>();
                    counter++;
                    collectionSamples.Add(sample);
                }
                catch (Exception e)
                {
                    Debug.Log(e + " CAUGHT ttt");
                    counter2++;
                }
            }
          //  AddTextAndPrefab(collectionSamples);
            Debug.Log("Try : " + counter + ".  Caught ttt: " + counter2);
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
            testQuery = samplesReference.WhereEqualTo(searchField, searchName);//.Limit(0);

        }
/*        else
        {
            testQuery = samplesReference;

        }*/
        if (searchLimit > 0)
        {
            testQuery = testQuery.Limit(searchLimit);
        }
        await testQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);
            QuerySnapshot collectionSnapshot = task.Result;
            int counter = 0;
            int counter2 = 0;
            foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
            {
                try
                {
                    counter++;
                    Debug.Log("trying doc snapshot - " + counter);
                    Sample sample = documentSnapshot.ConvertTo<Sample>();
                 //   counter++;
                    collectionSamples.Add(sample);
                }
                catch (Exception e)
                {
                    Debug.Log(e.StackTrace);
                    Debug.Log(e + " CAUGHT");
                    counter2++;
                    Debug.Log("---------------caught doc snapshot - " + counter2);
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
}
