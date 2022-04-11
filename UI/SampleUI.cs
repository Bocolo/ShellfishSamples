using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SampleUI : MonoBehaviour
{
    [SerializeField] private GameObject _bluePanelPrefab;
    [SerializeField] private GameObject _redPanelPrefab;
    [SerializeField] private Transform _contentParent;
    public void AddTextAndPrefab(Sample sample)
    {
        GameObject panel;
        panel = Instantiate(_redPanelPrefab);
        panel.transform.SetParent(_contentParent.transform);
        // GameObject panelChild = panel.transform.GetChild(0).gameObject;
        Text panelText = panel.transform.GetChild(0).gameObject.GetComponent<Text>();
        panel.transform.localScale = new Vector3(1, 1, 1);
        panelText.text = SampleDataToString(sample, false);
    }
    public void AddTextAndPrefab(List<Sample> sampleList)
    {
        foreach (Transform child in _contentParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < sampleList.Count; i++)
        {
            Debug.Log("in for");
            GameObject panel;
            if (i % 2 == 0)
            {
                panel = Instantiate(_bluePanelPrefab);

            }
            else
            {
                panel = Instantiate(_redPanelPrefab);
            }
            panel.transform.SetParent(_contentParent.transform);
            //    GameObject panelChild = panel.transform.GetChild(0).gameObject;
            Text panelText = panel.transform.GetChild(0).gameObject.GetComponent<Text>();
            panel.transform.localScale = new Vector3(1, 1, 1);
            panelText.text = SampleDataToString(sampleList[i], false);
        }
    }
    private String SampleDataToString(Sample sample, bool isRestricted)
    {
        if (isRestricted)
        {
            if (sample.SampleLocationName == null)
            {
                Debug.Log("Location is null restricted");

                return ("\nSpecies: " + sample.Species
               + $"\nICEs Rectangle: {sample.IcesRectangleNo}"
               + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date);
            }
            else
            {
                Debug.Log("ICES rectangle is null restriced");
                return ("\nSpecies: " + sample.Species
                + "\nLocation: " + sample.SampleLocationName + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date);
            }
        }
        else
        {
            if (sample.SampleLocationName == null)
            {
                Debug.Log("Location is null");

                return ("Name: " + sample.Name + "\nCompany: " + sample.Company + "\nSpecies: " + sample.Species
               + $"\nICEs Rectangle: {sample.IcesRectangleNo}"
               + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date + "\nComment: " + sample.Comment);
            }
            else
            {
                Debug.Log("ICES rectangle is null");
                return ("Name: " + sample.Name + "\nCompany: " + sample.Company + "\nSpecies: " + sample.Species
                + "\nLocation: " + sample.SampleLocationName + "\nWeek: " + sample.ProductionWeekNo + "\nDate: " + sample.Date
                + "\nComment: " + sample.Comment);
            }
        }
    }

#if UNITY_INCLUDE_TESTS
    public void SetUpTestVariables()
    {
        Debug.Log(1);

        GameObject go = new GameObject();
    
        _contentParent = go.GetComponent<Transform>();
        GameObject blueChild = new GameObject();
        GameObject redChild = new GameObject();
        redChild.AddComponent<Text>();
        blueChild.AddComponent<Text>();
        Debug.Log(15);

   
        _bluePanelPrefab = new GameObject();
        Debug.Log(16);
        _redPanelPrefab = new GameObject();
        blueChild.transform.SetParent(_bluePanelPrefab.transform);
        redChild.transform.SetParent(_redPanelPrefab.transform);
        Debug.Log(17);

    }
    public Transform GetContentParent()
    {
        return this._contentParent;
    }
    public void TextAndPrefabTest(List<Sample> samples)
    {
        AddTextAndPrefab(samples);
    }
#endif
}
