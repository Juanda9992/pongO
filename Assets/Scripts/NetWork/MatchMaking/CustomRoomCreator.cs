using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
public class CustomRoomCreator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private Slider pointsSlider;
    private ExitGames.Client.Photon.Hashtable matchMakingOptions = new ExitGames.Client.Photon.Hashtable();
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
        roomName = RoomName.ToUpper();
        
    }

    public void UpdateRoomPoints()
    {
        roomPoints = pointsSlider.value;
        matchMakingOptions["Points"] = roomPoints; 
        pointsText.text = roomPoints.ToString();
    }

    public void CreateRoom()
    {
        if(roomName != null)
        {
            PhotonNetwork.CreateRoom(roomName,new RoomOptions{MaxPlayers = 2, CustomRoomProperties = matchMakingOptions},TypedLobby.Default);
            launcher.HideAllPanels(); //Hides all the panels
            launcher.ShowJoiningText("CREATING ROOM... " + roomName); //Displays the text with the name of the room
        }
        else
        {
            Debug.LogWarning("THE ROOM NAME CANNOT BE EMPTY");
        }
    }

}
