using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class Match_State : MonoBehaviour
{
    public bool inGame = true;
    [SerializeField] private TextMeshProUGUI endMatchText;
    public void ShowEndMatchText()
    {
        endMatchText.gameObject.SetActive(true);
    }

    public void ChangeTextToWinner(bool Player1Wins = true)
    {
        if(Player1Wins)
        {
            endMatchText.text = "PLAYER 1 WINS";
        }
        else
        {
            endMatchText.text = "PLAYER 2 WINS";
        }
        inGame = false;
        ShowEndMatchText();
    }
}
