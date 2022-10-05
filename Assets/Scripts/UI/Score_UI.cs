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

    void Start()
    {
        view = GetComponent<PhotonView>();
        UpdateScore();
    }
    [PunRPC]
    private void IncreasePlayer1ScoreRPC()
    {
        player1Score++;
        UpdateScore();
    }

    public void IncreasePlayer1Score()
    {
        view.RPC("IncreasePlayer1ScoreRPC",RpcTarget.Others);
    }
    public void IncreasePlayer2Score()
    {
        view.RPC("IncreasePlayer2ScoreRPC",RpcTarget.Others);
    }

    [PunRPC]
    private void IncreasePlayer2ScoreRPC()
    {
        player2Score++;
        UpdateScore();
    }


    public void UpdateScore()
    {
        Debug.Log(player2Score);
        scoreText.text = player1Score.ToString() +"    "+ "-" +"    "+player2Score.ToString();
    }
}
