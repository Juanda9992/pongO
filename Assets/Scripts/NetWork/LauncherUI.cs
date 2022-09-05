using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class LauncherUI : MonoBehaviour
{
    [SerializeField] private GameObject RoomPanel;
    [SerializeField] private TextMeshProUGUI pingText,conectedToRegionText, conectingText;

    public void showRoomPanel()
    {
        conectingText.gameObject.SetActive(false);
        RoomPanel.SetActive(true);
        conectedToRegionText.text = "Conected to " +PhotonNetwork.CloudRegion;
    }

    public void ShowJoiningText()
    {
        conectingText.gameObject.SetActive(true);
        conectingText.text = "JOINING...";
    }

    void Update()
    {
        pingText.text = PhotonNetwork.GetPing().ToString();
    }
}
