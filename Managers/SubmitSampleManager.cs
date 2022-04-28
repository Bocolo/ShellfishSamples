using App.Samples.UI;
using App.Samples.Validation;
using App.SaveSystem.Manager;
using Firebase.Auth;
using Samples.Data;
using Samples.Data.Access;
using System;
using System.Collections.Generic;
using UnityEngine;
using Users.Data.Access;

namespace App.Samples.Manager
{
    /// <summary>
    /// Manages action related to submition, saving and uploading samples
    /// with buttons in unity scenes
    /// </summary>
    public class SubmitSampleManager : MonoBehaviour
    {
        private SampleValidator _sampleValidator;
        private SubmitCanvasManager _submitCanvasManager;
        private SampleDAO _sampleDAO;
        private UserDAO _userDAO;
        // [SerializeField] private PopUp popUp;
        /// <summary>
        /// Sets all local value fields on awake
        /// </summary>
        private void Awake()
        {
            _sampleValidator = GetComponent<SampleValidator>();
            _submitCanvasManager = GetComponent<SubmitCanvasManager>();
            _sampleDAO = new SampleDAO();
            _userDAO = new UserDAO();
        }
        /// <summary>
        /// Submits stored samples to firestre and updates the save file
        /// </summary>
        public void SubmitAndSaveStoredSamples()
        {
            try
            {
                SubmitStoredSamples();
                SaveData.Instance.UpdateSubmittedStoredSamples();
            }
            catch (Exception e)
            {
                Debug.LogError("SubmitAndSaveStoredSamples: " + e.StackTrace);
            }
        }
        /// <summary>
        /// Stores a samples in a save file if the values are validated
        /// </summary>
        public void StoreSample()
        {
            if (_sampleValidator.ValidateValues())
            {
                SaveData.Instance.AddAndSaveStoredSample(_sampleValidator.NewSample());
                _submitCanvasManager.CompleteStore();
            }
        }
        /// <summary>
        /// if sample values are validated:
        /// 1. uploades sample to firestore smaple collection
        ///  2. updates the submitted samples save file
        ///  3. notifies canvas maneger of successfull submission
        ///  4. calls UpdateFirebaseUserSample with the newly created sample
        /// </summary>
        public void UploadSample()
        {
            if (_sampleValidator.ValidateValues())
            {
                var sample = _sampleValidator.NewSample();
                _sampleDAO.AddSample(sample);
                SaveData.Instance.AddAndSaveSubmittedSample(sample);
                _submitCanvasManager.CompleteSubmission();
                UpdateFirebaseUserSample(sample);
            }
        }
        /// <summary>
        /// Loads stored sample from the device and upload them to the firestore sample
        /// collection.
        ///  updates the save files.
        ///  if firestore user is logged in:
        ///1. uploads to the firestore user-sample collection
        ///2, updates the firestore user sample count
        /// </summary>
        private void SubmitStoredSamples()
        {
            FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
            Debug.Log("User is null / not null " + user);
            List<Sample> storedSamples = SaveData.Instance.UsersStoredSamples;
            UploadStoredSamples(user, storedSamples);
            if (user != null)
            {
                Debug.Log("User is not null " + user);

                _userDAO.UpdateUserSampleCount(user, storedSamples.Count);
            }
            SaveData.Instance.UpdateSubmittedStoredSamples();
        }
        //move this to the firebase class or submission classes
        /// <summary>
        /// if there is a logged in firebase user, upload the passed sample to 
        /// the user -sample collection and update the user samples count
        /// </summary>
        /// <param name="sample">sample to upload</param>
        private void UpdateFirebaseUserSample(Sample sample)
        {
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                _sampleDAO.AddSampleToUserCollection(auth.CurrentUser, sample);
                _userDAO.UpdateUserSampleCount(auth.CurrentUser);
            }
        }
        /// <summary>
        /// upload List of samples to the firestore sample collection.
        ///  
        ///  if firestore user is logged in:
        ///1. uploads to the firestore user-sample collection
        /// </summary>
        /// <param name="user"></param>
        /// <param name="storedSamples"></param>
        private void UploadStoredSamples(FirebaseUser user, List<Sample> storedSamples)
        {
            for (int i = 0; i < storedSamples.Count; i++)
            {
                _sampleDAO.AddSample(storedSamples[i]);
                SaveData.Instance.AddToSubmittedSamples(storedSamples[i]);
                Debug.Log("adding to submitt");
                if (user != null)
                {
                    _sampleDAO.AddSampleToUserCollection(user, storedSamples[i]);
                }
            }
        }
    }
}