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
        ping = PhotonNetwork.GetPing(); //The ping between the client and the server

        if(ping < 100)
        {
            pingText.color = Color.green; //If the ping is less than 100, the color will be green
        }
        else if(ping is >100 and <200)
        {
            pingText.color = Color.yellow; //If the ping is between 100 and 200, the color will be yellow
        }
        else if(ping >200)
        {
            pingText.color = Color.red; //If the ping is greater than 200, the color will be red
        }
    }
}
