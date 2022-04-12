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
        private FirebaseFirestore firestore;
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
            Sample sample = new Sample();
            var firestore = FirebaseFirestore.DefaultInstance;
            await firestore.Document(_path).GetSnapshotAsync().ContinueWithOnMainThread(task =>
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
        public Query SetTestQuery(string searchField, string searchName, int searchLimit)
        {
        

            Query testQuery = SetQuerySearchParamaters(searchField, searchName);
            testQuery = SetTestQueryLimit(testQuery, searchLimit);
  
            return testQuery;
        }
        private Query SetQuerySearchParamaters(string searchField, string searchName)
        {
  
          Query testQuery =   firestore.Collection("Samples");
            if (searchField.Equals("ProductionWeekNo"))
            {
                try
                {
                    testQuery = testQuery.WhereEqualTo(searchField, int.Parse(searchName));
                }
                catch (FormatException formatException)
                {
                    Debug.LogError(formatException.Message + "rarar");
         
         
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
            return testQuery;
        }
        public async Task<List<Sample>> GetSamplesBySearch(Query testQuery)
        {
            List<Sample> collectionSamples = new List<Sample>();

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
      /*  public async Task<List<Sample>> GetSamplesBySearch(string searchField, string searchName, int searchLimit)
        {
            List<Sample> collectionSamples = new List<Sample>();
            Query testQuery = SetTestQuery(searchField, searchName, searchLimit);


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
        }*/
    }
}

/*  public async Task<List<Sample>> GetSamplesBySearch(string searchField, string searchName, int searchLimit)
        {
            List<Sample> collectionSamples = new List<Sample>();
         //  CollectionReference samplesReference = firestore.Collection("Samples");
            Query testQuery = firestore.Collection("Samples");
            if (searchField.Equals("ProductionWeekNo"))
            {
                testQuery = testQuery.WhereEqualTo(searchField, int.Parse(searchName));
            }
            else if ((!searchName.Equals("")) && (!searchField.Equals("")))
            {
                testQuery = testQuery.WhereEqualTo(searchField, searchName);
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
       */