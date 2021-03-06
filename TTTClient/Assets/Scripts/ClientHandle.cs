
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
//using System.Numeric;

public class ClientHandle : MonoBehaviour
{

    //HOW TO HANDLE THESE TYPES OF PACKETS
    public static void Welcome(Packet _packet)
    {

        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.players[_id].transform.position = _position;
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }

    public static void LoginInfo(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        GameObject.Find("LoginScreen").GetComponent<Login>().ErrorText(_msg);
        if (_msg[0] == '0')
        {
            UIManager.instance.GetComponent<UIManager>().TransitionToGame();
        }
        //Client.instance.myId = _myId;
        //ClientSend.LoginInfoRecieved();
    }

    public static void RegisterInfo(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        GameObject.Find("Canvas").GetComponent<Registration>().ErrorText(_msg);
      
        //LoginScreen.instance.GetComponent<Text>().text = _msg;
        //Client.instance.myId = _myId;
        //ClientSend.RegisterInfoRecieved();
    }

}
