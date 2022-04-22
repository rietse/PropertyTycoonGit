using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : Space
{
    public int utilPrice;
    public bool isMortgaged = false;

    public int GetPrice()
    {
        return utilPrice;
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
