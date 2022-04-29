using Samples.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace App.Samples.UI
{

    /// <summary>
    /// Manages the UI for Displaying samples to the user
    /// </summary>
    public class SamplePanelGenerator : MonoBehaviour
    {

        [SerializeField] private List<GameObject> _samplePanelPrefabs;
        [SerializeField] private Transform _contentParent;
        private SampleLogic _sampleDetails;

        /// <summary>
        /// creates the _sampleDetails object
        /// </summary>
        private void Awake()
        {
            _sampleDetails = new SampleLogic();
        }
        #region "Add Text and Prefab methods"
        /// <summary>
        /// Loads and displays a prefab with the details of the passed sample
        /// </summary>
        /// <param name="sample">sample to display</param>
        public void AddTextAndPrefab(Sample sample)
        {
            GameObject panel = Instantiate(_samplePanelPrefabs[0]);
            SetPanelParent(panel, _contentParent);
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
        #endregion
        #region "Panel Settings"
        /// <summary>
        /// sets the parent of the passed game object to the _contentParent
        /// sets the passed game object scale
        /// </summary>
        /// <param name="panel"></param>
        private void SetPanelParent(GameObject panel, Transform contentParent)
        {
            panel.transform.SetParent(contentParent.transform);
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
            panelText.text = _sampleDetails.SampleToString(sample);//should move this as passed param
        }
        #endregion
        #region "Panel children: destruction and creation"
      
        /// <summary>
        /// Destroys the children of _contentParent
        /// </summary>
        public void DestroyParentChildren()
        {
            foreach (Transform child in _contentParent)
            {
                Destroy(child.gameObject);
            }
        }
        /// <summary>
        /// Destroys the children of _contentParent passed param
        /// </summary>
        /// <param name="_contentParent"></param>
        public void DestroyParentChildren(Transform contentParent)
        {
            foreach (Transform child in contentParent)
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
                GameObject panel = Instantiate(_samplePanelPrefabs[prefabCount - 1]);
                prefabCount -= 1;
                Debug.Log("the panel being used: " + (prefabCount));
                SetPanelParent(panel, _contentParent);
                SetPanelText(panel, sampleList[i]);
            }
        }
        #endregion
    }
}