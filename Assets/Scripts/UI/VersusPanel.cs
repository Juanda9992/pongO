using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using DG.Tweening;
public class VersusPanel : MonoBehaviour
{
    [SerializeField] private RectTransform p1,p2,vs;
    [SerializeField] private TextMeshProUGUI player1;
    [SerializeField] private TextMeshProUGUI player2;

    private void Start() 
    {
        p1.anchoredPosition = new Vector2(-400,p1.anchoredPosition.y);
        p2.anchoredPosition = new Vector2(400,p2.anchoredPosition.y);
        vs.DOScale(0,0);    
    }
    private void OnEnable() 
    {

        p1.DOAnchorPosX(80,0.5f);
        p2.DOAnchorPosX(-80,0.5f);
        vs.DOScale(1,0.5f).SetDelay(0.5f);
        player1.text = PhotonNetwork.NickName;
        player2.text = PhotonNetwork.PlayerListOthers[0].NickName;
    }
}
