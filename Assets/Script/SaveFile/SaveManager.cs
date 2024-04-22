using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public PlayerData CurrentSave { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public PlayerData getCurrentSave() 
    {
        PlayerData data;
        // try
        // {
        //     data = SaveManager.Instance.CurrentSave;
        // }
        // catch (System.Exception)
        // {
        //     string path = Application.persistentDataPath + "/playerData_1.json";
        //     string json = File.ReadAllText(path);
        //     data = JsonUtility.FromJson<PlayerData>(json);
        // }
        return SaveManager.Instance.CurrentSave;
    }
}

