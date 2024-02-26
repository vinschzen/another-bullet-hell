using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public GameObject[] arrMenus;
    public TMP_Text nameSpan;
    public TMP_Text levelSpan;
    public TMP_Text healthSpan;
    public TMP_Text defenseSpan;
    public TMP_Text attackSpan;
    public TMP_Text difficultySpan;
    public TMP_Text stageDifficultySpan;
    void Start()
    {
        // nameSpan = transform.Find("Name Span").GetComponent<TMP_Text>();
        // levelSpan = transform.Find("Level Span").GetComponent<TMP_Text>();
        // healthSpan = transform.Find("Health Span").GetComponent<TMP_Text>();
        // defenseSpan = transform.Find("Defense Span").GetComponent<TMP_Text>();
        // attackSpan = transform.Find("Attack Span").GetComponent<TMP_Text>();
        // difficultySpan = transform.Find("Difficulty Span").GetComponent<TMP_Text>();
        // stageDifficultySpan = transform.Find("Stage Difficulty Span").GetComponent<TMP_Text>();

        PlayerData data = SaveManager.Instance.CurrentSave;

        nameSpan.text = data.name;
        levelSpan.text = "Level " + data.level;
        attackSpan.text = "Attack : " + data.stats[0].value;
        defenseSpan.text = "Defense : " + data.stats[1].value;
        healthSpan.text = "Health : " + data.stats[2].value;


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

    public void selectStage(int stage)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void exit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");
    }
}
