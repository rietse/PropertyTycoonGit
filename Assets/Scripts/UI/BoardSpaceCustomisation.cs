using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardSpaceCustomisation : MonoBehaviour
{
    public Board board;
    public TextMeshProUGUI currentText, inputText;
    public List<IntSelector> intSelectors; //Unity hates me so here theres now a bunch of these to avoid parsing - E
    public List<GameObject> propertySpaces = new List<GameObject>();
    public List<int> propertyPositions = new List<int>();
    public string spaceName;
    public int[] values = new int[7];
    public int displayedPos;

    public void InitialisePropertyList()
    {
        for (int i = 0; i < 40; i++) //grabs every space in board.spaces[] that is a property, it wouldn't do to try some of these methods on the jail for example! - E
        {
            if (board.GetSpace(i).GetComponent<Space>().GetType() == "PROP")
            {
                propertySpaces.Add(board.GetSpace(i));
                propertyPositions.Add(i);
            }
        }
        DisplayText(0); //defaults all data to the first property space (Old Creek my beloved) - E
    }

    void SetName(string text)
    {
        spaceName = text;
        currentText.text = text;
    }

    string GetName()
    {
        return spaceName;
    }

    public void SetIntCounter(int i, int j) //sets the values in the counters to their corresponding rent/price value - E
    {
        intSelectors[i].InitialiseText(j);
        values[i] = j;
    }

    public int GetIntCounter(int i) //grabs the value in the int counter, pretty self explainatory - E
    {
        int j = intSelectors[i].GetTotal();
        values[i] = j;
        return j;
    }

    public void DisplayText(int space)
    {
        displayedPos = space; //updates the pointer for where we are in the list - E
        List<int> rentList = propertySpaces[space].GetComponent<Property>().GetRentList(); //grabs the rent list - E

        SetName(propertySpaces[space].GetComponent<Property>().GetName()); //adds the property name to the inputField - E

        SetIntCounter(0, propertySpaces[space].GetComponent<Property>().GetPrice()); //sets all the counters - E
        SetIntCounter(1, rentList[0]);
        SetIntCounter(2, rentList[1]);
        SetIntCounter(3, rentList[2]);
        SetIntCounter(4, rentList[3]);
        SetIntCounter(5, rentList[4]);
        SetIntCounter(6, rentList[5]);
    }

    public void SaveText() //this is where the fun begins... not. - E
    {
        List<int> rentList = propertySpaces[displayedPos].GetComponent<Property>().GetRentList();
        for(int i = 0; i < 6; i++)
        {
            rentList[i] = GetIntCounter(i + 1); //offset by one as intCounters[0] is the space price - E
        }
        spaceName = inputText.text;
        currentText.text = spaceName;
        propertySpaces[displayedPos].GetComponent<Space>().SetName(spaceName); //grabs the inputted name, updates it within this script, then sends it off to the board space itself - E
        propertySpaces[displayedPos].GetComponent<Property>().SetPrice(GetIntCounter(0)); //sets the space price - E
        propertySpaces[displayedPos].GetComponent<Property>().SetRentList(rentList); //sets the rent values - E
        propertySpaces[displayedPos].GetComponent<Space>().RefreshText(GetIntCounter(0)); //refreshes the text on the board (the player won't see this happen as the menu is still up) - E
        print("Space " + propertyPositions[displayedPos] + " data saved!");
    }

    public void ResetText()
    {
        propertySpaces[displayedPos].GetComponent<Space>().ResetName(); //grabs the space's default values to reset it - E
        propertySpaces[displayedPos].GetComponent<Property>().ResetPrice();
        propertySpaces[displayedPos].GetComponent<Property>().ResetRentList();
        DisplayText(displayedPos); //refresh the UI - E
        propertySpaces[displayedPos].GetComponent<Space>().RefreshText(values[1]);
        print("Space " + propertyPositions[displayedPos] + " data reset!");
    }

    public void ShiftLeft()
    {
        int i = 0;
        if(displayedPos == 0) //checks if we are at the beginning of the list, then move to the other end if so - E
        {
            i = propertyPositions.Count - 1;
        }
        else { i = displayedPos - 1; }

        DisplayText(i); //updates the UI - E
    }

    public void ShiftRight()
    {
        int i = 0;
        if (displayedPos == (propertyPositions.Count - 1)) //checks if we are at the end of the list, then move to the other end if so - E
        {
            i = 0;
        }
        else { i = displayedPos + 1; }

        DisplayText(i); //updates the UI - E
    }
}
