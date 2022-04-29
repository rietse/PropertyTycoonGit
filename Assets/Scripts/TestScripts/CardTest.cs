using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardTest : MonoBehaviour
{
    public BoardTESTONLY board;
    public TextMeshProUGUI currentPlayerNo;
    public TextMeshProUGUI playerMoney;
    public TextMeshProUGUI OppCardList;
    public TextMeshProUGUI PotLuckList;
    public GameManagerTest gsm;
    public PlayerControllerTESTONLY player;
    public string oppList;
    public string potList;

    public void Dice()
    {
        gsm.GetComponent<GameManagerTest>().RollDice();
        currentPlayerText();
    }
    public void Move()
    {
        gsm.GetComponent<GameManagerTest>().MovePlayer();
    }

    void currentPlayerText()
    {
        currentPlayerNo.SetText("Current Player = " + gsm.GetCurrentPlayer().ToString());
    }
    public void EndTurn()
    {
        gsm.GetComponent<GameManagerTest>().NextPlayer();
        currentPlayerText();
    }

    void PlayerMoneyText()
    {
        playerMoney.SetText("Player " + gsm.GetCurrentPlayer().ToString() + ": " + player.GetMoney());
    }

    public void Buy()
    {
        gsm.GetComponent<GameManagerTest>().PurchaseProperty();
    }

    public void Sell()
    {
        gsm.GetComponent<GameManagerTest>().SellProperty();
    }

    public void DrawOpp()
    {
        gsm.GetComponent<GameManagerTest>().CheckSpace(7);
    }

    public void DrawPot()
    {
        gsm.GetComponent<GameManagerTest>().CheckSpace(2);
    }

    public void UpgradeProperty()
    {
        gsm.GetComponent<GameManagerTest>().UpgradeProperty();
    }

    public void DegradeProperty()
    {
        gsm.GetComponent<GameManagerTest>().DegradeProperty();
    }

    public void ListOppCards()
    {
        oppList = "Opp cards: " + "\n";
        foreach (GameObject card in board.OppCardList())
        {
            oppList += card.GetComponent<Card>().GetDescription() + "\n";
        }
        OppCardListText();
    }

    public void ListPotCards()
    {
        potList = "Pot cards: " + "\n";
        foreach (GameObject card in board.PotCardList())
        {
            potList += card.GetComponent<Card>().GetDescription() + "\n";
        }
        PotCardListText();
    }

    void OppCardListText()
    {
        OppCardList.SetText(oppList);
    }

    void PotCardListText()
    {
        PotLuckList.SetText(potList);
    }
}
