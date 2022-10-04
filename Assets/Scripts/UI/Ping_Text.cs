using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class Ping_Text : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pingText;
    private float ping;
    void Update()
    {
        ping = PhotonNetwork.GetPing();

        if(ping < 100)
        {
            pingText.color = Color.green;
        }
        else if(ping >100 && ping <200)
        {
            pingText.color = Color.yellow;
        }
        else if(ping >200)
        {
            pingText.color = Color.red;
        }
    }
}
