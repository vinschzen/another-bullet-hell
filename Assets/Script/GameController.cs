using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public string nextStageUnlock;
    private GameObject victoryScreen;
    private GameObject defeatScreen;

    private PlayerData playerData;
    private int damageTaken;
    private int damageDealt;
    private int enemyKilled;
    
    private int[] incrementStats;
    private int allocatables;

    private bool hasWonOnce = false;
    private float startTime;
    private float elapsedTime;

    void Start()
    {
        victoryScreen = GameObject.Find("Victory Screen");
        defeatScreen = GameObject.Find("Defeat Screen");
        this.playerData = SaveManager.Instance.CurrentSave;
        startTime = Time.realtimeSinceStartup;

        damageTaken = 0;
        damageDealt = 0;
        enemyKilled = 0;
        incrementStats = new int[3] { 0, 0, 0 };
        allocatables = 0;
        victoryScreen.SetActive(false);
        defeatScreen.SetActive(false);
    }

    void Update()
    {
        elapsedTime = Time.realtimeSinceStartup - startTime;

        GameObject player = GameObject.Find("Player");
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");

        if (player == null)
        {
            defeatScreen.SetActive(true);
        }

        if (enemy.Length == 0)
        {
            victoryScreen.SetActive(true);

            if (!hasWonOnce) {
                victory();
                hasWonOnce = true;
            }
        }
    }

    void victory()
    {

        int expGained = damageDealt * 10;
        int points = 0;
        playerData.exp += expGained;
        playerData.playtime += elapsedTime;
        playerData.progress.Add(nextStageUnlock);

        while (playerData.exp >= playerData.level * 100 )
        {
            playerData.exp -= playerData.level * 100;
            playerData.level++;
            points++;
        }
        this.allocatables = points;

        GameObject.Find("Increment Buttons").SetActive( points > 0);
        TextMeshProUGUI resultsText = GameObject.Find("Results Text (TMP)").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI statsIntegerText = GameObject.Find("Stats Integer Text (TMP)").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI characterLevelText = GameObject.Find("Character Level Text (TMP)").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI characterExpText = GameObject.Find("Character Exp Text (TMP)").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI availablePointsText = GameObject.Find("Available Points Text (TMP)").GetComponent<TextMeshProUGUI>();
        RectTransform expBarFill = GameObject.Find("Exp Bar Fill").GetComponent<RectTransform>();
        resultsText.text = $"Enemy Defeated : {enemyKilled}\nDamage Taken : {damageTaken}\nDamage Dealt : {damageDealt}";
        characterLevelText.text = $"Level {playerData.level}";
        characterExpText.text = $"Exp {playerData.exp} / {playerData.level * 100} (+{expGained})";
        availablePointsText.text = $"Available Points : {this.allocatables}";
        double levelPercentage = (double)(playerData.exp) / (playerData.level * 100);
        expBarFill.sizeDelta = new Vector2( (float) levelPercentage * 222.1f , expBarFill.sizeDelta.y);
        statsIntegerText.text = $"{playerData.stats[0].value}\n{playerData.stats[1].value}\n{playerData.stats[2].value}";
    }

    public void retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void toMainMenu()
    {
        SaveManager.Instance.CurrentSave = this.playerData;

        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/playerData_" + playerData.saveslot + ".json";
        System.IO.File.WriteAllText(path, json);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void incrementDamageTaken(int amount) {
        this.damageTaken += amount;
    }

    public void incrementDamageDealt(int amount) {
        this.damageDealt += amount;
    }

    public void incrementEnemyKilled(int amount) {
        this.enemyKilled += amount;
    }

    public void allocateStat(int stat) {
        if (allocatables > 0) {
            incrementStats[stat]++;
            allocatables--;
            playerData.stats[stat] = new PlayerData.Stat { name = playerData.stats[stat].name, value = playerData.stats[stat].value + 1 };

            TextMeshProUGUI availablePointsText = GameObject.Find("Available Points Text (TMP)").GetComponent<TextMeshProUGUI>();
            availablePointsText.text = $"Available Points : {this.allocatables}";

            TextMeshProUGUI statsIntegerText = GameObject.Find("Stats Integer Text (TMP)").GetComponent<TextMeshProUGUI>();
            statsIntegerText.text = $"{playerData.stats[0].value}\n{playerData.stats[1].value}\n{playerData.stats[2].value}";
        }

    }

    public void deallocateStat(int stat) {
        if (incrementStats[stat] > 0) {
            incrementStats[stat]--;
            allocatables++;
            playerData.stats[stat] = new PlayerData.Stat { name = playerData.stats[stat].name, value = playerData.stats[stat].value - 1 };

            TextMeshProUGUI availablePointsText = GameObject.Find("Available Points Text (TMP)").GetComponent<TextMeshProUGUI>();
            availablePointsText.text = $"Available Points : {this.allocatables}";

            TextMeshProUGUI statsIntegerText = GameObject.Find("Stats Integer Text (TMP)").GetComponent<TextMeshProUGUI>();
            statsIntegerText.text = $"{playerData.stats[0].value}\n{playerData.stats[1].value}\n{playerData.stats[2].value}";
        }
    }

}
