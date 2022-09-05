using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class LauncherUI : MonoBehaviour
{
    [SerializeField] private GameObject RoomPanel,MenuPanel;
    [SerializeField] private TextMeshProUGUI pingText,conectedToRegionText, conectingText,logText;

    public void showRoomPanel()
    {
        conectingText.gameObject.SetActive(false);
        RoomPanel.SetActive(true);
        conectedToRegionText.text = "Conected to " +PhotonNetwork.CloudRegion;
    }

    public void ShowJoiningText(string text)
    {
        conectingText.gameObject.SetActive(true);
        conectingText.text = text + "...";
    }
    public void showMainPanel()
    {
        MenuPanel.SetActive(true);
        RoomPanel.SetActive(false);
    }

    public void UpdateLogText(string error)
    {
        logText.gameObject.SetActive(true);
        logText.text = "Disconnected from the server by " + error;
    }

    void Update()
    {
        pingText.text = PhotonNetwork.GetPing().ToString();
    }


}
