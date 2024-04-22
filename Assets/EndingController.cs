using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] screens;
    void Start()
    {
        
        var endingScreen = GameObject.Find("Ending Screen");
        openScreen(endingScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openScreen(GameObject screen)
    {
        foreach (var s in screens)
        {
            s.SetActive(false);
        }

        screen.SetActive(true);
    }

    public void toNewGamePlus()
    {
        var playerData = SaveManager.Instance.CurrentSave;

        playerData.newgameplus += 1;
        playerData.progress = new List<string>();
        playerData.progress.Add("Stage 1");

        SaveManager.Instance.CurrentSave = playerData;

        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/playerData_" + playerData.saveslot + ".json";
        System.IO.File.WriteAllText(path, json);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void toReturn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}
