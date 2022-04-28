using UnityEngine;
using UnityEngine.SceneManagement;
namespace App.Navigation
{
    /// <summary>
    /// Class to return to the Menu Scene / scene 0
    /// </summary>
    public class BackToMenu : MonoBehaviour
    {
        /// <summary>
        /// Loads the scene at build index 0
        /// </summary>
        public void ReturnToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}