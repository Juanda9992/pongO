using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOffline : Ball
{
    private PaddleAgent paddleAgent;
    void Start()
    {
        paddleAgent = GameObject.FindObjectOfType<PaddleAgent>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Left"))
        {
            paddleAgent.OnGameWin();
        }    
        else if(CompareTag("Right"))
        {
            paddleAgent.OnGameLosed();
        }
    }

}
