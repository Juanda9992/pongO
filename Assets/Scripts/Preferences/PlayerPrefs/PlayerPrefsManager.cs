using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public void ChangeFlipper()
    {
        if(PlayerPrefs.GetInt("Flipped",0) == 1)
        {
            PlayerPrefs.SetInt("Flipped", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Flipped",1);
        }
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
