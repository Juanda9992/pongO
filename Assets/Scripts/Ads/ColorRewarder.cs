using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorRewarder : MonoBehaviour
{
    public static ColorRewarder colorRewarderInst;
    public Color colorToGive = Color.white;

    private void Awake()
    {
        if(colorRewarderInst == null)
        {
            colorRewarderInst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        colorToGive = Color.white;
    }

    public void UpdatePlayerColor(Button_Unlocker_Checker checker)
    {
        colorToGive = checker.GetComponent<Image>().color;
    }

    public Color GetCurrentColor()
    {
        return colorToGive;
    }

    [ContextMenu("DeletePlayerPrefs")]
    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
