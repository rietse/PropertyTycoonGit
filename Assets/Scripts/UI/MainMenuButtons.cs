using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject exitPopup;
    public GameObject propPopup;
    public GameObject debugMenu;
    public bool testing = true;

    public GameManager gm;
    public Board board;
    public TextMeshProUGUI spaceText;

    void Update()
    {
        SetSpaceText();
    }

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

    public void ShowDebugMenu()
    {
        debugMenu.gameObject.SetActive(!debugMenu.gameObject.activeSelf);
    }

    public void SetSpaceText()
    {
        int curPos = gm.playerList[gm.currentPlayer-1].currentPos;
        Space curspace = board.spaces[curPos].GetComponent<Space>();
        spaceText.text = "Current Position: " + curspace.GetName();
    }
}
