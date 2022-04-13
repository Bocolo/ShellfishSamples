using Firebase.Auth;
using System.Collections.Generic;
using UnityEngine;
using UI.SampleDisplay;
using Data.Access;
using Save.Manager;
using UI.Retrieve;
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
        }
        public void ShowStoredSamples()
        {
            sampleUI.AddTextAndPrefab(SaveData.Instance.LoadAndGetStoredSamples());
        }
        public void ShowAllDeviceSubmittedSamples()
        {
            sampleUI.AddTextAndPrefab(SaveData.Instance.LoadAndGetSubmittedSamples());
        }
        public async void ShowUserSubmittedSamples(GameObject popUp)
        {
            sampleDAO = new SampleDAO();
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                collectionSamples = await sampleDAO.GetAllUserSubmittedSamples(auth.CurrentUser);
                sampleUI.AddTextAndPrefab(collectionSamples);
            }
            else
            {
                popUp.SetActive(true);
            }
        }
        public async void ShowSearchSamples()
        {
            searchSampleUI.SetSearchValues();
            sampleDAO = new SampleDAO();
            //This is ugly looking
            Debug.Log("About to Search");
            collectionSamples = await sampleDAO.GetSamplesBySearch(
                sampleDAO.SetTestQuery(searchSampleUI.SearchFieldSelection, 
                searchSampleUI.SearchNameSelection, 
                searchSampleUI.SearchLimitSelection));
            Debug.Log("About to Search should be over");
            sampleUI.AddTextAndPrefab(collectionSamples);
            /*     collectionSamples = await sampleDAO.GetSamplesBySearch(
                   searchSampleUI.GetSearchFieldSelection(),
                   searchSampleUI.GetSearchNameSelection(),
                   searchSampleUI.GetSearchLimitSelection());
                 sampleUI.AddTextAndPrefab(collectionSamples);*/
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
