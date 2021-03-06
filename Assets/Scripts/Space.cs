using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Space : MonoBehaviour
{
    public int idNum;
    public enum Type { GO, JAIL, PARK, GOJAIL, POT, OPP, PROP, TAX, STAT, UTIL }
    public Type type;
    public string spaceName;
    public TextMeshPro boardSpaceName;
    public TextMeshPro boardSpacePrice;
    public string defaultName;

    //Initialises the property name and text
    public void InitialiseText()
    {
        if (type == Type.PROP || type == Type.STAT || type == Type.UTIL) //grabs the stored spaceName, allows player to customise buyable tiles without messing with gameplay tiles - E
        {
            boardSpaceName.SetText(spaceName);
        }
        else //if its nor purchasable, then we hide the price text as we don't want it to show on those tiles - E
        {
            boardSpacePrice.SetText("");
        }
    }

    public void SetDefaultName()
    {
        defaultName = spaceName;
    }

    //Resets the space name to its default name
    public void ResetName()
    {
        spaceName = defaultName;
    }

    //Initialise the displayed price of a property
    public void InitialisePriceText(int p)
    {
        boardSpacePrice.SetText("?" + p);
    }

    //Updates board text
    public void RefreshText(int i)
    {
        InitialiseText();
        InitialisePriceText(i);
    }

    public string GetType()
    {
        return type.ToString();
    }

    public string GetName()
    {
        return spaceName;
    }

    public void SetName(string n)
    {
        spaceName = n;
    }
}
