using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class Rematcher : MonoBehaviour
{
    private int requiredVotes = 2;
    private int currentVotes = 0;
    [SerializeField] private TextMeshProUGUI retryButtonText;
    private Match_State state;

    void Start()
    {
        state = GameObject.FindObjectOfType<Match_State>();
        UpdateText();
    }
    
    public void FindDependences()
    {
        retryButtonText = GameObject.Find("RetryText").GetComponent<TextMeshProUGUI>();
    }
    public void IncreaseVotes()
    {
        currentVotes++;
        UpdateText();
        if(currentVotes == requiredVotes)
        {
            state.ResetMatch();
            currentVotes = 0;
        }
    }

    private void UpdateText()
    {
        retryButtonText.text = "RETRY? " + currentVotes + " / " + requiredVotes; 
    }
}
