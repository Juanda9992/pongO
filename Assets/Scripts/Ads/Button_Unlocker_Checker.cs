using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Button_Unlocker_Checker: MonoBehaviour
{
    private Button adButton;
    [SerializeField] private Image adImage;
    public int index;
    private RewardedAdsButton rewardedAds;
    public bool isUnlocked = false;
    private ColorRewarder rewarder;

    private void Start()
    {
        rewardedAds = GameObject.FindObjectOfType<RewardedAdsButton>();
        adButton = GetComponent<Button>(); 

        if(SaveDataHolder.instance.saveModel.colorUnlocked[index])
        {
            isUnlocked = true;
        }
        CheckAdImage();
    }

    public void CheckButtonAndShowAd()
    {
        if(!isUnlocked)
        {
            rewardedAds.CheckForButton((Button_Unlocker_Checker) this);
        }
        else
        {
            ColorRewarder.colorRewarderInst.UpdatePlayerColor((Button_Unlocker_Checker) this);
        }

        CheckAdImage();
    }
    public void CheckAdImage()
    {
        if(isUnlocked)
        {
            adImage.enabled = false;
        }
    }
}
