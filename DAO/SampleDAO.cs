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
    /// <summary>
    /// Manages access to to Sample data in Firestore Database
    /// </summary>
    public class SampleDAO
    {
        private readonly FirebaseFirestore _firestore;
        private readonly string _collectionPath = "AllSampleDB";
        /// <summary>
        /// Constructor: sets the firestore instance
        /// </summary>
        public SampleDAO()
        {
            _firestore = FirebaseFirestore.DefaultInstance;
        }
        /// <summary>
        /// Adds a sample to the firestore Sample collection
        /// </summary>
        /// <param name="sample">the sample to add</param>
        public void AddSample(Sample sample)
        {
            _firestore.Collection(_collectionPath).Document().SetAsync(sample);
        }
        /// <summary>
        /// adds a sample to the firestore userSamples collection 
        /// which is located in the Users collection, in the document named after
        /// the firebase users email
        /// 
        /// </summary>
        /// <param name="currentUser">The current firebase user</param>
        /// <param name="sample">the sample to upoload</param>
        public void AddSampleToUserCollection(FirebaseUser currentUser, Sample sample)
        {
            //should this be in user dao??
            _firestore.Collection("Users").Document(currentUser.Email).Collection("UserSamples").Document().SetAsync(sample);
        }
        /// <summary>
        /// Retrieves a sample from firestore document located at the passed _path
        /// returns the retrieved sample
        /// </summary>
        /// <param name="_path">the path to retrieve the sample from</param>
        /// <returns>The returned sample</returns>
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
        /// <summary>
        /// Retrieves all samples submitted by a firebase user
        /// </summary>
        /// <param name="currentuser">the firebase user</param>
        /// <returns>a List of samples retrieved from the firebase user's collection</returns>
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
        /// <summary>
        /// Sets and returns a Query based on the passed params
        /// </summary>
        /// <param name="searchField">The name of the field to search</param>
        /// <param name="searchName">the name of search</param>
        /// <param name="searchLimit">the limit of results</param>
        /// <returns>the firestore query</returns>
        public Query SetTestQuery(string searchField, string searchName, int searchLimit)
        {
            Query testQuery = SetQuerySearchParamaters(searchField, searchName);
            testQuery = SetTestQueryLimit(testQuery, searchLimit);
            return testQuery;
        }
        /// <summary>
        ///  Sets and returns a Query based on the passed params
        /// </summary>
        /// <param name="searchField">The field to search</param>
        /// <param name="searchName">The name / value to search</param>
        /// <returns>the firestore query</returns>
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
        /// <summary>
        ///  Sets and returns a Query based on the passed params
        /// </summary>
        /// <param name="testQuery">The query to be modified</param>
        /// <param name="searchLimit">the query limit to set</param>
        /// <returns>the modified query</returns>
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
        /// <summary>
        /// Retrieves a list of samples from firestore based on the passed query
        /// </summary>
        /// <param name="query">the firestore query</param>
        /// <returns>a list of samples </returns>
        public async Task<List<Sample>> GetSamplesBySearch(Query query)
        {
            List<Sample> collectionSamples = new List<Sample>();
            await query.GetSnapshotAsync().ContinueWithOnMainThread(task =>
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
