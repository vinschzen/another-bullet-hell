using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SearchService;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.VisualScripting;

public class MainMenuController : MonoBehaviour
{
    public GameObject[] arrMenus;
    public TMP_Text nameSpan;
    public TMP_Text levelSpan;
    public TMP_Text expSpan;
    public TMP_Text healthSpan;
    public TMP_Text defenseSpan;
    public TMP_Text attackSpan;
    public TMP_Text difficultySpan;
    public TMP_Text stageDifficultySpan;
    void Start()
    {
        PlayerData data;
        try
        {
            data = SaveManager.Instance.CurrentSave;
        }
        catch (System.Exception)
        {
            string path = Application.persistentDataPath + "/playerData_1.json";
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<PlayerData>(json);
        }
        nameSpan.text = data.name;
        levelSpan.text = "Level " + data.level;
        expSpan.text = "Exp: " + data.exp + "/" + data.level*100;
        attackSpan.text = "Attack : " + data.stats[0].value;
        defenseSpan.text = "Bullets : " + data.stats[1].value;
        healthSpan.text = "Health : " + data.stats[2].value;
        
        RectTransform expBarFill = GameObject.Find("Exp Bar Fill").GetComponent<RectTransform>();
        double levelPercentage = (double)(data.exp) / (data.level * 100);
        expBarFill.sizeDelta = new Vector2( (float) levelPercentage * 222.1f , expBarFill.sizeDelta.y);

        var difficultyDisplay = "";
        switch (data.difficulty)
        {
            case 0:
                difficultyDisplay = "Story";
                break;
            case 1:
                difficultyDisplay = "Normal";
                break;
            case 2:
                difficultyDisplay = "Hard";
                break;
            default:
                difficultyDisplay = "None";
                break;
        }

        difficultySpan.text = "Difficulty : " + difficultyDisplay;
        stageDifficultySpan.text = "Difficulty : " + difficultyDisplay;

        foreach (string stageName in data.progress)
        {
            GameObject stage = GameObject.Find(stageName + " Button");
            // Debug.Log($"Stage Name : {stageName}");
            if (stage) {
                stage.SetActive(true);
                stage.GetComponent<Button>().interactable = true;
            }
            else {
                Debug.Log(stageName + " not found");
            }
        }

        openMenu( arrMenus[0] );   
    }

    void Update()
    {
        
    }

    public void openMenu(GameObject menu)
    {
        foreach (var item in arrMenus)
        {
            item.SetActive(false);
        }

        menu.SetActive(true);
    }

    public void selectStage(string stageName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(stageName);
    }

    public void exit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");
    }
}
