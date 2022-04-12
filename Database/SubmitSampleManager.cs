using Firebase.Auth;
using System;
using System.Collections.Generic;
/*using UI.Submit;*/
using UnityEngine;
using UI.Submit;
using Data.Access;
using Save.Manager;

namespace Data.Submit
{
    public class SubmitSampleManager : MonoBehaviour
    {
        private SampleValidator sampleValidator;
        private SampleDAO sampleDAO;
        private UserDAO userDAO;
        private void Awake()
        {
            sampleValidator = GetComponent<SampleValidator>();
            sampleDAO = new SampleDAO();
            userDAO = new UserDAO();
        }
        public void SubmitAndSaveStoredSamples()
        {
            try
            {
                SubmitStoredSamples();
                SaveData.Instance.UpdateSubmittedStoredSamples();

            }
            catch (Exception e)
            {
                Debug.LogError(e + " submitting stored data error");
            }
        }
        public void StoreSample()
        {
            if (sampleValidator.ValidateValues())
            {
                SaveData.Instance.AddAndSaveStoredSample(sampleValidator.NewSample());
                sampleValidator.CompleteSubmission();
            }
            //testing --- move to if block
            /* SaveData.Instance.SaveStoredSamples();*/
        }
        public void UploadSample()
        {
            if (sampleValidator.ValidateValues())
            {
                var sample = sampleValidator.NewSample();
                sampleDAO.AddSample(sample);

                SaveData.Instance.AddAndSaveSubmittedSample(sample);
                sampleValidator.CompleteSubmission();

                UpdateFirebaseUserSample(sample);
            }

        }
        private void SubmitStoredSamples()
        {
            FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;

            List<Sample> storedSamples = SaveData.Instance.UsersStoredSamples;

            UploadStoredSamples(user, storedSamples);
            if (user != null)
            {
                userDAO.UpdateUserSampleCount(user, storedSamples.Count);
            }
        }
        //move this to the firebase class or submission classes
        private void UpdateFirebaseUserSample(Sample sample)
        {
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                sampleDAO.AddSampleToUserCollection(auth.CurrentUser, sample);
                userDAO.UpdateUserSampleCount(auth.CurrentUser);
            }
        }
        private void UploadStoredSamples(FirebaseUser user, List<Sample> storedSamples)
        {
            for (int i = 0; i < storedSamples.Count; i++)
            {
                sampleDAO.AddSample(storedSamples[i]);
                SaveData.Instance.AddToSubmittedSamples(storedSamples[i]);
                //   counter++;
                if (user != null)
                {
                    sampleDAO.AddSampleToUserCollection(user, storedSamples[i]);
                }
            }
        }
   

    }
}