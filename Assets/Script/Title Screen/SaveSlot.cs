using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;

public class SaveSlot : MonoBehaviour
{
    public TMP_Text Username;
    public TMP_Text Stage;
    public TMP_Text Playtime;
    public TMP_Text Difficulty;
    public TMP_Text NewGamePlus;
    public TMP_Text EmptyPrompt;

    public int slot;

    void Start()
    {
        Username = transform.Find("Username").GetComponent<TMP_Text>();
        Stage = transform.Find("Stage").GetComponent<TMP_Text>();
        Playtime = transform.Find("Playtime").GetComponent<TMP_Text>();
        Difficulty = transform.Find("Difficulty").GetComponent<TMP_Text>();
        NewGamePlus = transform.Find("NG+").GetComponent<TMP_Text>();
        EmptyPrompt = transform.Find("Empty").GetComponent<TMP_Text>();

        LoadSaveData();
    }

    public void LoadSaveData()
    {
        string path = Application.persistentDataPath + "/playerData_" + slot + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Username.text = data.name;  
            Stage.text = data.progress[data.progress.Count-1].ToString();
            
            if (data.newgameplus > 0)
            {
                NewGamePlus.text = "NG+" + data.newgameplus;
            }
            else {
                NewGamePlus.text = "";
            }

            TimeSpan time = TimeSpan.FromSeconds(data.playtime);
            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", 
                time.Hours, 
                time.Minutes, 
                time.Seconds);
            Playtime.text = formattedTime;
            switch (data.difficulty)
            {
                case 0:
                    this.Difficulty.text = "Story";
                    break;
                case 1:
                    this.Difficulty.text = "Normal";
                    break;
                case 2:
                    this.Difficulty.text = "Hard";
                    break;
                default:
                    this.Difficulty.text = "None";
                    break;
            }
            EmptyPrompt.gameObject.SetActive(false);
            
            
        }
        else
        {
            Username.gameObject.SetActive(false);
            Stage.gameObject.SetActive(false);
            Playtime.gameObject.SetActive(false);
            Difficulty.gameObject.SetActive(false);
            NewGamePlus.gameObject.SetActive(false);
            EmptyPrompt.gameObject.SetActive(true);
        }
        
    }
}
