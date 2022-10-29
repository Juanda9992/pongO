using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))] //Required Component
public class Ball : MonoBehaviour, IPunObservable
{
    private Rigidbody2D rb;
    private PhotonView view;
    private Match_State state;
    private Vector2 networkPosition;
    private CircleCollider2D ballCollider;
    [SerializeField] private float minStartSpeed,maxStartSpeed;
    // Start is called before the first frame update
    private void Awake()
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        Invoke("AddSpeed",4); //Waits 3 seconds before adding speed to the ball
        state = GameObject.FindObjectOfType<Match_State>();
        ballCollider = GetComponent<CircleCollider2D>();

    }

    private void AddSpeed()
    {
        ballCollider.enabled = true;
        if(PhotonNetwork.IsMasterClient)
        {
            rb.velocity = Random.insideUnitCircle.normalized * Random.Range(8,10);
            if(rb.velocity.x is > -2 and < 2)
            {
                rb.velocity = new Vector2(6,rb.velocity.y);
            }

            if(rb.velocity.y is > -2 and < 2)
            {
                rb.velocity = new Vector2(rb.velocity.x,-6);
            }
        }
    }

        //Resets the position of the ball and his velocity
    [PunRPC]
    private void StopBall()
    {
        ballCollider.enabled = false;
        if(state.inGame)
        {
            transform.position = Vector2.zero;
            rb.velocity = Vector2.zero;
            if(PhotonNetwork.IsMasterClient)
            {
                Invoke("AddSpeed",3); //Then waits another 3 seconds before adding speed again
            }
        }
    }

    private void StopBallRPC()
    {
        view.RPC("StopBall",RpcTarget.All);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(view.IsMine)
        {
            if(other.CompareTag("Left"))
            {
                GameObject.FindObjectOfType<Score_UI>().IncreasePlayer2Score(); //If the ball scores on the left side, it will call and RPC that increases the player 2 Score
                StopBallRPC();
            }
            else if(other.CompareTag("Right"))
            {
                //If the ball scores on the right side, it will call and RPC that increases the player 1 Score
                GameObject.FindObjectOfType<Score_UI>().IncreasePlayer1Score();
                StopBallRPC();
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

    void FixedUpdate()
    {
        if(!view.IsMine)
        {
            rb.position = Vector2.MoveTowards(rb.position, networkPosition, Time.fixedDeltaTime); //Smoothly moves the ball to the network position
        }
    }

    private void OnEnable()
    {
        state.OnRestartMatch += StopBallRPC;
    }

    private void OnDisable()
    {
        state.OnRestartMatch -= StopBallRPC;
    }
}
