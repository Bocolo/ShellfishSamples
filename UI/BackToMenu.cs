using UnityEngine;
using UnityEngine.SceneManagement;
namespace UI.Menu
{
    public class BackToMenu : MonoBehaviour
    {
        public void ReturnToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}