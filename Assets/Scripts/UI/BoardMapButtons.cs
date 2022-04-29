using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMapButtons : MonoBehaviour
{
    public Board board;
    public int pos;
    public PropertyDisplay propertyDisplay;

    //Updates space display depending on what button on board menu was clicked
    public void MapButtonPressed()
    {
        print("Button at position " + pos + " clicked!");
        propertyDisplay.SetDisplay(board.GetSpace(pos), pos); 
    }
}
