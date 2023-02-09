using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Animator_Manager : MonoBehaviour
{
    public float timeToAnimate;
    public float scalerSize;
    public static Animator_Manager instance;

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
    public void MakeButtonBigger(Transform objTransform)
    {
        objTransform.DOScale(scalerSize,timeToAnimate);
    }
    public void MakeButtonNormal(Transform objTransform)
    {
        objTransform.DOScale(1,timeToAnimate);
    }
}
