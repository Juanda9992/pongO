using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //Photon Pun namespace
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks //Class wich contains Network Callbacks
{
    [SerializeField] private byte maxPlayersPerRoom = 2;
    void Start()
    {
    }

    public void Connect()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom(); //If the client is already connected, it will join  a rondom room
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(); //Else it will connect to a master server
            PhotonNetwork.GameVersion = "1";
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.LogFormat("Conected to {0}" ,PhotonNetwork.CloudRegion); //When the player is connected, it will call a Debug Method and join  a random room
        PhotonNetwork.JoinLobby();
        Debug.Log(PhotonNetwork.IsMasterClient);
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room avaliable, creating one...");
        Debug.Log(PhotonNetwork.IsMasterClient);
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = maxPlayersPerRoom});
    }

}
