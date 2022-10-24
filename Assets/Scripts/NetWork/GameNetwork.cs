using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameNetwork : MonoBehaviourPunCallbacks
{
    public static GameNetwork gameNetworkInstance;
    public Player_Control player1, player2;
    public GameObject playerPrefab; //Paddle to instantiate
    private Rematcher rematcher;
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

    //Allows the player to leave the match, then it proceeds to load the main menu scene;
    public void ExitMatch()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
        Debug.Log(PhotonNetwork.InRoom);
        SceneManager.LoadScene(0);
    }

    public void IncreaseRematchVotesThroughtButton()
    {
        rematcher.IncreaseVotes();
    }
}
