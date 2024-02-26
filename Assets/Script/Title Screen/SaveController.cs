using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
public class SaveController : MonoBehaviour
{    
    public TMP_InputField playerNameInput;
    public TMP_Dropdown difficultyDropdown;

    public TMP_Text usernameDisplay;
    public TMP_Text difficultyDisplay;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void updateDisplay()
    {
        this.usernameDisplay.text = playerNameInput.text;
        switch (difficultyDropdown.value)
        {
            case 0:
                this.difficultyDisplay.text = "Story";
                break;
            case 1:
                this.difficultyDisplay.text = "Normal";
                break;
            case 2:
                this.difficultyDisplay.text = "Hard";
                break;
            default:
                this.difficultyDisplay.text = "None";
                break;
        }
    }

    public void newSaveFile(int saveslot)
    {
        string playerName = playerNameInput.text;
        int difficulty = difficultyDropdown.value;
        

        PlayerData playerData = new PlayerData(playerName, difficulty, saveslot);
        string json = JsonUtility.ToJson(playerData);

        string path = Application.persistentDataPath + "/playerData_" + saveslot + ".json";
        System.IO.File.WriteAllText(path, json);
    }

    public void selectSaveFile(int slot)
    {
        string path = Application.persistentDataPath + "/playerData_" + slot + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            SaveManager.Instance.CurrentSave = data;

            SceneManager.LoadScene("MainMenu");
        }
        else {
            Debug.Log("save fille " + slot + " not found");
        }

    }
}
