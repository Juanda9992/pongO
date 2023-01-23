using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class OfflineRoomCreator : MonoBehaviour
{
    [SerializeField] private Increase_Placeholder placeholder;
    [SerializeField] private bool isHard;
    [SerializeField] private string SceneName;
    [SerializeField] private TextMeshProUGUI warningText;
    public void UpdateDifficulty(bool isChangingToHard)
    {
        isHard = isChangingToHard;
    }

    public void CreateRoom()
    {
        if(placeholder.textValue > 0)
        {
            OfflineStatsManager.instance.SetStats(placeholder.textValue,isHard);
            SceneManager.LoadScene(SceneName);
        }
        else
        {
            warningText.gameObject.SetActive(true);
        }

    }
}
