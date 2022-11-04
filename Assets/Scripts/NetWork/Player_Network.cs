using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Network : MonoBehaviour, IPunObservable
{
    private Player_Control myPlayer; //The paddle to control
    private SpriteRenderer sRenderer;
    private PhotonView view;

    private Vector2 oldPosition, movement;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        if(view.IsMine)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                myPlayer = GameNetwork.gameNetworkInstance.player1; //If the player is the master client, it will control the player 1
            }
            else
            {
                myPlayer = GameNetwork.gameNetworkInstance.player2; //If the user is not the master client, it will control the player 2
            }
            GameObject.FindObjectOfType<Button_Script>().localPlayer = myPlayer; //Tell the UI button to control the asigned player
            sRenderer = GetComponent<SpriteRenderer>();
            SetColor();
        }
    }

    public void MovePlayerUp()
    {
        myPlayer.moveUp(); //Move the paddle up
    }
    public void MovePlayerDown()
    {
        myPlayer.moveDown(); //Move the paddle down
    }

    public void StopMovingPlayer()
    {
        myPlayer.StopMoving(); //Stops moving the paddle
    }
    public Player_Control GetLocalPlayer()
    {
        return myPlayer; //Returns the player the user is controlling
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
    {
        return;
    }

    public void SetColor()
    {
        sRenderer.color = ColorRewarder.colorRewarderInst.GetCurrentColor();
    }

}
