using UnityEngine;
using UnityEngine.SceneManagement;
namespace UI.Navigation
{
    public class BackToMenu : MonoBehaviour
    {
        public void ReturnToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}