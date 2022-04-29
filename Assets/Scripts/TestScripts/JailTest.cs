using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JailTest : MonoBehaviour
{
    public TextMeshProUGUI currentPlayerNo;
    public TextMeshProUGUI playerMoney;
    public TextMeshProUGUI diceResult;
    public TextMeshProUGUI doublesCounter;
    public TextMeshProUGUI hasMoved;
    public GameManagerTest gsm;
    public PlayerControllerTESTONLY player;
    public TextMeshProUGUI jailCounter;

    public void Dice()
    {
        gsm.GetComponent<GameManagerTest>().RollDice();
        currentPlayerText();
        DiceResultText();
        DoublesCounterText();
        HasMovedText();
        jailCounterText();
    }
    public void Move()
    {
        gsm.GetComponent<GameManagerTest>().MovePlayer();
        HasMovedText();
        jailCounterText();
    }

    void currentPlayerText()
    {
        currentPlayerNo.SetText("Current Player = " + gsm.GetCurrentPlayer().ToString());
    }
    public void EndTurn()
    {
        gsm.GetComponent<GameManagerTest>().NextPlayer();
        currentPlayerText();
        jailCounterText();
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

    public void PayFine()
    {
        gsm.GetComponent<GameManagerTest>().JailFine();
        jailCounterText();
    }

    public void GoJail()
    {
        gsm.GetComponent<GameManagerTest>().GoToJail();
        jailCounterText();
    }

    public void UseJailFreeCard()
    {
        gsm.GetComponent<GameManagerTest>().UseJailCard();
    }

    public void ReceiveJailCard()
    {
        player.RecieveFreeJailCard();
    }

    void DiceResultText()
    {
        diceResult.SetText("diceResult = " + gsm.RollDice().ToString());
    }

    void DoublesCounterText()
    {
        doublesCounter.SetText("doublesCounter = " + player.GetDoublesCounter().ToString());
    }

    void HasMovedText()
    {
        hasMoved.SetText("hasMoved = " + player.GetHasMoved().ToString());
    }

    void jailCounterText()
    {
        jailCounter.SetText("jailCounter = " + player.GetJailCounter().ToString());
    }
}
