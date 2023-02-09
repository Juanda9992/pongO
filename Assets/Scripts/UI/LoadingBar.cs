using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private GameObject loadingBar;
    [SerializeField] private Match_State state;
    [SerializeField] private float initialSize = 15;
    public void ShrinkBar()
    {
        ShakeCamera();
        DOTween.Kill(transform);
        loadingBar.transform.localScale = new Vector2(initialSize,0.3f);
        loadingBar.transform.DOScaleX(0,3); 
    }

    public void ShakeCamera()
    {
        if(state.inGame)
        {
            Camera.main.transform.DOShakePosition(0.4f,1f).OnComplete(()=> Camera.main.transform.position = new Vector3(0,0,-10));
        }
    }
}
