using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
public class CustomRoomCreator : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI pointsText, errorText;
    [SerializeField] private Slider pointsSlider;
    private ExitGames.Client.Photon.Hashtable matchMakingOptions = new ExitGames.Client.Photon.Hashtable(); //Hashtable for matchmaking
    private string roomName;
    private float roomPoints = 3;
    private LauncherUI launcher;

    private void Start()
    {
        UpdateRoomPoints();
        launcher = GameObject.FindObjectOfType<LauncherUI>();
    }
    public void SetRoomName(string RoomName)
    {
        roomName = RoomName.ToUpper(); //Sets the name of the custom room
    }

    public void ClearRoomName()
    {
        roomName = "";
    }

    public void UpdateRoomPoints() //Sets the points for the custom room
    {
        roomPoints = pointsSlider.value;
        matchMakingOptions["Points"] = roomPoints; 
        pointsText.text = roomPoints.ToString(); //Updates text 
    }

    public void CreateRoom()
    {
        if(roomName != null) //If the room name is not empty
        {
            if(roomName.Length == 5) //If it has 5 characters
            {
                PhotonNetwork.CreateRoom(roomName,new RoomOptions{MaxPlayers = 2, CustomRoomProperties = matchMakingOptions, IsVisible = false},TypedLobby.Default); //Creates the room and set the properties
                GameObject.Find("CUSTOM ROOM PANEL").SetActive(false);
                launcher.ShowJoiningText("CREATING ROOM... " + roomName); //Displays the text with the name of the room
            }
            else //If not, a error message will be displayed
            {
                errorText.gameObject.SetActive(true);
                errorText.text = "The room name must have 5 characters";
            }
        }
        else
        {       //If is empty, another error message will be displayed
                errorText.gameObject.SetActive(true);
                errorText.text = "The room name cannot be empty";
        }
    }

    public void JoinRandomRoom()
    {
        matchMakingOptions["Points"] = 3; //Default points will be filtered
        PhotonNetwork.JoinRandomRoom(); //Joins a random room using the filter
        launcher.ShowJoiningText("JOINING TO A ROOM...");
    }

    
    public override void OnJoinRandomFailed(short returnCode, string message) //If the player cant join to the room (No room existing or all the avaliables rooms are full, it will create an empty room)
    {
        Debug.Log("No room avaliable, creating one...");
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = 2, IsVisible = true});//Creating room with the specified byte of max players per room
    }

}
