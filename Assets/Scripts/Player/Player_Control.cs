using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Control : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float minY,maxY;
    public float speed = 10;
    // Start is called before the first frame update
    void Awake()
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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

}
