using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientSend : MonoBehaviour
{
    // TCP AND UDP METHODS TO CREATE AND SEND PACKAGE TO SERVER
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();

        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    //HOW TO SEND PACKET TYPES TO SERVER
    #region Packets
    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);

            SendUDPData(_packet);
        }
    }

    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    public static void LoginInfo()
    {
        using (Packet _packet = new Packet((int)ClientPackets.loginInfo))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);
            _packet.Write(UIManager.instance.passwordField.text);
            //UIManager.instance.passwordField.text = null;
            SendTCPData(_packet);
        }
    }

    public static void RegisterInfo()
    {
        using (Packet _packet = new Packet((int)ClientPackets.registerInfo))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);
            _packet.Write(UIManager.instance.passwordField.text);
            //UIManager.instance.passwordField.text = null;
            SendTCPData(_packet);
        }
    }

    #endregion
}
