using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject exitPopup;
    public GameObject propPopup;
    public bool testing = true;

    public void StartGame()
    {
        startMenuPanel.gameObject.SetActive(false);
    }

    public void Options()
    {
        //put something here
    }

    public void ExitPopup()
    {
        exitPopup.gameObject.SetActive(!exitPopup.gameObject.activeSelf);
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        if (!testing)
        {
            Application.Quit();
        }
    }

    public void ShowPlayerProps()
    {
        propPopup.gameObject.SetActive(!propPopup.gameObject.activeSelf);
    }
}
