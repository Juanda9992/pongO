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
    public void IncreasePlayer1Score()
    {
        player1Score++;
        UpdateScore();
    }

    [PunRPC]
    public void IncreasePlayer2Score()
    {
        player2Score++;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = player1Score.ToString() +"    "+ "-" +"    "+player2Score.ToString();
    }
}
