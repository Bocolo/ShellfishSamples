using App.Samples.UI;
using App.SaveSystem.Manager;
using App.UI;
using Firebase.Auth;
using Samples.Data;
using Samples.Data.Access;
using System.Collections.Generic;
using UnityEngine;
namespace App.Samples.Manager
{

    /// <summary>
    /// Manages actions for disaplying samples
    /// </summary>
    public class ShowSamplesManager : MonoBehaviour
    {

        private List<Sample> _collectionSamples = new List<Sample>();
        private SampleDAO _sampleDAO;
        private SamplePanelGenerator _samplePanelGenerator;
        private SearchSampleUI _searchSampleUI;
        private ShowSampleLogic _showSample;
        /// <summary>
        /// gets the_samplePanelGenerator,_searchSampleUI components 
        /// creates _sampleDAO and _showSample objects 
        /// </summary>
        private void Awake()
        {
            _samplePanelGenerator = GetComponent<SamplePanelGenerator>();
            _searchSampleUI = GetComponent<SearchSampleUI>();
            _sampleDAO = new SampleDAO();
            _showSample = new ShowSampleLogic();
        }
        /// <summary>
        /// calls the Show Samples method to Load and displays stored samples
        /// </summary>
        /// <param name="popUp">pop up to use in if case</param>
        public void ShowStoredSamples(PopUp popUp)
        {
            _showSample.ShowSamples(_samplePanelGenerator,
                SaveData.Instance.LoadAndGetStoredSamples(),
                popUp, "There are no stored samples");

        }
        /// <summary>
        /// calls the Show Samples method to Load and display submitted samples
        /// </summary>
        /// <param name="popUp">pop up to use in if case</param>
        public void ShowAllDeviceSubmittedSamples(PopUp popUp)
        {
            _showSample.ShowSamples(_samplePanelGenerator, SaveData.Instance.LoadAndGetSubmittedSamples(),
                popUp, "No samples have been submitted");

        }

        /// <summary>
        /// loads and displays Firebase user submitted samples,
        /// if there is no Firebase user , the pop up activates with the passed text
        /// </summary>
        /// <param name="popUp">pop up to use in if case</param>
        public async void ShowUserSubmittedSamples(PopUp popUp)
        {
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                _collectionSamples = await _sampleDAO.GetAllUserSubmittedSamples(auth.CurrentUser);
                _showSample.ShowSamples(_samplePanelGenerator, _collectionSamples, popUp,
                    "You have not submitted any samples");
            }
            else
            {
                _showSample.ShowSamplesFailed(_samplePanelGenerator, popUp,
                    "You Must be Signed In to View These");

            }
        }
        /// <summary>
        /// Load and displays the sample list that resuls 
        /// from a search on the firestore database
        /// </summary>
        public async void ShowSearchSamples(PopUp popUp)
        {
            _searchSampleUI.SetSearchValues();
            _collectionSamples = await _sampleDAO.GetSamplesBySearch(
                _sampleDAO.SetQuery(
                    _searchSampleUI.SearchFieldSelection,
                    _searchSampleUI.SearchNameSelection,
                    _searchSampleUI.SearchLimitSelection
                    )
                );
            _showSample.ShowSamples(_samplePanelGenerator, 
                _collectionSamples, popUp,
                "No matching samples found");
        }

    }
}
