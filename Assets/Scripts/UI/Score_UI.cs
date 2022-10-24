using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Score_UI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private int player1Score = 0,player2Score = 0;
    private PhotonView view;
    private Match_State state;

    void Awake()
    {
        view = GetComponent<PhotonView>();
        state = GameObject.FindObjectOfType<Match_State>();
        UpdateScore();
    }
    [PunRPC] //Remote Procedura Call
    private void IncreasePlayer1ScoreRPC()
    {
        player1Score++; //Increases the player 1 score
        UpdateScore(); //Updates the socreboard
    }

    [PunRPC] //Remote Procedural Call
    private void IncreasePlayer2ScoreRPC()
    {
        player2Score++;//Increases the player 2 score
        UpdateScore(); //Updates the scoreboard
    }

    public void IncreasePlayer1Score()
    {
        if(state.inGame)
        {
            view.RPC("IncreasePlayer1ScoreRPC",RpcTarget.Others); //Sends the IncreasePlayer1Score to the other client      
        }
    }
    public void IncreasePlayer2Score()
    {
        if(state.inGame)
        {    
            view.RPC("IncreasePlayer2ScoreRPC",RpcTarget.Others); //Sends the increase player 1 score to the other client
        }
    }


    public void UpdateScore()
    {
        scoreText.text = player1Score.ToString() +"    "+ "-" +"    "+player2Score.ToString();
        if(state.inGame)
        {
            if(player1Score == Room_Stats.Stats_inst.matchPoints)
            {
                state.ChangeTextToWinner();
            }
            else if(player2Score == Room_Stats.Stats_inst.matchPoints)
            {
                state.ChangeTextToWinner(false);
            }
        }
    }

    private void ResetValues()
    {
        Debug.Log(state.inGame);
        player1Score = 0;
        player2Score = 0;
        UpdateScore();
    }

    private void OnEnable()
    {
        state.OnRestartMatch += ResetValues;
    }

    private void OnDisable()
    {
        state.OnRestartMatch -= ResetValues;
    }

}
