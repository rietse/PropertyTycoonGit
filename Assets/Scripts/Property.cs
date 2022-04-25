using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : Space
{
    public enum Colour { BROWN, BLUE, PURPLE, ORANGE, RED, YELLOW, GREEN, DBLUE}
    public Colour colour;

    public bool isMortgaged = false;
    public int price;
    public int rent0;
    public int rent1;
    public int rent2;
    public int rent3;
    public int rent4;
    public int rent5;
    public List<int> rentList = new List<int>(); //make this public if you decide to do the task below :) - E

    public int developmentLevel = 0;

    public void InitialiseRentList()
    {
        rentList.Add(rent0); //super inefficient but I can't be asked to rewrite the price data into a List instead of seperate ints (If someone else wants to the by all means *wink wink nudge nudge*) - E
        rentList.Add(rent1); //I mean, it's either that or a ton of if statements and that's even worse... actually I will nuke the repo if anyone does this. Don't test me. - also E
        rentList.Add(rent2);
        rentList.Add(rent3);
        rentList.Add(rent4);
        rentList.Add(rent5);
    }

    public void SetMortgaged(bool m)
    {
        isMortgaged = m;
    }

    public bool GetMortgaged()
    {
        return isMortgaged;
    }

    public int GetPrice()
    {
        return price;
    }

    public int GetRent()
    {
        return rentList[developmentLevel];
    }

    public int GetDevelopmentLevel()
    {
        return developmentLevel;
    }

    public void UpgradeProperty()
    {
        developmentLevel += 1;
    }

    public void DegradeProperty()
    {
        developmentLevel += -1;
    }
}
