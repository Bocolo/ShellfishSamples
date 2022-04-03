using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageLogUI : MonoBehaviour
{
    [SerializeField] List<DataSO> messages;
    [SerializeField] GameObject messageEntryPrefab;
    [SerializeField] Transform messageEntryRoot;
    // [SerializeField] TextMeshProUGUI dataContent;
    //  [SerializeField] RectTransform dataContentRoot;
    void Start()
    {
        Debug.Log(messages.Count +"__________count");
        int counter = 0;
        foreach(var message in messages)
        {
            counter++;
            Debug.Log(counter + "__________counter_________");
            var messageGO = Instantiate(messageEntryPrefab, Vector3.zero, Quaternion.identity, messageEntryRoot);
            //   buttonGO.name = "Selector_" + data.name;
            //   var buttonScript = messageGO.GetComponent<MessageEntryUI>();
            // buttonScript.Bind(message.content);

            messageGO.name = "Selector_" + message.name;
            Debug.Log(messageGO.name);


            //not set to instance--- why.. list is 20, stops afrer one
           // messageGO.GetComponent<MessageEntryUI>().Bind(message.content);

            MessageEntryUI mui = messageGO.GetComponent<MessageEntryUI>();
            Debug.Log("have component "+mui.name);
            mui.Bind(message.content);
            Debug.Log("going to bind");

            //     buttonScript.OnDataSelect.AddListener(OnDataSelected);
        }
    }

}
