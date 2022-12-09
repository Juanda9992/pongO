using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Control : MonoBehaviour, IPunObservable
{
    private Rigidbody2D rb;
    [SerializeField] private float minY,maxY;
    private Vector2 networkPosition;
    public float speed = 10;
    private PhotonView view;
    // Start is called before the first frame update
    void Awake()
    {
        if(!PhotonNetwork.OfflineMode)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                GameNetwork.gameNetworkInstance.player1 = this.GetComponent<Player_Control>(); //If the player is the master client, it will control the player 1
            }
            else
            {
                GameNetwork.gameNetworkInstance.player2 = this.GetComponent<Player_Control>(); //Else, the local player will be the player 2
            }
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
    }

    public void moveUp()
    {
        if(transform.position.y < maxY)
        {    
            rb.velocity = transform.up * speed; //MOves the paddle up
        }
    }

    public void moveDown()
    {
        if(transform.position.y > minY)
        {
            rb.velocity = -transform.up * speed; //Moves the player down
        }
    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero; //Not moving the player
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x,Mathf.Clamp(transform.position.y,minY,maxY)); //Clamps the position of the player to not move outside the map
    }

    private void FixedUpdate() 
    {
        if(!PhotonNetwork.OfflineMode)
        {
            if(!view.IsMine)
            {
                rb.position = Vector2.MoveTowards(rb.position, networkPosition, Time.fixedDeltaTime); //Smoothly moves the ball to the network position
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
    {
        if(stream.IsWriting) //If we are the master client, we are sending data
        {
            stream.SendNext(transform.position); //Send the position of the ball
            stream.SendNext(rb.velocity); //Send the velocity of the ball
        }
        else if(stream.IsReading) //If we are not the master client, we will read the data sended by the master client
        {
            float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.SentServerTime)); //Calculate the lag
            networkPosition = (Vector3)stream.ReceiveNext(); //The Vector2 Network Position will receive the position sent by the master client
            rb.velocity = (Vector2)stream.ReceiveNext(); //The velocity of the rigidbody will be the velocity sended by the master client

            networkPosition += (this.rb.velocity * lag); //Increases the value of the speed to the network position and multiplies it by the lag value
        }
    }

}
