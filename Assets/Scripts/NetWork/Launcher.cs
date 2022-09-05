using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //Photon Pun namespace
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks //Class wich contains Network Callbacks
{
    [SerializeField] private LauncherUI launcherUI;
    private byte maxPlayersPerRoom = 2;

    void Awake()
    {
        launcherUI = GetComponent<LauncherUI>();
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
        PhotonNetwork.JoinLobby();
        launcherUI.showRoomPanel();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        //PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room avaliable, creating one...");
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = maxPlayersPerRoom});
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRoomWithName(string name)
    {
        PhotonNetwork.JoinOrCreateRoom(name,new RoomOptions{MaxPlayers = maxPlayersPerRoom},default);
    }


}
