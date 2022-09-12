using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Network : MonoBehaviour
{
    private Player_Control myPlayer;

    private PhotonView view;
    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponent<PhotonView>();
        Debug.Log(view.IsMine);
        if(view.IsMine)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                myPlayer = GameNetwork.gameNetworkInstance.player1;
            }
            else
            {
                myPlayer = GameNetwork.gameNetworkInstance.player1;
            }
        }
    }

    public void MovePlayerUp()
    {
        myPlayer.moveUp();
    }
    public void MovePlayerDown()
    {
        myPlayer.moveDown();
    }

    public void StopMovingPlayer()
    {
        myPlayer.StopMoving();
    }
    public Player_Control GetLocalPlayer()
    {
        return myPlayer;
    }
}
