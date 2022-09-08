using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //Photon Pun namespace
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks //Class wich contains Network Callbacks
{
    [SerializeField] private LauncherUI launcherUI;
    private byte maxPlayersPerRoom = 2; //Set the max players per room (is a byte)
    private string roomName;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true; ///When the master client (the first to enter in the room) loads a level, the other user will do it
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
            launcherUI.ShowJoiningText("CONNECTING TO THE SERVER...");
            PhotonNetwork.ConnectUsingSettings(); //Else it will connect to a master server
            PhotonNetwork.GameVersion = "1";
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(); //When the player connects to the server it automatically will join to a lobby
        launcherUI.showRoomPanel(); //Shows Room Menu
    }
    public override void OnJoinedRoom()
    {
        //When the players enter to a room the console will display all the room´s info
        launcherUI.inRoom = true;
        Debug.Log(PhotonNetwork.InRoom);
        Debug.Log(PhotonNetwork.CurrentLobby + " " + PhotonNetwork.CurrentRoom);
        //PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinRandomFailed(short returnCode, string message) //If the player cant join to the room (No room existing or all the avaliables rooms are full, it will create an empty room)
    {
        Debug.Log("No room avaliable, creating one...");
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = maxPlayersPerRoom});//Creating room with the specified byte of max players per room
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom(); //Joins a random room, no matter the name
        launcherUI.ShowJoiningText("JOINING TO A ROOM...");
    }

    public void SetRoomName(string name)
    {
        roomName = name;
    }

    public void JoinRoomWithName()
    {
        if(roomName != null)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName,new RoomOptions{MaxPlayers = maxPlayersPerRoom},default); //When the input field are deselected, the game will create a room with the name of the value of the thext input
            launcherUI.HideAllPanels(); //Hides all the panels
            launcherUI.ShowJoiningText("JOINING ROOM... " + roomName); //Displays the text with the name of the room
        }
        else
        {
            Debug.LogWarning("THE ROOM NAME CANNOT BE EMPTY");
        }
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect(); //DIsconects the server
    }

    public override void OnDisconnected(DisconnectCause cause) //When the player disconnects the server
    {
        launcherUI.showMainPanel(); //The game will show the main menu
        launcherUI.UpdateLogText(cause.ToString()); //A log text will be shown on the bottom oh the screen
        Debug.LogFormat("Disconnetcted, the reason is {0}", cause);
    }

    public override void OnLeftRoom()
    {
        launcherUI.inRoom = false; //The player is no longer in a room
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName);
        if(PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(1);
            }
        }
    }


}
