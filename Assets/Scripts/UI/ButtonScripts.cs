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

    public void DrawOpp()
    {
        //this is only for testing purposes pls remove once we make game do game - E
        gsm.GetComponent<GameManager>().CheckSpace(7);
    }

    public void DrawPot()
    {
        //this is only for testing purposes pls remove once we make game do game - E
        gsm.GetComponent<GameManager>().CheckSpace(2);
    }

    public void PayFine()
    {
        gsm.GetComponent<GameManager>().JailFine();
    }

    public void GoJail()
    {
        gsm.GetComponent<GameManager>().GoToJail();
    }

    public void UpgradeProperty()
    {
        gsm.GetComponent<GameManager>().UpgradeProperty();
    }

    public void DegradeProperty()
    {
        gsm.GetComponent<GameManager>().DegradeProperty();
    }

    public void UseJailFreeCard()
    {
        gsm.GetComponent<GameManager>().UseJailCard();
    }

    public void SwitchCamera()
    {
        gsm.GetComponent<CameraController>().SwitchCameraView();
    }
}
