using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
public class Rematcher : MonoBehaviourPunCallbacks
{
    private int requiredVotes = 1;
    private int currentVotes = 0;
    [SerializeField] private TextMeshProUGUI retryButtonText;
    [SerializeField] private Button rematchButton;
    private PhotonView view;
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
            UpdateText();
            rematchButton.interactable = true;
        }
    }

    public void IncreaseVotesRPC()
    {
        view.RPC("IncreaseVotes",RpcTarget.All);
    }

    private void UpdateText()
    {
        if(!PhotonNetwork.OfflineMode)
        {
            retryButtonText.text = "RETRY? " + currentVotes + " / " + requiredVotes; 
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        rematchButton.gameObject.SetActive(false);
    }
}
