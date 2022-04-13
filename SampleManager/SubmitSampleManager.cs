using Firebase.Auth;
using System;
using System.Collections.Generic;
/*using UI.Submit;*/
using UnityEngine;
using UI.Submit;
using Data.Access;
using Save.Manager;
using UI.Popup;
namespace Data.Submit
{

    public class SubmitSampleManager : MonoBehaviour
    {
        private SampleValidator sampleValidator;
        private SubmitCanvasManager submitCanvasManager;
        private SampleDAO sampleDAO;
        private UserDAO userDAO;
       // [SerializeField] private PopUp popUp;
        private void Awake()
        {
            sampleValidator = GetComponent<SampleValidator>();
            submitCanvasManager = GetComponent<SubmitCanvasManager>();
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
                submitCanvasManager.CompleteStore();
            }
        }
        public void UploadSample()
        {
            if (sampleValidator.ValidateValues())
            {
                var sample = sampleValidator.NewSample();
                sampleDAO.AddSample(sample);

                SaveData.Instance.AddAndSaveSubmittedSample(sample);
                submitCanvasManager.CompleteSubmission();

                UpdateFirebaseUserSample(sample);
            }

        }
    
        private void SubmitStoredSamples()
        {
            FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;

            List<Sample> storedSamples = SaveData.Instance.UsersStoredSamples;

            UploadStoredSamples(user, storedSamples);
         //   SaveData.Instance.ClearSubmittedSamplesList();
            if (user != null)
            {
                userDAO.UpdateUserSampleCount(user, storedSamples.Count);
            }
            SaveData.Instance.UpdateSubmittedStoredSamples();
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