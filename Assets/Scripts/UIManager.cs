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
        startMenu.SetActive(false);
        mainCamera.SetActive(false);
        usernameField.interactable = false;
        Client.instance.ConnectToServer();
    }
}
