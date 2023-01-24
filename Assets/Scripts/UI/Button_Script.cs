using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Script : MonoBehaviour
{
    public Player_Control localPlayer; //The local player to control

    void Start()
    {
        localPlayer = GameObject.FindObjectOfType<Player_Network>().GetLocalPlayer(); //The player to control is the local player
    }

    public void MovePlayerUp()
    {
        Debug.Log("Moving Up");
        localPlayer.moveUp();
        
    }
    public void MovePlayerDown()
    {
        Debug.Log("Moving Down");
        localPlayer.moveDown();
    }

    public void StopMovingPlayer()
    {
        Debug.Log("NotMoving");
        localPlayer.StopMoving();
    }
}
