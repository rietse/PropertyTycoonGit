using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntSelector : MonoBehaviour
{
    public int total;
    public TextMeshProUGUI i1, i2, i3, i4;

    //Initialises number to be selected
    public void InitialiseText(int i) 
    {
        string s = i.ToString();
        switch(s.Length) //switch case determines the number of digits so they go where they are supposed to - E
        {
            case 1:
                i1.text = "0";
                i2.text = "0";
                i3.text = "0";
                i4.text = s.ToCharArray()[0].ToString();
                break;
            case 2:
                i1.text = "0";
                i2.text = "0";
                i3.text = s.ToCharArray()[0].ToString();
                i4.text = s.ToCharArray()[1].ToString();
                break;
            case 3:
                i1.text = "0";
                i2.text = s.ToCharArray()[0].ToString();
                i3.text = s.ToCharArray()[1].ToString();
                i4.text = s.ToCharArray()[2].ToString();
                break;
            case 4:
                i1.text = s.ToCharArray()[0].ToString();
                i2.text = s.ToCharArray()[1].ToString();
                i3.text = s.ToCharArray()[2].ToString();
                i4.text = s.ToCharArray()[3].ToString();
                break;
        }
        SetTotal(i);
    }

    public int GetTotal()
    {
        return total;
    }

    public void SetTotal(int i)
    {
        total = i;
    }

    void CalculateTotal()
    {
        int i = 0;
        i += Convert.ToInt32(i1.text) * 1000;
        i += Convert.ToInt32(i2.text) * 100;
        i += Convert.ToInt32(i3.text) * 10;
        i += Convert.ToInt32(i4.text);
        SetTotal(i);
    }

    public void PressUB1()
    {
        int i = Convert.ToInt32(i1.text);
        if (i == 9) //loops back if at 9 - E
        {
            i1.text = "0";
        }
        else
        {
            i1.text = (Convert.ToInt32(i1.text) + 1).ToString();
        }
        CalculateTotal(); //gotta keep this updated - E
    }

    public void PressUB2()
    {
        int i = Convert.ToInt32(i2.text);
        if (i == 9)
        {
            i2.text = "0";
        }
        else
        {
            i2.text = (Convert.ToInt32(i2.text) + 1).ToString();
        }
        CalculateTotal();
    }

    public void PressUB3()
    {
        int i = Convert.ToInt32(i3.text);
        if (i == 9)
        {
            i3.text = "0";
        }
        else
        {
            i3.text = (Convert.ToInt32(i3.text) + 1).ToString();
        }
        CalculateTotal();
    }
    public void PressUB4()
    {
        int i = Convert.ToInt32(i4.text);
        if (i == 9)
        {
            i4.text = "0";
        }
        else
        {
            i4.text = (Convert.ToInt32(i4.text) + 1).ToString();
        }
        CalculateTotal();
    }
    public void PressDB1()
    {
        int i = Convert.ToInt32(i1.text);
        if (i == 0)
        {
            i1.text = "9";
        }
        else
        {
            i1.text = (Convert.ToInt32(i1.text) - 1).ToString();
        }
        CalculateTotal();
    }

    public void PressDB2()
    {
        int i = Convert.ToInt32(i2.text);
        if (i == 0)
        {
            i2.text = "9";
        }
        else
        {
            i2.text = (Convert.ToInt32(i2.text) - 1).ToString();
        }
        CalculateTotal();
    }

    public void PressDB3()
    {
        int i = Convert.ToInt32(i3.text);
        if (i == 0)
        {
            i3.text = "9";
        }
        else
        {
            i3.text = (Convert.ToInt32(i3.text) - 1).ToString();
        }
        CalculateTotal();
    }

    public void PressDB4()
    {
        int i = Convert.ToInt32(i4.text);
        if (i == 0)
        {
            i4.text = "9";
        }
        else
        {
            i4.text = (Convert.ToInt32(i4.text) - 1).ToString();
        }
        CalculateTotal();
    }
}
