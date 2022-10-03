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
        StartCoroutine("SpawnPlayer");
    }

    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(1);
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(playerPrefab.name,new Vector3(-7,0,0),Quaternion.identity);    
        }
        else
        {
            PhotonNetwork.Instantiate(playerPrefab.name,new Vector3(7,0,0),Quaternion.identity);
        }
        
    }
}
