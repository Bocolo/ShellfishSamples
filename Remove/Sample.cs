using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Sample
{
   
    public string species;
    [JsonProperty("Production Week No")]
    public int productionWeekNo;
    public string icesRectangleNo;
    public string company;
    public string date;
    public string name;
    public string sampleLocationName;
}
