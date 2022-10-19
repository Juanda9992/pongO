using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Room_Stats : MonoBehaviour
{
    public static Room_Stats Stats_inst;
    private PhotonView view;
    public int matchPoints = 0;
    // Start is called before the first frame update
    private void Start()
    {
        if(Stats_inst == null)
        {
            Stats_inst = this;
            DontDestroyOnLoad(this.gameObject);
            view = GetComponent<PhotonView>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void UpdateMatchPointsViaRPC()
    {
        view.RPC("UpdatePoints",RpcTarget.OthersBuffered,this.matchPoints);
    }
    [PunRPC]
    private void UpdatePoints(int DesiredPoints)
    {
        this.matchPoints = DesiredPoints; 
    }

}
