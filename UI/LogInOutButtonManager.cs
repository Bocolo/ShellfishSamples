using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class LogInOutButtonManager : MonoBehaviour
{
    [SerializeField] private bool isLoggedIn;
    [SerializeField] private Button logInButton;
    [SerializeField] private Button signoutButton;

    private void Start()
    {
        SetLoggedIn(FirebaseAuth.DefaultInstance.CurrentUser != null);
    }

    public void SetLoggedIn(bool isLoggedIn)
    {
        this.isLoggedIn = isLoggedIn;
        Debug.Log("Is someone logged in ?? " + this.isLoggedIn);
        SetLogInOutButtonInteractable();

    }
    private void SetLogInOutButtonInteractable()
    {
        if (this.isLoggedIn)
        {
            logInButton.interactable = false;
            signoutButton.interactable = true;
            Debug.Log("Loggin button should be inactive");
        }
        else
        {
            logInButton.interactable = true;
            signoutButton.interactable = false;
            Debug.Log("SIGNOUY button should be inactive");

        }
    }
}