using App.UI;
using Samples.Data;
using System.Collections.Generic;
namespace App.Samples.UI
{
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
        public void ShowSamples(SamplePanelGenerator samplePanelGenerator, List<Sample> sampleList, PopUp popUp, string popupText)
        {
            if (sampleList.Count == 0)
            {

                ShowSamplesFailed(samplePanelGenerator, popUp, popupText);
            }
            else
            {
                samplePanelGenerator.AddTextAndPrefab(sampleList);
            }
        }
        /// <summary>
        ///  activates the pop up with the passed text
        /// and destroys previously loaded panels
        /// </summary>
        /// <param name="popUp">pop up to use in if case</param>
        public void ShowSamplesFailed(SamplePanelGenerator samplePanelGenerator, PopUp popUp, string popupText)
        {
            popUp.SetPopUpText(popupText);
            samplePanelGenerator.DestroyParentChildren();
        }
    }
}
