using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    public GameObject playerPrefab;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }


    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        Server.Start(50, 21586);
    }

    private void OnApplicationQuit() => Server.Stop();

    public Player InstantiatePlayer()
    {
        try
        {

            //Instantiate(playerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity);
            return Instantiate(playerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity).GetComponent<Player>();
        } catch (Exception _ex)
        {
            Debug.Log("Error creating player object");
            return null;
        }
    }
}
