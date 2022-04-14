using Firebase.Auth;
using System.Collections.Generic;
using UnityEngine;
using UI.SampleDisplay;
using Data.Access;
using Save.Manager;
using UI.Retrieve;
using UI.Popup;
using System.Collections;
using System;

namespace Data.Display

{
    public class ShowSamplesUI : MonoBehaviour
    {
        //https://www.youtube.com/watch?v=b5h1bVGhuRk&t=276s
        private List<Sample> collectionSamples = new List<Sample>();
        private SampleDAO sampleDAO;
        private SampleUI sampleUI;
        private SearchSampleUI searchSampleUI;
        private void Awake()
        {
            sampleUI = GetComponent<SampleUI>();
            searchSampleUI = GetComponent<SearchSampleUI>();
            sampleDAO = new SampleDAO();
        }
        public void ShowStoredSamples(PopUp popUp)
        {
            sampleUI.AddTextAndPrefab(SaveData.Instance.LoadAndGetStoredSamples());
            if (SaveData.Instance.UsersStoredSamples.Count == 0)
            {
                popUp.SetPopUpText("There are no stored samples");

            }
        }



        public void ShowAllDeviceSubmittedSamples(PopUp popUp)
        {
            sampleUI.AddTextAndPrefab(SaveData.Instance.LoadAndGetSubmittedSamples());
            if (SaveData.Instance.UsersSubmittedSamples.Count == 0)
            {
                popUp.SetPopUpText("No samples have been submitted");

            }
        }
        public async void ShowUserSubmittedSamples(PopUp popUp)
        {
            //   sampleDAO = new SampleDAO();
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                collectionSamples = await sampleDAO.GetAllUserSubmittedSamples(auth.CurrentUser);
                sampleUI.AddTextAndPrefab(collectionSamples);
            }
            else
            {
                //pop up with text - you need to sign in to show user submitted samples
                popUp.SetPopUpText("You must be signed in to view these");
                //popUp.SetActive(true);
            }
        }
        public async void ShowSearchSamples()
        {
            searchSampleUI.SetSearchValues();
            sampleDAO = new SampleDAO();
            //This is ugly looking
            collectionSamples = await sampleDAO.GetSamplesBySearch(
                sampleDAO.SetTestQuery(
                    searchSampleUI.SearchFieldSelection,
                    searchSampleUI.SearchNameSelection,
                    searchSampleUI.SearchLimitSelection
                    )
                );
            sampleUI.AddTextAndPrefab(collectionSamples);
            /*     collectionSamples = await sampleDAO.GetSamplesBySearch(
                   searchSampleUI.GetSearchFieldSelection(),
                   searchSampleUI.GetSearchNameSelection(),
                   searchSampleUI.GetSearchLimitSelection());
                 sampleUI.AddTextAndPrefab(collectionSamples);*/
        }

        public void LoadAndLogAllSamples()
        {
            SaveData.Instance.LoadFullData();
            List<Sample> allsamples = SaveData.Instance.AllSamples;
            for (int i = 0; i < allsamples.Count; i++)
            {
                Debug.Log(allsamples[i].Name);
            }
        }
 
#if UNITY_INCLUDE_TESTS
        public void SetUpTestVariables()
        {
            sampleUI = this.gameObject.AddComponent<SampleUI>();
            searchSampleUI = GetComponent<SearchSampleUI>();
            searchSampleUI.SetUpTestVariables();
        }
#endif
    }
}
/*       public void AddSamplesFromLoadedList(int start)
        {
            int end = (start + 100);
            Debug.Log("End is: "+end);
            if (end > 2316)
            {
                end = 2316;
            }
            sampleDAO = new SampleDAO();
            SaveData.Instance.LoadFullData();
            List<Sample> allsamples = SaveData.Instance.AllSamples;
            for (int i = start; i < end; i++)
            {
                Debug.Log(allsamples[i].Name);
                sampleDAO.AddSampleStripped(allsamples[i]);
                Debug.Log("Added to all samples Stripped -");
            }
        }
  */
/*        public void AddSampleStripped(Sample sample)
        {
            Debug.Log("adding");
            //  firestore.Document(_samplePath).SetAsync(sample); //,SetOptions.MergeAll);
            firestore.Collection("AllSampleDB").Document().SetAsync(sample).ContinueWithOnMainThread(task => {
                Debug.Log("Added data to the AllSampleDB in the sampldb collection.");
            }); ;
            Debug.Log("added");
        }*/
/*        public void TerminateFB()
        {
            sampleDAO = new SampleDAO();
            sampleDAO.Term();
            Debug.Log("Terminated fb");
        }*/