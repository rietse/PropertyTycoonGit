using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    public GameObject[] spaces;
    public int idNum;
    public enum Type { GO, JAIL, PARK, GOJAIL, POT, OPP, PROP, TAX, STAT, UTIL }
    public Type type;

    public enum Colour { BROWN, BLUE, PURPLE, ORANGE, RED, YELLOW, GREEN, DBLUE, NULL}
    public Colour colour;

    public string spaceName;

    public int price;
    public int rent0;
    public int rent1;
    public int rent2;
    public int rent3;
    public int rent4;
    public int rent5;

    public int developmentLevel = 0;

}
