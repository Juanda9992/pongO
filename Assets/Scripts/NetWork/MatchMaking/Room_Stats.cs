using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Room_Stats : MonoBehaviour
{
    public static Room_Stats Stats_inst;

    public int matchPoints = 3;
    // Start is called before the first frame update
    void Start()
    {
        if(Stats_inst == null)
        {
            Stats_inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
