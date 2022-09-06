using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameNetwork : MonoBehaviourPunCallbacks
{
    public static GameNetwork gameNetworkInstance;
    void Awake()
    {
        if(gameNetworkInstance == null)
        {
            gameNetworkInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
        
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Has entered the room {0}",newPlayer);
    }

}
