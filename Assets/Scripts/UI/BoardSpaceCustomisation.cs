using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardSpaceCustomisation : MonoBehaviour
{
    public Board board;
    public TextMeshProUGUI currentText, inputText;
    public List<IntSelector> intSelectors;
    public List<GameObject> propertySpaces = new List<GameObject>();
    public List<int> propertyPositions = new List<int>();
    public string spaceName;
    public int[] values = new int[7];
    public int displayedPos;

    public void InitialisePropertyList()
    {
        for (int i = 0; i < 40; i++)
        {
            if (board.GetSpace(i).GetComponent<Space>().GetType() == "PROP")
            {
                propertySpaces.Add(board.GetSpace(i));
                propertyPositions.Add(i);
            }
        }
        DisplayText(0);
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

    public void SetIntCounter(int i, int j)
    {
        intSelectors[i].InitialiseText(j);
        values[i] = j;
    }

    public int GetIntCounter(int i)
    {
        int j = intSelectors[i].GetTotal();
        values[i] = j;
        return j;
    }

    public void DisplayText(int space)
    {
        displayedPos = space;
        List<int> rentList = propertySpaces[space].GetComponent<Property>().GetRentList();

        SetName(propertySpaces[space].GetComponent<Property>().GetName());

        SetIntCounter(0, propertySpaces[space].GetComponent<Property>().GetPrice());
        SetIntCounter(1, rentList[0]);
        SetIntCounter(2, rentList[1]);
        SetIntCounter(3, rentList[2]);
        SetIntCounter(4, rentList[3]);
        SetIntCounter(5, rentList[4]);
        SetIntCounter(6, rentList[5]);
    }

    public void SaveText()
    {
        List<int> rentList = propertySpaces[displayedPos].GetComponent<Property>().GetRentList();
        for(int i = 0; i < 6; i++)
        {
            rentList[i] = GetIntCounter(i + 1); //offset by one as intCounters[0] is the space price - E
        }
        spaceName = inputText.text;
        currentText.text = spaceName;
        propertySpaces[displayedPos].GetComponent<Space>().SetName(spaceName);
        propertySpaces[displayedPos].GetComponent<Property>().SetPrice(GetIntCounter(0));
        propertySpaces[displayedPos].GetComponent<Property>().SetRentList(rentList);
        propertySpaces[displayedPos].GetComponent<Space>().RefreshText(GetIntCounter(0));
        print("Space " + propertyPositions[displayedPos] + " data saved!");
    }

    public void ResetText()
    {
        propertySpaces[displayedPos].GetComponent<Space>().ResetName();
        propertySpaces[displayedPos].GetComponent<Property>().ResetPrice();
        propertySpaces[displayedPos].GetComponent<Property>().ResetRentList();
        DisplayText(displayedPos);
        propertySpaces[displayedPos].GetComponent<Space>().RefreshText(values[1]);
        print("Space " + propertyPositions[displayedPos] + " data reset!");
    }

    public void ShiftLeft()
    {
        int i = 0;
        if(displayedPos == 0)
        {
            i = propertyPositions.Count - 1;
        }
        else { i = displayedPos - 1; }

        DisplayText(i);
    }

    public void ShiftRight()
    {
        int i = 0;
        if (displayedPos == (propertyPositions.Count - 1))
        {
            i = 0;
        }
        else { i = displayedPos + 1; }

        DisplayText(i);
    }
}
