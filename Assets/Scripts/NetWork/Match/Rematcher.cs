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
    PhotonView view;
    private Match_State state;

    void Start()
    {
        state = GameObject.FindObjectOfType<Match_State>();
        view = GetComponent<PhotonView>();
        UpdateText();
    }

    [PunRPC]
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

    public void IncreaseVotesRPC()
    {
        view.RPC("IncreaseVotes",RpcTarget.All);
    }

    private void UpdateText()
    {
        retryButtonText.text = "RETRY? " + currentVotes + " / " + requiredVotes; 
    }
}
