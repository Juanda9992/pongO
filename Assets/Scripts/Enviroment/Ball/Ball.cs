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
    [SerializeField] private float minStartSpeed,maxStartSpeed;
    // Start is called before the first frame update
    private void Start()
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        Invoke("AddSpeed",3); //Waits 3 seconds before adding speed to the ball
        state = GameObject.FindObjectOfType<Match_State>();
    }

    private void AddSpeed()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            rb.velocity = new Vector2(Random.Range(minStartSpeed,maxStartSpeed) * Mathf.RoundToInt(Random.Range(-1,1)),Random.Range(minStartSpeed /2,maxStartSpeed/2) * Mathf.RoundToInt(Random.Range(-1,1))); //Adds a random speed to the ball

            //If the velocity x or y of the ball is equal to zero, it will changed to 6
            if(rb.velocity.x == 0)
            {
                rb.velocity = new Vector2(6,rb.velocity.y); 
            }
            else if(rb.velocity.y == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x,6);
            }
        }


    }

    private void StopBall()
    {//Resets the position of the ball and his velocity
        if(state.inGame)
        {
            transform.position = Vector2.zero;
            rb.velocity = Vector2.zero;
            Invoke("AddSpeed",3); //Then waits another 3 seconds before adding speed again
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Left"))
        {
            GameObject.FindObjectOfType<Score_UI>().IncreasePlayer2Score(); //If the ball scores on the left side, it will call and RPC that increases the player 2 Score
            StopBall();
        }
        else if(other.CompareTag("Right"))
        {
            //If the ball scores on the right side, it will call and RPC that increases the player 1 Score
            GameObject.FindObjectOfType<Score_UI>().IncreasePlayer1Score();
            StopBall();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(rb.velocity);
        }
        else if(stream.IsReading)
        {
            float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.SentServerTime));
            networkPosition = (Vector3)stream.ReceiveNext();
            rb.velocity = (Vector2)stream.ReceiveNext();

            networkPosition += (this.rb.velocity * lag);
        }
    }

    void FixedUpdate()
    {
        if(!view.IsMine)
        {
            rb.position = Vector2.MoveTowards(rb.position, networkPosition, Time.fixedDeltaTime);
        }
    }
}
