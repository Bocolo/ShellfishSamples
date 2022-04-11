using UnityEngine;
namespace UI.Orientation
{
    public class OrientationLock : MonoBehaviour
    {
        void Start()
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        private void Update()
        {
            if (Screen.orientation == ScreenOrientation.Landscape)
            {
                Screen.orientation = ScreenOrientation.Portrait;
            }
        }
    }
}