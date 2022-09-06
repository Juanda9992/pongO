using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class LauncherUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject RoomPanel,MenuPanel; //Panels to activate and deactivate
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI pingText,conectedToRegionText, conectingText,logText; //All the text

    public bool inRoom; //The player is in a room?

    public void showRoomPanel()
    {
        conectingText.gameObject.SetActive(false);
        RoomPanel.SetActive(true);
        conectedToRegionText.text = "Conected to " +PhotonNetwork.CloudRegion;
    }

    public void HideAllPanels()
    {
        RoomPanel.SetActive(false);
    }

    public void ShowJoiningText(string text) //Sohws the placeholder text with a custom message
    {
        conectingText.gameObject.SetActive(true);
        conectingText.text = text;
    }
    public void showMainPanel() //Disables the Room panel and enables the Main Menu Panel
    {
        MenuPanel.SetActive(true);
        RoomPanel.SetActive(false);
    }

    public void UpdateLogText(string error)
    {
        logText.gameObject.SetActive(true);
        logText.text = "Disconnected from the server by " + error; //shows the log text at the botton of the screen
    }

    void Update()
    {
        pingText.text = PhotonNetwork.GetPing().ToString(); //Displays the ping of the user on the right corner of the screen
        if(inRoom)
        {
            //If the player is in a room, the text will show a message with the number of players in the room
            conectingText.text = "WAITING FOR PLAYERS" + "\n" +PhotonNetwork.CurrentRoom.PlayerCount + " / 2";
        }
    }



}
