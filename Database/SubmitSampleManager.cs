using Firebase.Auth;
using Firebase.Firestore;
using Submit.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SubmitSampleManager : MonoBehaviour
{
    //    private SubmitCanvasManager canvasManager;
    private SubmitSampleUI submitSampleUI;
    private SampleDAO sampleDAO;
    private UserDAO userDAO;

    private void Awake()
    {
        submitSampleUI = GetComponent<SubmitSampleUI>();
        sampleDAO = new SampleDAO();
        userDAO = new UserDAO();
    }
    public void UploadSample()
    {
        submitSampleUI.SetValues();
        if (!submitSampleUI.IsValuesMissing())
        {
      
            var sample = submitSampleUI.NewSample();
            sampleDAO.AddSample(sample);
            SaveData.Instance.AddToSubmittedSamples(sample);
            SaveData.Instance.SaveSubmittedSamples();
            submitSampleUI.CompleteSubmission();
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                sampleDAO.AddSampleToUserCollection(auth.CurrentUser, sample);
                userDAO.UpdateUserSampleCount(auth.CurrentUser);
            }
        }

    }



    public void SubmitStoredSamples()
    {
        try
        {
      
            var firestore = FirebaseFirestore.DefaultInstance;
            List<Sample> storedSamples = SaveData.Instance.GetUserStoredSamples();
            FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
           // int counter = 0;
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
            userDAO.UpdateUserSampleCount(user, storedSamples.Count);
            SaveData.Instance.ClearStoredSamplesList();
            SaveData.Instance.SaveStoredSamples();
            SaveData.Instance.SaveSubmittedSamples();
        }
        catch (Exception e)
        {
            Debug.LogError(e + " submitting stored data error");
        }
    }
    public void StoreSample()
    {
        submitSampleUI.SetValues();
        if (!submitSampleUI.IsValuesMissing())
        {
            var sample = submitSampleUI.NewSample();
            SaveData.Instance.AddToStoredSamples(sample);
            submitSampleUI.CompleteSubmission();


        }
        SaveData.Instance.SaveStoredSamples();
        // tidy this up??
    }


}
