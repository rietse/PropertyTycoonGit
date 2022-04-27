using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupMenuButtons : MonoBehaviour
{
    public int i;
    public bool isAbridged;
    public GameManager gm;

    public void SetNumber()
    {
        //enabling all other buttons so only 1 appears "selected"
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }

        //disabling selected button so it appears to stay selected persistently
        gameObject.GetComponent<Button>().interactable = false;

        gm.noOfPlayers = i;

    }

    public void SetRules()
    {
        //enabling all other buttons so only 1 appears "selected"
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button2");
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }

        //disabling selected button so it appears to stay selected persistently
        gameObject.GetComponent<Button>().interactable = false;

        if (isAbridged)
        {
            //talk to gm
        }
        else
        {
            //talk to gm
        }
    }
}
