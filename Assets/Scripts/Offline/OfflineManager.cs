using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class OfflineManager : MonoBehaviour
{
    private void Awake() 
    {
        PhotonNetwork.OfflineMode = true;
    }
}
