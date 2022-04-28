using System.Collections.Generic;
using UI.Popup;
using UI.SampleDisplay;
/// <summary>
/// Handles logic related to displaying samples
/// </summary>
public class ShowSampleLogic 
{
    /// <summary>
    ///  displays the passed samples list in the ui,
    /// if there are no samples,call the Show Samples Failed methos
    /// </summary>
    /// <param name="popUp">pop up to use in if case</param>
    public void ShowSamples(SampleUI sampleUI,List<Sample> sampleList, PopUp popUp, string popupText)
    {
        if (sampleList.Count == 0)
        {
     
            ShowSamplesFailed(sampleUI, popUp,popupText);
        }
        else
        {
            sampleUI.AddTextAndPrefab(sampleList);
        }
    }
    /// <summary>
    ///  activates the pop up with the passed text
    /// and destroys previously loaded panels
    /// </summary>
    /// <param name="popUp">pop up to use in if case</param>
    public void ShowSamplesFailed(SampleUI sampleUI,PopUp popUp, string popupText)
    {
        popUp.SetPopUpText(popupText);
        sampleUI.DestroyParentChildren();
    }
}
