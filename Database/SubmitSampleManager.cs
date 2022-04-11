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
        private SubmitSampleUI submitSampleUI;
        private SampleDAO sampleDAO;
        private UserDAO userDAO;
        private void Awake()
        {
            submitSampleUI = GetComponent<SubmitSampleUI>();
            sampleDAO = new SampleDAO();
            userDAO = new UserDAO();
        }

        /// <summary>
        /// Can be removed later
        /// </summary>
        /// <param name="AddAndSaveSample"></param>
        public void Sampler(Del AddAndSaveSample)
        {
            if (submitSampleUI.ValidateValues())
            {
                var sample = submitSampleUI.NewSample();
                sampleDAO.AddSample(sample);
                AddAndSaveSample(sample);
                submitSampleUI.CompleteSubmission();

                UpdateFirebaseUserSample(sample);
            }
        }
        public delegate void Del(Sample sample);

        //move this to the firebase class or submission classes
        public void UpdateFirebaseUserSample(Sample sample)
        {
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                sampleDAO.AddSampleToUserCollection(auth.CurrentUser, sample);
                userDAO.UpdateUserSampleCount(auth.CurrentUser);
            }
        }
        public void UploadStoredSamples(FirebaseUser user, List<Sample> storedSamples)
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
        public void SubmitStoredSamples()
        {
            FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;

            List<Sample> storedSamples = SaveData.Instance.GetUserStoredSamples();

            UploadStoredSamples(user, storedSamples);
            if (user != null)
            {
                userDAO.UpdateUserSampleCount(user, storedSamples.Count);
            }
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
    }
}