using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameNetwork : MonoBehaviourPunCallbacks
{
    public static GameNetwork gameNetworkInstance;
    public Player_Control player1, player2;
    public GameObject playerPrefab;
    private void Awake()
    {
        if(gameNetworkInstance == null)
        {
            gameNetworkInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name,Vector3.zero,Quaternion.identity);
    }
}
