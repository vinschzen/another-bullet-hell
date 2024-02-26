using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject victoryScreen;
    public GameObject defeatScreen;
    void Start()
    {
        victoryScreen.SetActive(false);
        defeatScreen.SetActive(false);
    }

    void Update()
    {
        GameObject player = GameObject.Find("Player");
        GameObject enemy = GameObject.Find("Enemy");
        if (player == null)
        {
            defeatScreen.SetActive(true);
        }

        if (enemy == null)
        {
            victoryScreen.SetActive(true);
        }
    }

    public void retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void toMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
