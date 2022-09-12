using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Script : MonoBehaviour
{
    Player_Control localPlayer;

    void Start()
    {
        localPlayer = GameObject.FindObjectOfType<Player_Network>().GetLocalPlayer();
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
