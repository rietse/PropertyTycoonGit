using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idNum;
    public enum Type { OPP, POT };
    public Type type;

    public string cardDescription;
    public int[] cardEffects;
    /*
    KEY:
    0 - allow player to take a new card instead of this card's effects (0,1,2) 1=OPP, 2=POT
    1 - player gets money (0,1)
    2 - player loses money (0,1)
    3 - player is fined (0,1)
    4 - money changed = total house/hotel value? (0,1,2) 1 = 40/house, 115/hotel, 2 = 25/house, 100/hotel
    5 - money to be changed (~)
    6 - move player? (0,1,2) 1 = move to space, 2 = move x space(s)
    7 - pass go? (0,1)
    8 - where move (if #6 = 1 (0,39), if #6 = 2 (~))
    9 - goto jail? (0,1)
    10 - get out of jail free? (0,1)
    11 - birthday? (0,1)
    */

    //Returns the ID number of a card
    public int GetID()
    {
        return idNum;
    }

    //Returns the type enum of a card
    public string GetType()
    {
        return type.ToString();
    }

    //Returns the description of a card
    public string GetDescription()
    {
        return cardDescription;
    }

    //Returns the effects of a card
    public int[] GetEffects()
    {
        return cardEffects;
    }

    //Replaces the effect of a card with a given value
    public void SetEffect(int pos, int val)
    {
        cardEffects[pos] = val;
    }
}
