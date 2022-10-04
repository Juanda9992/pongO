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
        localPlayer.moveUp();
    }
    public void MovePlayerDown()
    {
        localPlayer.moveDown();
    }

    public void StopMovingPlayer()
    {
        localPlayer.StopMoving();
    }
}
