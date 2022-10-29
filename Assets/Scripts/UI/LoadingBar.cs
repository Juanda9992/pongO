using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private GameObject loadingBar;
    [SerializeField] private float initialSize = 15;

    private void Start() 
    {
        ShrinkBar(4);
    }

    public void ShrinkBar(float time)
    {
        loadingBar.transform.localScale = new Vector2(initialSize,transform.localScale.y);
        loadingBar.transform.DOScaleX(0,time); 
    }
}
