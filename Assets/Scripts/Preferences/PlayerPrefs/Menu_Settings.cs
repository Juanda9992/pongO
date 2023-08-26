using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu_Settings : MonoBehaviour
{
    [SerializeField] private Toggle buttonsFlipped; 
    // Start is called before the first frame update
    void Start()
    {
        buttonsFlipped.isOn = SaveDataHolder.instance.saveModel.buttonsFlipped;
    }

    public void FlipButtons()
    {
        SaveDataHolder.instance.saveModel.buttonsFlipped= buttonsFlipped.isOn;
    }
}
