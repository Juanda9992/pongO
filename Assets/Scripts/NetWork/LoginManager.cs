using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private GameObject errorText;
    [SerializeField] private SaveModel saveModel;
    [SerializeField] private GameObject nextPanel,currentPanel;

    private void Start() 
    {
        if(saveModel.nickName != string.Empty)
        {
            GoToNextPanel();
        }    
    }

    public void Validate()
    {
        if(nameInput.text.Length > 0)
        {
            GoToNextPanel();
            saveModel.nickName = nameInput.text;
            SaveManager.instance.Save();
        }
        else
        {
            errorText.SetActive(true);
        }
    }

    public void GoToNextPanel()
    {
        PhotonNetwork.NickName = saveModel.nickName;
        nextPanel.SetActive(true);
        currentPanel.SetActive(false);
    }
}
