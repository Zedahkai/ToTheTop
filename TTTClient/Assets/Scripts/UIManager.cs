using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject mainCamera;
    public GameObject startMenu;
    public InputField usernameField;
    public InputField passwordField;

    //ENSURES ONLY ONE INSTANCE OF UIMANAGER EXISTS
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    //CONNECT CLIENT TO SERVER
    public void ConnectToServer()
    {
        Client.instance.ConnectToServer();
    }

    public void TransitionToGame()
    {
        startMenu.SetActive(false);
        mainCamera.SetActive(false);
        usernameField.interactable = false;
        passwordField.interactable = false;
    }
}
