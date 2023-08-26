using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataHolder : MonoBehaviour
{
    public static SaveDataHolder instance;
    public SaveModel saveModel;

    private void Awake() 
    {
        instance = this;
        if(saveModel.colorUnlocked.Count == 0)
        {
            saveModel.colorUnlocked = new List<bool>(new bool[10]);
        }    
    }
}
