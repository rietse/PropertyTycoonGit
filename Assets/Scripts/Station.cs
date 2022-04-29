using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : Space
{
    //Holds price and mortgage data for Station spaces
    public int statPrice;
    public bool isMortgaged = false;

    public int GetPrice()
    {
        return statPrice;
    }

    public void SetMortgaged(bool m)
    {
        isMortgaged = m;
    }

    public bool GetMortgaged()
    {
        return isMortgaged;
    }
}
