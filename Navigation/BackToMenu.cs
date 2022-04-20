using UnityEngine;
using UnityEngine.SceneManagement;
namespace UI.Navigation
{
    /// <summary>
    /// Class to return to the Menu Scene
    /// </summary>
    public class BackToMenu : MonoBehaviour
    {
        /// <summary>
        /// Loads the scene at build index 0
        /// </summary>
        public void ReturnToMenu()
        {
            SceneManager.LoadScene(0);
           // Debug.Log("Back to menu");
        }
    }
}