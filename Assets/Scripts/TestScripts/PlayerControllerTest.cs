using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControllerTest : MonoBehaviour
{
    public TextMeshProUGUI currentPlayerNo;
    public TextMeshProUGUI diceResult;
    public TextMeshProUGUI playerMoney;
    public GameManagerTest gsm;
    public PlayerControllerTESTONLY player;

    public void Dice()
    {
        gsm.GetComponent<GameManagerTest>().RollDice();
        currentPlayerText();
        DiceResultText();
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

    public void Bankrupt()
    {
        gsm.GetComponent<GameManagerTest>().Bankrupt();
    }

    void PlayerMoneyText()
    {
        playerMoney.SetText("Player " + gsm.GetCurrentPlayer().ToString() + ": " + player.GetMoney());
    }

    void DiceResultText()
    {
        diceResult.SetText("diceResult = " + gsm.RollDice().ToString());
    }

    public void UpdateMoney()
    {
        PlayerMoneyText();
    }

    public void Buy()
    {
        gsm.GetComponent<GameManagerTest>().PurchaseProperty();
    }

    public void Sell()
    {
        gsm.GetComponent<GameManagerTest>().SellProperty();
    }

    public void UpgradeProperty()
    {
        gsm.GetComponent<GameManagerTest>().UpgradeProperty();
    }

    public void DegradeProperty()
    {
        gsm.GetComponent<GameManagerTest>().DegradeProperty();
    }

    public void Mortgage()
    {
        gsm.GetComponent<GameManagerTest>().MortgageProperty();
    }

    public void ReturnStartMenu()
    {
        SceneManager.LoadScene("Main");
    }
}
