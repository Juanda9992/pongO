 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    [SerializeField]private SaveModel saveModel;

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
        Load();

        StartCoroutine("SaveLoop");    
    }

    public IEnumerator SaveLoop()
    {
        while(true)
        {
            Save();
            yield return new WaitForSeconds(60);
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(saveModel);
        PlayerPrefs.SetString("json",json);
        Debug.Log(json);
    }

    public void Load()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("json"),saveModel);
    }


}
