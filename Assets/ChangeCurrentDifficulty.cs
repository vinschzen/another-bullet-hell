using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeCurrentDifficulty : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerData data;
    public TMP_Text difficultyDisplay;
    public TMP_Dropdown difficultyDropdown;
    void Start()
    {
        this.data = SaveManager.Instance.CurrentSave;
        difficultyDropdown.value = data.difficulty;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCurrentDifficulty(  ) {
        switch (difficultyDropdown.value)
        {
            case 0:
                this.difficultyDisplay.text = "Difficulty : Story";
                break;
            case 1:
                this.difficultyDisplay.text = "Difficulty : Normal";
                break;
            case 2:
                this.difficultyDisplay.text = "Difficulty : Hard";
                break;
            default:
                this.difficultyDisplay.text = "Difficulty :";
                break;
        }
        this.data.difficulty = difficultyDropdown.value;
        SaveManager.Instance.CurrentSave = data;

        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/playerData_" + data.saveslot + ".json";
        System.IO.File.WriteAllText(path, json);
    }
}
