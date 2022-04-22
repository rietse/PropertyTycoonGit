using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScripts : MonoBehaviour
{
    public GameObject gsm;

    // literally a dice roller, what more do you want? - E
    public void Dice()
    {
        gsm.GetComponent<GameManager>().RollDice();
    }

    //does the player movements - E
    public void Move()
    {
        gsm.GetComponent<GameManager>().MovePlayer();
    }

    public void EndTurn()
    {
        gsm.GetComponent<GameManager>().NextPlayer();
    }

    public void Buy()
    {
        gsm.GetComponent<GameManager>().PurchaseProperty();
    }

    public void Sell()
    {
        gsm.GetComponent<GameManager>().SellProperty();
    }

    public void Bankrupt()
    {
        gsm.GetComponent<GameManager>().Bankrupt();
    }

    public void Mortgage()
    {
        gsm.GetComponent<GameManager>().MortgageProperty();
    }
}
