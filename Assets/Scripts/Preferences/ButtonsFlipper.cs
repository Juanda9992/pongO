using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsFlipper : MonoBehaviour
{
    public bool flipped = false;

    private void Start() 
    {
        flipped = SaveDataHolder.instance.saveModel.buttonsFlipped;

        if(flipped)
        {
            transform.localScale = new Vector2(-1,1);
        }    
    }
}
