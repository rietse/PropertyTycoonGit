using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenuButtons : MonoBehaviour
{
    public enum ButtonTypes {MOVE, BUY, SELL, MORTGAGE, ENDTURN}
    public ButtonTypes buttonType;

    public GameManager gm;

    void Update()
    {
        //sets what buttons are interactable depending on the state of the player's turn
        switch (buttonType)
        {
            case ButtonTypes.MOVE:
                if(gm.turnState == GameManager.TurnState.MOVING)
                {
                    gameObject.GetComponent<Button>().interactable = true;
                }
                else
                {
                    gameObject.GetComponent<Button>().interactable = false;
                }
                break;
            case ButtonTypes.BUY:
                if (gm.turnState == GameManager.TurnState.BUY)
                {
                    gameObject.GetComponent<Button>().interactable = true;
                }
                else
                {
                    gameObject.GetComponent<Button>().interactable = false;
                }
                break;
            case ButtonTypes.SELL:
                if (gm.turnState == GameManager.TurnState.SELL || gm.turnState == GameManager.TurnState.BUY)
                {
                    gameObject.GetComponent<Button>().interactable = true;
                }
                else
                {
                    gameObject.GetComponent<Button>().interactable = false;
                }
                break;
            case ButtonTypes.MORTGAGE:
                if (gm.turnState == GameManager.TurnState.SELL || gm.turnState == GameManager.TurnState.BUY)
                {
                    gameObject.GetComponent<Button>().interactable = true;
                }
                else
                {
                    gameObject.GetComponent<Button>().interactable = false;
                }
                break;
            case ButtonTypes.ENDTURN:
                if (gm.turnState == GameManager.TurnState.SELL || gm.turnState == GameManager.TurnState.BUY)
                {
                    gameObject.GetComponent<Button>().interactable = true;
                }
                else
                {
                    gameObject.GetComponent<Button>().interactable = false;
                }
                break;
            default:
                break;


        }
    }

    public void Move()
    {
        gm.RollDice();
        gm.MovePlayer();
    }
}
