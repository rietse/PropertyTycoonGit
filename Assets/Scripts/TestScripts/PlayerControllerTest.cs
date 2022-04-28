using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControllerTest : MonoBehaviour
{
    public TextMeshProUGUI currentPlayerNo;
    public TextMeshProUGUI playerMoney;
    public GameManager gsm;
    public PlayerController player;

    public void Dice()
    {
        gsm.GetComponent<GameManager>().RollDice();
        currentPlayerText();
    }
    public void Move()
    {
        gsm.GetComponent<GameManager>().MovePlayer();
    }

    void currentPlayerText()
    {
        currentPlayerNo.SetText("Current Player = " + gsm.GetCurrentPlayer().ToString());
    }
    public void EndTurn()
    {
        gsm.GetComponent<GameManager>().NextPlayer();
        currentPlayerText();
    }

    public void Bankrupt()
    {
        gsm.GetComponent<GameManager>().Bankrupt();
    }

    void PlayerMoneyText()
    {
        playerMoney.SetText("Player " + gsm.GetCurrentPlayer().ToString() + ": " + player.GetMoney());
    }

    public void UpdateMoney()
    {
        PlayerMoneyText();
    }
}
