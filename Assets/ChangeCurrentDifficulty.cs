using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ChangeCurrentDifficulty : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerData data;
    public TMP_Text difficultyDisplay;
    public TMP_Text stageDifficultyDisplay;
    public TMP_Dropdown difficultyDropdown;
    void Start()
    {
        try
        {
            this.data = SaveManager.Instance.CurrentSave;
        }
        catch (System.Exception)
        {
            string path = Application.persistentDataPath + "/playerData_1.json";
            string json = File.ReadAllText(path);
            this.data = JsonUtility.FromJson<PlayerData>(json);
        }

        difficultyDropdown.value = this.data.difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCurrentDifficulty(  ) {
        string display = "";
        switch (difficultyDropdown.value)
        {
            case 0:
                display = "Difficulty : Story";
                break;
            case 1:
                display = "Difficulty : Normal";
                break;
            case 2:
                display = "Difficulty : Hard";
                break;
            default:
                display = "Difficulty :";
                break;
        }

        this.difficultyDisplay.text = display;
        this.stageDifficultyDisplay.text = display;

        this.data.difficulty = difficultyDropdown.value;

        SaveManager.Instance.CurrentSave = data;

        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/playerData_" + data.saveslot + ".json";
        System.IO.File.WriteAllText(path, json);
    }
}
