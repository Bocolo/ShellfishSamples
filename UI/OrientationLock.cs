using UnityEngine;
namespace App.Settings.Orientation
{
    /// <summary>
    /// Manages the orientation of scenes containing this scripts
    /// </summary>
    public class OrientationLock : MonoBehaviour
    {
        /// <summary>
        /// sets orientation to portrait on start
        /// </summary>
        private void Start()
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        /// <summary>
        /// sets orientation to portrait if orientation is in landscape
        /// each time Update is called
        /// </summary>
        private void Update()
        {
            if (Screen.orientation == ScreenOrientation.Landscape)
            {
                Screen.orientation = ScreenOrientation.Portrait;
            }
        }
    }
}