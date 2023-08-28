using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class LauncherUI : MonoBehaviourPunCallbacks
{
    [Header("Panels")]
    [SerializeField] private GameObject RoomPanel,MenuPanel, CustomRoomPanel; //Panels to activate and deactivate
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI conectedToRegionText, conectingText,logText; //All the text

    [SerializeField] private GameObject versusPanel;

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

    public override void OnJoinedRoom()
    {
        conectingText.text = "WAITING FOR PLAYERS" + "\n" +PhotonNetwork.CurrentRoom.PlayerCount + " / 2";
        CheckForPlayersAndUpdateText();
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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        CheckForPlayersAndUpdateText();
    }

    public void CheckForPlayersAndUpdateText()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if(PhotonNetwork.CurrentRoom.IsVisible)
            {
                StartCoroutine("StartRandomMatch");
            }
        }
    }

    public IEnumerator StartRandomMatch()
    {
        versusPanel.SetActive(true);
        yield return new WaitForSeconds(5);
        conectingText.text = "THE MATCH WILL BE " + Room_Stats.Stats_inst.matchPoints + " POINTS";
        yield return new WaitForSeconds(3);
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }



}
