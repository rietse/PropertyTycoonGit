using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager: MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject setupMenuPanel;
    public GameObject exitPopup;
    public GameObject propPopup;
    public GameObject spacePopup;
    public GameObject debugMenu;
    public GameObject mortgageMenu;
    public GameObject sellMenu;
    public GameObject buyMenu;
    public GameObject boardMenu;
    public PropertyDisplay propertyDisplay;
    public bool testing = true;
    private bool gameActive;

    public GameManager gm;
    public Board board;
    public TextMeshProUGUI spaceText;

    public TextMeshProUGUI propText;
    public TextMeshProUGUI buyMenuText;


    void Update()
    {
        if (gameActive)
        {
            SetSpaceText();
        }
        if (propText.isActiveAndEnabled)
        {
            UpdatePropDisplay();
        }
    }

    public void StartGame()
    {
        if (gm.noOfPlayers > 0)
        {
            startMenuPanel.gameObject.SetActive(false);
            gm.InitialiseGame();
            gameActive = true;
        }  
    }

    public void GameSetup()
    {
        setupMenuPanel.gameObject.SetActive(true);
    }

    public void Options()
    {
        //put something here
    }

    public void ExitPopup()
    {
        GameObject[] popups = GameObject.FindGameObjectsWithTag("Popup");
        foreach (GameObject o in popups)
        {
            o.gameObject.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        if (!testing)
        {
            Application.Quit();
        }
    }

    public void ShowSpaceData()
    {
        spacePopup.gameObject.SetActive(!spacePopup.gameObject.activeSelf);
        boardMenu.gameObject.SetActive(spacePopup.gameObject.activeSelf);
    }

    public void ShowPropData()
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

    public void Move()
    {
        gm.RollDice();
        gm.MovePlayer();
    }

    public void EndTurn()
    {
        gm.NextPlayer();
    }

    public void BuyMenu()
    {
        buyMenu.gameObject.SetActive(true);
        UpdateBuyMenu();
    }

    public void Buy()
    {
        gm.PurchaseProperty();
        buyMenu.SetActive(false);
    }

    public void MortgageMenu()
    {
        mortgageMenu.gameObject.SetActive(true);
        boardMenu.gameObject.SetActive(true);
    }

    public void Mortgage()
    {
        gm.MortgageProperty();
        mortgageMenu.SetActive(false);
        boardMenu.gameObject.SetActive(false);
    }

    public void SellMenu()
    {
        sellMenu.gameObject.SetActive(true);
        boardMenu.gameObject.SetActive(true);
    }

    //change this
    public void Sell()
    {
        gm.SellProperty();
        sellMenu.SetActive(false);
        boardMenu.gameObject.SetActive(false);
    }

    private void UpdatePropDisplay()
    {
        string propString = "";
        for (int i = 0; i < 40; i++)
        {
            if(board.spaceStates[i] == gm.currentPlayer)
            {
                if(board.spaces[i].GetComponent<Space>().type == Space.Type.PROP)
                {
                    propString += board.spaces[i].GetComponent<Space>().GetName() + ": Development Level " + board.spaces[i].GetComponent<Property>().GetDevelopmentLevel().ToString() + "\n";
                }
                else
                {
                    propString += board.spaces[i].GetComponent<Space>().GetName() + "\n";
                }
            }
        }
        propText.text = propString;
    }

    private void UpdateBuyMenu()
    {
        string buyString = "";

        if (board.spaces[gm.playerList[gm.currentPlayer - 1].currentPos].GetComponent<Space>().type == Space.Type.PROP)
        {
            buyString = "Are you sure you want to buy " + board.spaces[gm.playerList[gm.currentPlayer - 1].currentPos].GetComponent<Space>().GetName() + "for £" + board.spaces[gm.playerList[gm.currentPlayer - 1].currentPos].GetComponent<Property>().GetPrice().ToString() + "?";
        }
        else if (board.spaces[gm.playerList[gm.currentPlayer - 1].currentPos].GetComponent<Space>().type == Space.Type.STAT)
        {
            buyString = "Are you sure you want to buy " + board.spaces[gm.playerList[gm.currentPlayer - 1].currentPos].GetComponent<Space>().GetName() + "for £" + board.spaces[gm.playerList[gm.currentPlayer - 1].currentPos].GetComponent<Station>().GetPrice().ToString() + "?";
        }
        else if(board.spaces[gm.playerList[gm.currentPlayer - 1].currentPos].GetComponent<Space>().type == Space.Type.UTIL)
        {
            buyString = "Are you sure you want to buy " + board.spaces[gm.playerList[gm.currentPlayer - 1].currentPos].GetComponent<Space>().GetName() + "for £" + board.spaces[gm.playerList[gm.currentPlayer - 1].currentPos].GetComponent<Utility>().GetPrice().ToString() + "?";
        }
        else
        {
            buyString = "I'm not quite sure how we got here but something's very wrong";
        }

        buyMenuText.text = buyString;
    }


}
