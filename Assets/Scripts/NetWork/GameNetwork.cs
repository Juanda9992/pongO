using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameNetwork : MonoBehaviourPunCallbacks
{
    public static GameNetwork gameNetworkInstance;
    public Player_Control player1, player2;
    public GameObject playerPrefab; //Paddle to instantiate
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
            PhotonNetwork.Instantiate(playerPrefab.name,new Vector3(-7,0,0),Quaternion.identity);//If the user is the master client, the game will instantiate a paddle in the left side  
        }
        else
        {
            PhotonNetwork.Instantiate(playerPrefab.name,new Vector3(7,0,0),Quaternion.identity); //If the player is not the master client, it will instantiate a paddle on the right side
        }
        
    }

    public void ExitMatch()
    {
        PhotonNetwork.LoadLevel(0);
        PhotonNetwork.Disconnect();
    }
}
