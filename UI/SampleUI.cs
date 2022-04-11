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
        GameObject panel = Instantiate(_redPanelPrefab);
        SetPanelParent(panel);
        SetPanelText(panel, sample);

    }
 
    public void AddTextAndPrefab(List<Sample> sampleList)
    {
        DestroyParentChildren(_contentParent); 
        CreatePanelChildren(sampleList);
    }
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
    private String FullSampleToString(Sample sample)
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
    private void SetPanelParent(GameObject panel)
    {
        panel.transform.SetParent(_contentParent.transform);
        panel.transform.localScale = new Vector3(1, 1, 1);
    }
    private void SetPanelText(GameObject panel, Sample sample)
    {
        Text panelText = panel.transform.GetChild(0).gameObject.GetComponent<Text>();
        panelText.text = FullSampleToString(sample);
    }
    private void DestroyParentChildren(Transform _contentParent)
    {
        foreach (Transform child in _contentParent)
        {
            Destroy(child.gameObject);
        }
    }
    private void CreatePanelChildren(List<Sample> sampleList)
    {
        for (int i = 0; i < sampleList.Count; i++)
        {
            GameObject panel;
            if (i % 2 == 0)
            {
                panel = Instantiate(_redPanelPrefab);
            }
            else
            {
                panel = Instantiate(_bluePanelPrefab);
            }
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
        _bluePanelPrefab = new GameObject();
        _redPanelPrefab = new GameObject();
        redChild.AddComponent<Text>();
        blueChild.AddComponent<Text>();
      
        blueChild.transform.SetParent(_bluePanelPrefab.transform);
        redChild.transform.SetParent(_redPanelPrefab.transform);
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
