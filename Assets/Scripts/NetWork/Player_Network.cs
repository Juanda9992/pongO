using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Network : MonoBehaviour, IPunObservable
{
    private Player_Control myPlayer; //The paddle to control
    private SpriteRenderer sRenderer;
    private PhotonView view;
    private Color currentColor, realColor;

    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    private Vector2 oldPosition, movement;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        sRenderer = GetComponent<SpriteRenderer>();
        if(view.IsMine)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                myPlayer = GameNetwork.gameNetworkInstance.player1; //If the player is the master client, it will control the player 1
            }
            else
            {
                myPlayer = GameNetwork.gameNetworkInstance.player2; //If the user is not the master client, it will control the player 2
            }
            GetColor();
        }
        GameObject.FindObjectOfType<Button_Script>().localPlayer = myPlayer; //Tell the UI button to control the asigned player
    }

    public void MovePlayerUp()
    {
        myPlayer.moveUp(); //Move the paddle up
    }
    public void MovePlayerDown()
    {
        myPlayer.moveDown(); //Move the paddle down
    }

    public void StopMovingPlayer()
    {
        myPlayer.StopMoving(); //Stops moving the paddle
    }
    public Player_Control GetLocalPlayer()
    {
        return myPlayer; //Returns the player the user is controlling
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
    {
        return;
    }
    private void GetColor()
    {
        this.currentColor = ColorRewarder.colorRewarderInst.GetCurrentColor();
        string hexColor = ColorUtility.ToHtmlStringRGB(currentColor);
        customProperties["Color"] = hexColor;
        PhotonNetwork.LocalPlayer.CustomProperties = customProperties;
        view.RPC("SetColor",RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void SetColor()
    {
        StartCoroutine("UpdateColor");
    }

    public IEnumerator UpdateColor()
    {
        yield return new WaitForSeconds(1);
        sRenderer.color = Random.ColorHSV();

    }

}
