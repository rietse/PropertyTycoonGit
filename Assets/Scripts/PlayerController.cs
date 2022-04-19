using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Board board;
    public MovementController mc;
    private int currentPos;
    void Start()
    {
    }


    void Update()
    {
        
    }

    public void Move()
    {

        if ((currentPos + mc.diceResult) > 39)
        {
            int i = (currentPos + mc.diceResult) - 40;
            currentPos = i;
        }
        else
        {
            currentPos += mc.diceResult;
        }
        transform.position = board.spaces[currentPos].transform.position;
    }

}
