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

    public GameObject scrollPanel;
    public Material font;


    void Update()
    {
        if (gameActive)
        {
            SetSpaceText();
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

    public void ShowSpaceData()
    {
        spacePopup.gameObject.SetActive(!spacePopup.gameObject.activeSelf);
        boardMenu.gameObject.SetActive(!boardMenu.gameObject.activeSelf);
        propertyDisplay.RefreshDisplay();
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
    }

    public void Buy()
    {
        gm.PurchaseProperty();
    }

    public void MortgageMenu()
    {
        mortgageMenu.gameObject.SetActive(true);
    }

    //change this
    public void Mortgage()
    {
        gm.MortgageProperty();
    }

    public void SellMenu()
    {
        sellMenu.gameObject.SetActive(true);
    }

    //change this
    public void Sell()
    {
        gm.SellProperty();
    }

    public void AddPropName(Property prop)
    {
        TextMeshProUGUI pText = scrollPanel.AddComponent<TextMeshProUGUI>();
        pText.text = "test";
        /*pText.text = prop.GetName() + ": Development Level" + prop.GetDevelopmentLevel().ToString();

        pText.fontMaterial = font;
        pText.fontSize = 36;

        if (prop.colour == Property.Colour.BLUE)
        {
            pText.color = Color.blue;
        }
        else if (prop.colour == Property.Colour.BROWN)
        {
            pText.color = new Color(114, 53, 32);
        }
        else if (prop.colour == Property.Colour.DBLUE)
        {
            pText.color = new Color(15, 15, 255);
        }
        else if (prop.colour == Property.Colour.GREEN)
        {
            pText.color = Color.green;
        }
        else if (prop.colour == Property.Colour.ORANGE)
        {
            pText.color = new Color(224, 99, 21);
        }
        else if (prop.colour == Property.Colour.PURPLE)
        {
            pText.color = Color.magenta;
        }
        else if (prop.colour == Property.Colour.RED)
        {
            pText.color = Color.red;
        }
        else if (prop.colour == Property.Colour.YELLOW)
        {
            pText.color = Color.yellow;
        }*/

    }
}
