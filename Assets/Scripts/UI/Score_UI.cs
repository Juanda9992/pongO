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
    public void IncreasePlayer1ScoreRPC()
    {
        player1Score++;
        UpdateScore();
    }

    public void IncreasePlayer1Score()
    {
        view.RPC("IncreasePlayer1ScoreRPC",RpcTarget.All);
    }
    public void IncreasePlayer2Score()
    {
        view.RPC("IncreasePlayer1ScoreRPC",RpcTarget.All);
    }

    [PunRPC]
    public void IncreasePlayer2ScoreRPC()
    {
        player2Score++;
        UpdateScore();
    }


    public void UpdateScore()
    {
        scoreText.text = player1Score.ToString() +"    "+ "-" +"    "+player2Score.ToString();
    }
}
