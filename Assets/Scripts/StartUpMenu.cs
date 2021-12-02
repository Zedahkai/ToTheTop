using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUpMenu : MonoBehaviour
{
    public Text playerDisplay;
    //public Button playButton;
    public Button loginButton;
    public Button registerButton;

    private void Start()
    {
        //playButton.interactable = false;
        if (DBManager.LoggedIn)
        {
            playerDisplay.text = "Player: " + DBManager.username;
            //playButton.interactable = true;
            loginButton.interactable = false;
            registerButton.interactable = false;
        }
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToLogin()
    {
        SceneManager.LoadScene(2);
    }

}
