using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))] //Required Component
public class Ball : MonoBehaviour, IPunObservable
{
    [HideInInspector] public Rigidbody2D rb;
    private PhotonView view;
    private Match_State state;
    private Vector2 networkPosition;
    private LoadingBar bar;
    private float xTime;
    private bool ballInited = false;
    [SerializeField] private float minStartSpeed,maxStartSpeed;
    // Start is called before the first frame update
    private void Start()
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        state = GameObject.FindObjectOfType<Match_State>();
        bar = GameObject.FindObjectOfType<LoadingBar>();
        StopBall();

    }

    private void AddSpeed()
    {
        Debug.Log("The ball is entering here");
        ballInited = true;
        if(PhotonNetwork.IsMasterClient)
        { 
            rb.velocity = Random.insideUnitCircle.normalized * Random.Range(6,8);
            if(Mathf.Abs(rb.velocity.x) < 1.5f)
            {
                rb.velocity = new Vector2(6,rb.velocity.y);
            }
            else if(Mathf.Abs(rb.velocity.y) <1.5f)
            {
                rb.velocity = new Vector2(rb.velocity.x,6);
            }
        }
    }

        //Resets the position of the ball and his velocity
    [PunRPC]
    private void StopBall()
    {
        xTime = 0;
        bar.ShrinkBar();
        ballInited = false;
        if(state.inGame)
        {
            transform.localPosition = Vector2.zero;
            rb.velocity = Vector2.zero;
            if(PhotonNetwork.IsMasterClient)
            {
                Invoke("AddSpeed",3); //Then waits another 3 seconds before adding speed again
            }
        }
    }

    private void StopBallRPC()
    {
        if(!PhotonNetwork.OfflineMode)
        {
            view.RPC("StopBall",RpcTarget.All);
        }
        else
        {
            StopBall();
        }
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
            stream.SendNext(transform.localPosition); //Send the position of the ball
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
        if(transform.localPosition.x < -13 || transform.localPosition.x > 13 || transform.localPosition.y > 15 || transform.localPosition.y < -15)
        {
            StopBall();
        }
        if(!view.IsMine)
        {
            rb.position = Vector2.MoveTowards(rb.position, networkPosition, Time.fixedDeltaTime); //Smoothly moves the ball to the network position
        }
    }

    private void Update() 
    {
        if(ballInited)
        {
            if(Mathf.Abs(rb.velocity.x) < 0.3f)
            {
                xTime+= Time.deltaTime;

                if(xTime >= 3)
                {
                    xTime = 0;
                    StopBall();
                }
            }
            if(Mathf.Abs(rb.velocity.y) < 0.3f)
            {
                xTime+= Time.deltaTime;

                if(xTime >= 3)
                {
                    xTime = 0;
                    StopBall();
                }
            }

        }

    }

    private void OnCollisionEnter2D() 
    {
        if(rb.velocity.magnitude < 13)
        {
            rb.velocity *= 1.003f;
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
