using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class ServerHandle
{
    public static void WelcomeReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }


    }

    public static void PlayerMovement(int _fromClient, Packet _packet)
    {
        bool[] _inputs = new bool[_packet.ReadInt()];
        for (int i = 0; i < _inputs.Length; i++)
        {
            _inputs[i] = _packet.ReadBool();
        }
        Quaternion _rotation = _packet.ReadQuaternion();

        Server.clients[_fromClient].player.SetInput(_inputs, _rotation);
    }

    public static void LoginInfo(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();
        string _password = _packet.ReadString();

        using (var client = new System.Net.WebClient())
        {
            string _msg;
            var values = new System.Collections.Specialized.NameValueCollection();
            values.Add("username", _username);
            values.Add("password", _password);
            var response = client.UploadValues("http://localhost/sqlconnect/login.php", values);
            var responseString = Encoding.ASCII.GetString(response);
            if (responseString[0] == '0')
            {
                _msg = $"0 {_username} HAS LOGGED IN SUCCESSFULLY";
                Debug.Log(_msg);
                //Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
                Server.clients[_fromClient].SendIntoGame(_username);
                ServerSend.LoginInfo(_fromClient, _msg);
            }
            else
            {
                _msg = $"LOGGIN FAILED: " + responseString;
                Debug.Log(responseString);
                ServerSend.LoginInfo(_fromClient, _msg);
            }

        }

    }

    public static void RegisterInfo(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();
        string _password = _packet.ReadString();

        using (var client = new System.Net.WebClient())
        {
            string _msg;
            var values = new System.Collections.Specialized.NameValueCollection();
            values.Add("username", _username);
            values.Add("password", _password);
            var response = client.UploadValues("http://localhost/sqlconnect/register.php", values);
            var responseString = Encoding.ASCII.GetString(response);
            if (responseString[0] == '0')
            {
                _msg = $"0 {_username} HAS CREATED AN ACCOUNT SUCCESSFULLY";
                Debug.Log(_msg);
                //Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
                // Server.clients[_fromClient].SendIntoGame(_username);
                ServerSend.RegisterInfo(_fromClient, _msg);

            }
            else
            {
                _msg = "ACCOUNT CREATION FAILED " + responseString;
                Debug.Log(_msg);
                ServerSend.RegisterInfo(_fromClient, _msg);
            }
            //Server.clients[_fromClient].Disconnect();
        }
    }
}
