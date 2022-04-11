using Firebase.Auth;
using System.Collections.Generic;
using UnityEngine;
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
        SaveData.Instance.LoadStoredSamples();//MAYBE???
        sampleUI.AddTextAndPrefab(SaveData.Instance.GetUserStoredSamples());
    }
    public void ShowAllDeviceSubmittedSamples()
    {
        SaveData.Instance.LoadSubmittedSamples();//MAYBE???
        sampleUI.AddTextAndPrefab(SaveData.Instance.GetUserSubmittedSamples());
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
        collectionSamples = await sampleDAO.GetSamplesBySearch(searchSampleUI.GetSearchFieldSelection(), searchSampleUI.GetSearchNameSelection(), searchSampleUI.GetSearchLimitSelection());
        sampleUI.AddTextAndPrefab(collectionSamples);
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
