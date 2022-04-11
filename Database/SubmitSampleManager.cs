using Firebase.Auth;
using Firebase.Firestore;
using Submit.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SubmitSampleManager : MonoBehaviour
{
    //    private SubmitCanvasManager canvasManager;
    private SubmitSampleData submitSampleUI;
    private SampleDAO sampleDAO;
    private UserDAO userDAO;

    private void Awake()
    {
        submitSampleUI = GetComponent<SubmitSampleData>();
        //       canvasManager = GetComponent<SubmitCanvasManager>();
    }
    public void UploadData()
    {
        submitSampleUI.SetValues();
        if (!submitSampleUI.IsValuesMissing())
        {
            userDAO = new UserDAO();
            sampleDAO = new SampleDAO();
            var sample = submitSampleUI.NewSample();
            sampleDAO.AddSample(sample);
            SaveData.Instance.AddToSubmittedSamples(sample);
            SaveData.Instance.SaveSubmittedSamples();
            submitSampleUI.SubmissionComplete();
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                sampleDAO.AddSampleToUserCollection(auth.CurrentUser, sample);
                userDAO.UpdateUserSampleCount(auth.CurrentUser);
            }
        }

    }



    public void SubmitStoredData()
    {
        try
        {
            sampleDAO = new SampleDAO();
            userDAO = new UserDAO();
            var firestore = FirebaseFirestore.DefaultInstance;
            List<Sample> storedSamples = SaveData.Instance.GetUserStoredSamples();
            FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
            int counter = 0;
            for (int i = 0; i < storedSamples.Count; i++)
            {
                sampleDAO.AddSample(storedSamples[i]);
                SaveData.Instance.AddToSubmittedSamples(storedSamples[i]);
                counter++;
                if (user != null)
                {
                    sampleDAO.AddSampleToUserCollection(user, storedSamples[i]);
                }
            }
            userDAO.UpdateUserSampleCount(user, counter);
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
            submitSampleUI.SubmissionComplete();


        }
        SaveData.Instance.SaveStoredSamples();
        // tidy this up??
    }


}
