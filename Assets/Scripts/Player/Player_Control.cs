using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Control : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float minY,maxY;
    [SerializeField] private float speed = 10;
    // Start is called before the first frame update
    void Awake()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            GameNetwork.gameNetworkInstance.player1 = this.GetComponent<Player_Control>();
        }
        else
        {
            GameNetwork.gameNetworkInstance.player2 = this.GetComponent<Player_Control>();
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
            rb.velocity = transform.up * speed;
        }
    }

    public void moveDown()
    {
        if(transform.position.y > minY)
        {
            rb.velocity = -transform.up * speed;
        }
    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x,Mathf.Clamp(transform.position.y,minY,maxY));
    }

}
