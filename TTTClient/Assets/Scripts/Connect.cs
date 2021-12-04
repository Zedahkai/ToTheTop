using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connect : MonoBehaviour
{
    public Button connectButton;

    public void ConnectToServer()
    {
        Client.instance.ConnectToServer();
    }

}
