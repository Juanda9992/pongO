using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerColor : MonoBehaviour
{
    private PhotonView view;
    private SpriteRenderer sRenderer;

    private void Awake() 
    {
        if(PhotonNetwork.OfflineMode == false)
        {
            view = GetComponent<PhotonView>();  
        }
        sRenderer = GetComponent<SpriteRenderer>();   
    }
    private void Start() 
    {
        if(view != null && !PhotonNetwork.OfflineMode)
        {
            if(view.IsMine)
            {

                Color currentColor = ColorRewarder.colorRewarderInst.colorToGive;
                sRenderer.color = currentColor;
                Vector3 color3 = new Vector3(currentColor.r,currentColor.g,currentColor.b);
                view.RPC("SetColor",RpcTarget.AllBuffered,color3);
            }
        }
        
        if(PhotonNetwork.OfflineMode)
        {
            sRenderer.color = ColorRewarder.colorRewarderInst.colorToGive;
        }

    }

    [PunRPC]
    public void SetColor(Vector3 color)
    {
        sRenderer.color = new Color(color.x,color.y,color.z,1);
    }
}
