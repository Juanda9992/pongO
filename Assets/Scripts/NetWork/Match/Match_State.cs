using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System;
public class Match_State : MonoBehaviour
{
    public bool inGame = true;
    [SerializeField] private TextMeshProUGUI endMatchText;

    [SerializeField] private GameObject endMatchPanel;

    public delegate void onRestartMatch();
    public event onRestartMatch OnRestartMatch; 
    public void ShowEndMatchPanel()
    {
        endMatchPanel.SetActive(true);
    }

    public void ChangeTextToWinner(bool Player1Wins = true)
    {
        inGame = false;
        if(Player1Wins)
        {
            endMatchText.text = "PLAYER 1 WINS";
        }
        else
        {
            endMatchText.text = "PLAYER 2 WINS";
        }
        ShowEndMatchPanel();
    }

    public void ResetMatch()
    {
        endMatchPanel.SetActive(false);
        inGame = true;
        OnRestartMatch?.Invoke();
    }
}
