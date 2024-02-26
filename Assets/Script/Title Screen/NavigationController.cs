using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject NewMenu;
    public GameObject LoadMenu;
    public GameObject SelectMenu;

    void Start()
    {
        openMenu(MainMenu);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void openMenu(GameObject menu)
    {
        MainMenu.SetActive(false);
        NewMenu.SetActive(false);
        LoadMenu.SetActive(false);
        SelectMenu.SetActive(false);

        menu.SetActive(true);
    }


}
