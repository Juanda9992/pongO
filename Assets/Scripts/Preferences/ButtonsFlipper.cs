using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsFlipper : MonoBehaviour
{
    public bool flipped = false;

    private void Start() 
    {
        flipped = PlayerPrefs.GetInt("Flipped",0) == 1;

        if(flipped)
        {
            transform.localScale = new Vector2(-1,1);
        }    
    }
}
