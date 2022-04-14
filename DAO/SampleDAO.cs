using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
namespace Data.Access
{
    public class SampleDAO
    {
        private readonly FirebaseFirestore _firestore;
        private readonly string _collectionPath = "AllSampleDB";
        public SampleDAO()
        {
            _firestore = FirebaseFirestore.DefaultInstance;
        }



        public void AddSample(Sample sample)
        {
            _firestore.Collection(_collectionPath).Document().SetAsync(sample);
        }
        public void AddSampleToUserCollection(FirebaseUser currentUser, Sample sample)
        {
            //should this be in user dao??
            _firestore.Collection("Users").Document(currentUser.Email).Collection("UserSamples").Document().SetAsync(sample);
        }
        public async Task<Sample> GetSample(string _path)
        {
            Sample sample = new Sample();
            await _firestore.Document(_path).GetSnapshotAsync().ContinueWithOnMainThread(task =>
           {
               Assert.IsNull(task.Exception);
               sample = task.Result.ConvertTo<Sample>();
           });
            return sample;
        }
        public async Task<List<Sample>> GetAllUserSubmittedSamples(FirebaseUser currentuser)
        {
            List<Sample> collectionSamples = new List<Sample>();
            CollectionReference userSampleCollection = _firestore.Collection("Users").Document(currentuser.Email).Collection("UserSamples");
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
                         Debug.Log("GetAllUserSubmittedSamples: failed to convert to Sample: " + e.StackTrace);
                     }
                 }
             });
            return collectionSamples;
        }
        public Query SetTestQuery(string searchField, string searchName, int searchLimit)
        {
            Query testQuery = SetQuerySearchParamaters(searchField, searchName);
            testQuery = SetTestQueryLimit(testQuery, searchLimit);
            return testQuery;
        }
        private Query SetQuerySearchParamaters(string searchField, string searchName)
        {

            Query testQuery = _firestore.Collection(_collectionPath);
            if (searchField.Equals("ProductionWeekNo"))
            {
                try
                {
                    testQuery = testQuery.WhereEqualTo(searchField, int.Parse(searchName));
                }
                catch (FormatException formatException)
                {
                    Debug.LogError("SetQuerySearchParamaters: failed to parse limit: " + formatException.StackTrace);


                }
            }
            else if ((!searchName.Equals("")) && (!searchField.Equals("")))
            {
                testQuery = testQuery.WhereEqualTo(searchField, searchName);
            }
            return testQuery;
        }
        private Query SetTestQueryLimit(Query testQuery, int searchLimit)
        {
            if (searchLimit > 0)
            {
                testQuery = testQuery.Limit(searchLimit);
            }
            else
            {
                testQuery = testQuery.Limit(100);
            }
            return testQuery;
        }
        public async Task<List<Sample>> GetSamplesBySearch(Query testQuery)
        {
            List<Sample> collectionSamples = new List<Sample>();
            await testQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                try
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
                            Debug.Log("GetSamplesBySearch: failed to convert to Sample: " + e.StackTrace);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("GetSamplesBySearch: " + e.StackTrace);
                }
            });
            return collectionSamples;
        }

    }
}
