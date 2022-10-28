using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Network : MonoBehaviour, IPunObservable
{
    private Player_Control myPlayer; //The paddle to control

    private PhotonView view;

    private Vector2 oldPosition, movement;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        Debug.Log(view.IsMine);
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
        }
    }

    private void Update()
    {
        if(!view.IsMine)
        {
            transform.position = Vector2.zero;
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
        if(stream.IsWriting) //If we are the master client, we are sending data
        {
            oldPosition = myPlayer.transform.position;
            movement = (Vector2)transform.position - (Vector2)oldPosition;
            
            stream.SendNext(movement); //Send the position of the ball
        }
        else if(stream.IsReading) //If we are not the master client, we will read the data sended by the master client
        {
            float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.SentServerTime)); //Calculate the lag
            movement = (Vector2)stream.ReceiveNext();
        }
    }
}
