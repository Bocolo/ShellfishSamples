using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.SampleDisplay
{
    /// <summary>
    /// Manages the UI for Displaying samples to the user
    /// </summary>
    public class SampleUI : MonoBehaviour
    {
     
        [SerializeField] private List<GameObject> _samplePanelPrefabs;
        [SerializeField] private Transform _contentParent;
        /// <summary>
        /// Loads and displays a prefab with the details of the passed sample
        /// </summary>
        /// <param name="sample">sample to display</param>
        public void AddTextAndPrefab(Sample sample)
        {
            GameObject panel = Instantiate(_samplePanelPrefabs[0]);
            SetPanelParent(panel);
            SetPanelText(panel, sample);
        }
        /// <summary>
        /// removes all children on the content parent
        /// Loads and displays prefabs with the details of the passed samples list
        /// </summary>
        /// <param name="sampleList">samples to display</param>
        public void AddTextAndPrefab(List<Sample> sampleList)
        {
            DestroyParentChildren(_contentParent);
            CreatePanelChildren(sampleList);
        }
        /// <summary>
        /// sets and  returns the string of the sample details, restricting some information
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        private String RestrictedSampleToString(Sample sample)
        {
            if (sample.SampleLocationName == null)
            {
                return ("\nSpecies: " + sample.Species
               + $"\nICEs Rectangle: {sample.IcesRectangleNo}"
               + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date);
            }
            else
            {
                return ("\nSpecies: " + sample.Species
                + "\nLocation: " + sample.SampleLocationName + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date);
            }
        }
        /// <summary>
        /// sets and returns the string of the sample details
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        private String SampleToString(Sample sample)
        {
            if (sample.SampleLocationName == null)
            {
                return ("Name: " + sample.Name + "\nCompany: " + sample.Company + "\nSpecies: " + sample.Species
               + $"\nICEs Rectangle: {sample.IcesRectangleNo}"
               + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date + "\nComment: " + sample.Comment);
            }
            else
            {
                return ("Name: " + sample.Name + "\nCompany: " + sample.Company + "\nSpecies: " + sample.Species
                + "\nLocation: " + sample.SampleLocationName + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date
                + "\nComment: " + sample.Comment);
            }
        }
        /// <summary>
        /// sets the parent of the passed game object to the _contentParent
        /// sets the passed game object scale
        /// </summary>
        /// <param name="panel"></param>
        private void SetPanelParent(GameObject panel)
        {
            panel.transform.SetParent(_contentParent.transform);
            panel.transform.localScale = new Vector3(1, 1, 1);
        }
        /// <summary>
        /// sets the text of the passed game object to 
        /// the sample details
        /// </summary>
        /// <param name="panel"></param>
        private void SetPanelText(GameObject panel, Sample sample)
        {
            Text panelText = panel.transform.GetChild(0).gameObject.GetComponent<Text>();
            panelText.text = SampleToString(sample);
        }
        /// <summary>
        /// Destroys the children of _contentParent passed param
        /// </summary>
        /// <param name="_contentParent"></param>
        public void DestroyParentChildren(Transform _contentParent)
        {
            foreach (Transform child in _contentParent)
            {
                Destroy(child.gameObject);
            }
        }    /// <summary>
             /// Destroys the children of _contentParent
             /// </summary>
        public void DestroyParentChildren( )
        {
            foreach (Transform child in _contentParent)
            {
                Destroy(child.gameObject);
            }
        }
        /// <summary>
        /// instantiates panel object and sets their text and parent
        /// the details of each item in the samplesList is set as the text 
        /// </summary>
        /// <param name="sampleList"></param>
        private void CreatePanelChildren(List<Sample> sampleList)
        {
            int prefabCount = _samplePanelPrefabs.Count;
            for (int i = 0; i < sampleList.Count; i++)
            {
                if (prefabCount <= 0)
                {
                    prefabCount = _samplePanelPrefabs.Count;
                }
                GameObject panel = Instantiate(_samplePanelPrefabs[prefabCount-1]);
                prefabCount -= 1;
                Debug.Log("the panel being used: " + (prefabCount));
                SetPanelParent(panel);
                SetPanelText(panel, sampleList[i]);
            }
        }
#if UNITY_INCLUDE_TESTS
        public void SetUpTestVariables()
        {
            GameObject go = new GameObject();
            _contentParent = go.GetComponent<Transform>();
            GameObject blueChild = new GameObject();
            GameObject redChild = new GameObject();
            _samplePanelPrefabs = new List<GameObject>();
            _samplePanelPrefabs.Add(new GameObject());
            _samplePanelPrefabs.Add(new GameObject());
            redChild.AddComponent<Text>();
            blueChild.AddComponent<Text>();
        //    _redPanelPrefab.AddComponent<Text>();
            blueChild.transform.SetParent(_samplePanelPrefabs[0].transform);
            redChild.transform.SetParent(_samplePanelPrefabs[1].transform);
        }
        public Transform GetContentParent()
        {
            return this._contentParent;
        }
#endif
    }
}