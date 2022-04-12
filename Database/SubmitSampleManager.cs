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
        private SampleValidator submitSampleUI;
        private SampleDAO sampleDAO;
        private UserDAO userDAO;
        private void Awake()
        {
            submitSampleUI = GetComponent<SampleValidator>();
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
            if (submitSampleUI.ValidateValues())
            {
                SaveData.Instance.AddAndSaveStoredSample(submitSampleUI.NewSample());
                submitSampleUI.CompleteSubmission();
            }
            //testing --- move to if block
            /* SaveData.Instance.SaveStoredSamples();*/
        }
        public void UploadSample()
        {
            if (submitSampleUI.ValidateValues())
            {
                var sample = submitSampleUI.NewSample();
                sampleDAO.AddSample(sample);

                SaveData.Instance.AddAndSaveSubmittedSample(sample);
                submitSampleUI.CompleteSubmission();

                UpdateFirebaseUserSample(sample);
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
        private void SubmitStoredSamples()
        {
            FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;

            List<Sample> storedSamples = SaveData.Instance.GetUserStoredSamples();

            UploadStoredSamples(user, storedSamples);
            if (user != null)
            {
                userDAO.UpdateUserSampleCount(user, storedSamples.Count);
            }
        }

    }
}