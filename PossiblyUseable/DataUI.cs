using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DataUI : MonoBehaviour
{

    [SerializeField] List<DataSO> data;
    [SerializeField] GameObject ButtonUIPrefeab;
    [SerializeField] Transform ButtonUIRoot;
    [SerializeField] TextMeshProUGUI dataContent;
    [SerializeField] RectTransform dataContentRoot;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var data in data)
        {
            var buttonGO = Instantiate(ButtonUIPrefeab, Vector3.zero, Quaternion.identity, ButtonUIRoot);
            buttonGO.name = "Selector_" + data.name;
            var buttonScript = buttonGO.GetComponent<ButtonUIPanel>();
            buttonScript.Bind(data);
            buttonScript.OnDataSelect.AddListener(OnDataSelected);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDataSelected(DataSO data)
    {
        Debug.Log(data.name);
        var dimensions = dataContent.GetPreferredValues(data.content,dataContentRoot.rect.width,dataContentRoot.rect.height);
        Debug.Log(dimensions);
           dataContentRoot.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimensions.y);
         //  dataContentRoot.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dimensions.x);

        dataContent.text = data.content;

    }
}
