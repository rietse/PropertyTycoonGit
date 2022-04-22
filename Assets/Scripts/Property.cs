using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : Space
{
    public enum Colour { BROWN, BLUE, PURPLE, ORANGE, RED, YELLOW, GREEN, DBLUE}
    public Colour colour;

    public int price;
    public int rent0;
    public int rent1;
    public int rent2;
    public int rent3;
    public int rent4;
    public int rent5;

    public int developmentLevel = 0;

    public int GetPrice()
    {
        return price;
    }
}
