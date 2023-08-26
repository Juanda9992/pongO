using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorRewarder : MonoBehaviour
{
    public static ColorRewarder colorRewarderInst;
    public Color colorToGive = Color.white;
    [SerializeField] private SaveModel saveModel;

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
    private void Start() 
    {
        colorToGive = new Color(saveModel.color.x,saveModel.color.y,saveModel.color.z,1);
    }

    public void UpdatePlayerColor(Button_Unlocker_Checker checker)
    {
        colorToGive = checker.GetComponent<Image>().color;
        saveModel.color = new Vector3(colorToGive.r,colorToGive.g,colorToGive.b);
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
