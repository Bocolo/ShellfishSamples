using Data.Access;
using Firebase.Auth;
using Save.Manager;
using System.Collections.Generic;
using UI.Popup;
using UI.Retrieve;
using UI.SampleDisplay;
using UnityEngine;
namespace Data.Display
{
    /// <summary>
    /// Manages buttons actions for disaplying samples
    /// </summary>
    public class ShowSamplesUI : MonoBehaviour
    {
        //https://www.youtube.com/watch?v=b5h1bVGhuRk&t=276s
        private List<Sample> _collectionSamples = new List<Sample>();
        private SampleDAO _sampleDAO;
        private SampleUI _sampleUI;
        private SearchSampleUI _searchSampleUI;
        /// <summary>
        /// set _sampleUI,_searchSampleUI and _sampleDAO on awake
        /// </summary>
        private void Awake()
        {
            _sampleUI = GetComponent<SampleUI>();
            _searchSampleUI = GetComponent<SearchSampleUI>();
            _sampleDAO = new SampleDAO();
        }
        /// <summary>
        /// Loads and displays stored samples,
        /// if there are no stored samples activate the pop up with the passed text
        /// </summary>
        /// <param name="popUp">pop up to use in if case</param>
        public void ShowStoredSamples(PopUp popUp)
        {
            List<Sample> loadedSamples = SaveData.Instance.LoadAndGetStoredSamples();
            if (loadedSamples.Count == 0)
            {
                popUp.SetPopUpText("There are no stored samples");
                _sampleUI.DestroyParentChildren();

            }
            else
            {
                _sampleUI.AddTextAndPrefab(loadedSamples);
            }
        }
        /// <summary>
        /// loads and displays Device submitted samples,
        /// if there are no Device submitted samples activate the pop up with the passed text
        /// </summary>
        /// <param name="popUp">pop up to use in if case</param>
        public void ShowAllDeviceSubmittedSamples(PopUp popUp)
        {
            List<Sample> loadedSamples = SaveData.Instance.LoadAndGetSubmittedSamples();
            if (loadedSamples.Count == 0)
            {
                popUp.SetPopUpText("No samples have been submitted");
                _sampleUI.DestroyParentChildren();
            }
            else
            {
                Debug.Log("Loaded in show all devices count is " + loadedSamples.Count);
                _sampleUI.AddTextAndPrefab(loadedSamples);
            }
        }
        /// <summary>
        /// loads and displays Firebase user  submitted samples,
        /// if there is no Firebase user , the pop up activates with the passed text
        /// </summary>
        /// <param name="popUp">pop up to use in if case</param>
        public async void ShowUserSubmittedSamples(PopUp popUp)
        {
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                _collectionSamples = await _sampleDAO.GetAllUserSubmittedSamples(auth.CurrentUser);
                _sampleUI.AddTextAndPrefab(_collectionSamples);
            }
            else
            {
                popUp.SetPopUpText("You must be signed in to view these");
                _sampleUI.DestroyParentChildren();

            }
        }
        /// <summary>
        /// Load and displays the sample list that resuls 
        /// from a search on the firestore database
        /// </summary>
        public async void ShowSearchSamples()
        {
            _searchSampleUI.SetSearchValues();
            _sampleDAO = new SampleDAO();
            _collectionSamples = await _sampleDAO.GetSamplesBySearch(
                _sampleDAO.SetTestQuery(
                    _searchSampleUI.SearchFieldSelection,
                    _searchSampleUI.SearchNameSelection,
                    _searchSampleUI.SearchLimitSelection
                    )
                );
            _sampleUI.AddTextAndPrefab(_collectionSamples);
        }
#if UNITY_INCLUDE_TESTS
        public void SetUpTestVariables()
        {
            _sampleUI = this.gameObject.AddComponent<SampleUI>();
            _searchSampleUI = GetComponent<SearchSampleUI>();
            _searchSampleUI.SetUpTestVariables();
        }
#endif
    }
}
/*
        public void LoadAndLogAllSamples()
        {
            SaveData.Instance.LoadFullData();
            List<Sample> allsamples = SaveData.Instance.AllSamples;
            for (int i = 0; i < allsamples.Count; i++)
            {
                Debug.Log(allsamples[i].Name);
            }
        }*/
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