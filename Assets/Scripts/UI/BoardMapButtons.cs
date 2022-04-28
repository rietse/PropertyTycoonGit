using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMapButtons : MonoBehaviour
{
    public Board board;
    public int pos;

    public void MapButtonPressed()
    {
        print("Button at position " + pos + " clicked!");
        //do update property UT @ board.GetSpace(pos) - E
    }
}
