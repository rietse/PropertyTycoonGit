using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public PlayerController player;
    public int diceResult;

    public void MovePlayer()
    {
        //player.Move();
    }

    public void RollDice()
    {
        int d1 = Random.Range(1, 6);
        int d2 = Random.Range(1, 6);
        diceResult = d1 + d2;
        print(diceResult);
    }

}
