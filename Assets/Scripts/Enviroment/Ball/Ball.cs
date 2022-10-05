using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private PhotonView view;
    [SerializeField] private float minStartSpeed,maxStartSpeed;
    // Start is called before the first frame update
    private void Start()
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        Invoke("AddSpeed",3);
    }

    private void AddSpeed()
    {
        rb.velocity = new Vector2(Random.Range(minStartSpeed,maxStartSpeed),Random.Range(minStartSpeed,maxStartSpeed)); //Adds a random speed to the ball
    }
    

    private void StopBall()
    {
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;
        Invoke("AddSpeed",3);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Left"))
        {
            GameObject.FindObjectOfType<Score_UI>().IncreasePlayer2Score();
            StopBall();
        }
        else if(other.CompareTag("Right"))
        {
            GameObject.FindObjectOfType<Score_UI>().IncreasePlayer1Score();
            StopBall();
        }
    }
}
