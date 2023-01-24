using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineStatsManager : MonoBehaviour
{
    public static OfflineStatsManager instance;
    public int matchPoints;
    public bool isHard;
    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }    
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void SetStats(int points,bool ishardDifficulty)
    {
        matchPoints = points;
        isHard = ishardDifficulty;
    }
}
