using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class VersusPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1;
    [SerializeField] private TextMeshProUGUI player2;

    private void OnEnable() 
    {
        player1.text = PhotonNetwork.NickName;
        player2.text = PhotonNetwork.PlayerListOthers[0].NickName;
    }
}
