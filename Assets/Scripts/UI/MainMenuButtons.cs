using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public bool testing = true;

    public void StartGame()
    {
        mainMenuPanel.gameObject.SetActive(false);
    }

    public void Options()
    {
        //put something here
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        if (!testing)
        {
            Application.Quit();
        }
    }
}
