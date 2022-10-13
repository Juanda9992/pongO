using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class LauncherUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject RoomPanel,MenuPanel, CustomRoomPanel; //Panels to activate and deactivate
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

    public void UpdateLogText(string error, bool externalReason = true)
    {
        if(externalReason)
        {
        logText.text = "Disconnected from the server by " + error; //shows the log text at the botton of the screen
        }
        else
        {
            logText.text = error;
        }
        logText.gameObject.SetActive(true);
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
    public IEnumerator OnRoomNotFound()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.Disconnect();
        yield return new WaitForSeconds(0.2f);
        UpdateLogText("Cant´t find the room, are you searching the right name?",false);
    }

    public void StartSearchingRoomCoroutine()
    {
        StartCoroutine(OnRoomNotFound());
    }

    public void HidePanelsOnDisconect()
    {
        conectingText.gameObject.SetActive(false);
        RoomPanel.SetActive(false);
        CustomRoomPanel.SetActive(false);
        MenuPanel.SetActive(true);

    }



}
