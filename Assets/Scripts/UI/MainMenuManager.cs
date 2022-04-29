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
    public GameObject customMenu;
    public PropertyDisplay propertyDisplay;
    private bool gameActive;

    public GameManager gm;
    public Board board;
    public BoardSpaceCustomisation BoardSpaceCustomisation;
    public TextMeshProUGUI spaceText;

    public TextMeshProUGUI propText;
    public TextMeshProUGUI buyMenuText;

    //Updates UI text indicating current space and current owned properties
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

    //Button function to start game
    public void StartGame()
    {
        if (gm.noOfPlayers > 0)
        {
            startMenuPanel.gameObject.SetActive(false);
            gm.InitialiseGame();
            gameActive = true;
        }  
    }

    //Button function to activate game setup menu
    public void GameSetup()
    {
        setupMenuPanel.gameObject.SetActive(true);
    }

    //Shifts board customisation UI
    public void ShiftLeft()
    {
        BoardSpaceCustomisation.ShiftLeft();
    }

    //Shifts board customisation UI
    public void ShiftRight()
    {
        BoardSpaceCustomisation.ShiftRight();
    }

    //Saves Customisation input text
    public void SaveCustomSpace()
    {
        BoardSpaceCustomisation.SaveText();
    }

    //Resets customisation text
    public void ResetCustomSpace()
    {
        BoardSpaceCustomisation.ResetText();
    }

    //Button function reactivates customisation menu
    public void CloseCustomMenu()
    {
        customMenu.gameObject.SetActive(false);
    }

    //UNFINISHED
    public void Options()
    {
        //put something here
    }

    //Button function closes all open popups
    public void ExitPopup()
    {
        GameObject[] popups = GameObject.FindGameObjectsWithTag("Popup");
        foreach (GameObject o in popups)
        {
            o.gameObject.SetActive(false);
        }
    }

    //Button function opens exit popup menu
    public void OpenExitPopup()
    {
        exitPopup.gameObject.SetActive(true);
    }

    //Button function quits game
    public void ExitGame()
    {
        Application.Quit();
    }

    //Button function opens customisation menu
    public void Customise()
    {
        customMenu.gameObject.SetActive(!customMenu.gameObject.activeSelf);
    }

    //Button function opens space data menu
    public void ShowSpaceData()
    {
        spacePopup.gameObject.SetActive(!spacePopup.gameObject.activeSelf);
        boardMenu.gameObject.SetActive(spacePopup.gameObject.activeSelf);
    }

    //Button function activates owned property data panel
    public void ShowPropData()
    {
        propPopup.gameObject.SetActive(!propPopup.gameObject.activeSelf);
    }

    //Toggles debug menu
    public void ShowDebugMenu()
    {
        debugMenu.gameObject.SetActive(!debugMenu.gameObject.activeSelf);
    }

    //Updates UI text displaying current position
    public void SetSpaceText()
    {
        int curPos = gm.playerList[gm.currentPlayer-1].currentPos;
        Space curspace = board.spaces[curPos].GetComponent<Space>();
        spaceText.text = "Current Position: " + curspace.GetName();
    }

    //Button function to move player
    public void Move()
    {
        gm.RollDice();
        gm.MovePlayer();
    }

    //Button function to end turn
    public void EndTurn()
    {
        gm.NextPlayer();
    }

    //Button function to open buy menu
    public void BuyMenu()
    {
        buyMenu.gameObject.SetActive(true);
        UpdateBuyMenu();
    }

    //Button function to buy property
    public void Buy()
    {
        gm.PurchaseProperty();
        buyMenu.SetActive(false);
    }

    //Button function to open mortage menu
    public void MortgageMenu()
    {
        mortgageMenu.gameObject.SetActive(true);
        boardMenu.gameObject.SetActive(true);
    }

    //Button function to mortgage property
    public void Mortgage()
    {
        gm.MortgageProperty();
        mortgageMenu.SetActive(false);
        boardMenu.gameObject.SetActive(false);
    }

    //Button function to open sell menu
    public void SellMenu()
    {
        sellMenu.gameObject.SetActive(true);
        boardMenu.gameObject.SetActive(true);
    }

    //Button function to sell property
    public void Sell()
    {
        gm.SellProperty();
        sellMenu.SetActive(false);
        boardMenu.gameObject.SetActive(false);
    }

    //Updates UI text displaying owned properties
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

    //Updates UI text on buy menu
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

    //Triggers card popup
    public void TriggerCard()
    {
        board.TriggerLatestCard(false);
    }

    //Opens new card popup
    public void DrawNewCard()
    {
        if (board.CheckDrawNew() == true)
        {
            ExitPopup();
            board.TriggerLatestCard(true);
        }
        else
            print("Can't draw a new card!");
    }

    public void PayJailFine()
    {
        ExitPopup();
        gm.JailFine();
    }

    public void UseJailCard()
    {
        if (gm.GetCurrentPlayerFreeJailCards() > 0)
        {
            ExitPopup();
            gm.UseJailCard();
        }
        else
        {
            print("Player doesn't have a jail card!");
        }
    }

    public void SwitchCamera()
    {
        gm.GetComponent<CameraController>().SwitchCameraView();
    }

    public void UpgradeProp()
    {
        gm.UpgradeProperty();
    }

    public void DegradeProp()
    {
        gm.DegradeProperty();
    }
}
