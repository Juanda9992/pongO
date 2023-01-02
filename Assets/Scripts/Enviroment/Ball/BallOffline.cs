using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOffline : Ball
{
    [SerializeField] private PaddleAgent player1,player2;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Left"))
        {
            player1.OnGameLosed();
            player2.OnGameWin();
        }    
        else if(CompareTag("Right"))
        {
            player2.OnGameLosed();
            player1.OnGameWin();
        }
    }


}
