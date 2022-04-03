using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    /*
        private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
     */
}


//public static GameManager instance = null;
//bool gameHasEnded = false;
//[SerializeField] float restartDelay = 2f;
//public int score = 0;
//private void Awake()
//{
//    if (instance == null)
//    {
//        instance = this;
//    }
//    else
//    {
//        Destroy(gameObject);
//    }
//}
//public void GameOver()
//{
//    if (gameHasEnded == false)
//    {
//        gameHasEnded = true;
//        Invoke("Restart", restartDelay);
//    }
//}
//void Restart()
//{
//    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//    // SceneManager.LoadScene(0);
//}