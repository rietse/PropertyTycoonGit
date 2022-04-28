using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMapButtons : MonoBehaviour
{
    public Board board;
    public int pos;
    public PropertyDisplay propertyDisplay;

    public void MapButtonPressed()
    {
        print("Button at position " + pos + " clicked!");
        propertyDisplay.SetDisplay(board.GetSpace(pos), pos); //updates whatever space display thingamajig UI we have to show the space data, cool - E
    }
}
