using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceMovementTest : MonoBehaviour
{
    public TextMeshProUGUI diceResult;
    public TextMeshProUGUI doublesCounter;
    public TextMeshProUGUI hasMoved;
    public GameManagerTest gsm;
    public PlayerControllerTESTONLY player1;

    public void Dice()
    {
        gsm.GetComponent<GameManagerTest>().RollDice();
        DiceResultText();
        DoublesCounterText();
        HasMovedText();
    }
    public void Move()
    {
        gsm.GetComponent<GameManagerTest>().MovePlayer();
        HasMovedText();
    }

    void DiceResultText()
    {
        diceResult.SetText("diceResult = " + gsm.RollDice().ToString());
    }

    void DoublesCounterText()
    {
        doublesCounter.SetText("doublesCounter = " + player1.GetDoublesCounter().ToString());
    }

    void HasMovedText()
    {
        hasMoved.SetText("hasMoved = " + player1.GetHasMoved().ToString());
    }

    public void AllowMovement()
    {
        player1.SetHasMoved(false);
        HasMovedText();
    }
}
