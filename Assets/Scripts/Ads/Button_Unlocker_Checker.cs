using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Button_Unlocker_Checker: MonoBehaviour
{
    private Button adButton;
    public string colorId;
    [SerializeField] private Image adImage;
    private RewardedAdsButton rewardedAds;
    public bool isUnlocked = false;

    private void Start()
    {
       rewardedAds = GameObject.FindObjectOfType<RewardedAdsButton>();
       adButton = GetComponent<Button>(); 
       isUnlocked = PlayerPrefs.GetInt(colorId,0) == 1;

       CheckAdImage();
    }

    public void CheckButtonAndShowAd()
    {
        if(!isUnlocked)
        {
            rewardedAds.CheckForButton((Button_Unlocker_Checker) this);
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
