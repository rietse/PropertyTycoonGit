using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardSpaceCustomisation : MonoBehaviour
{
    public Board board;
    public List<TextMeshProUGUI> currentUI, playerInputUI;
    public List<GameObject> propertySpaces = new List<GameObject>();
    public List<int> propertyPositions = new List<int>();
    public string[] spaceText = new string[8];
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

    void SetText(int i, string text)
    {
        spaceText[i] = text;
        currentUI[i].text = text;
    }

    string GetEnteredText(int x)
    {
        return playerInputUI[x].text;
    }

    public void DisplayText(int space)
    {
        displayedPos = space;
        List<int> rentList = propertySpaces[space].GetComponent<Property>().GetRentList();

        SetText(0, propertySpaces[space].GetComponent<Property>().GetName());
        SetText(1, propertySpaces[space].GetComponent<Property>().GetPrice().ToString());

        SetText(2, rentList[0].ToString());
        SetText(3, rentList[1].ToString());
        SetText(4, rentList[2].ToString());
        SetText(5, rentList[3].ToString());
        SetText(6, rentList[4].ToString());
        SetText(7, rentList[5].ToString());
    }

    public void SaveText()
    {
        List<int> rentList = propertySpaces[displayedPos].GetComponent<Property>().GetRentList();
        for (int i = 0; i < 8; i++)
        {
            string entered = GetEnteredText(i);
            if (entered != null)
            {
                spaceText[i] = entered;
                switch (i)
                {
                    case 0:
                        propertySpaces[displayedPos].GetComponent<Space>().SetName(spaceText[0]);
                        break;
                    case 1:
                        propertySpaces[displayedPos].GetComponent<Property>().SetPrice(Convert.ToInt32(spaceText[1]));
                        break;
                    case 2:
                        rentList[1] = Convert.ToInt32(spaceText[2]);
                        break;
                    case 3:
                        rentList[2] = Convert.ToInt32(spaceText[3]);
                        break;
                    case 4:
                        rentList[3] = Convert.ToInt32(spaceText[4]);
                        break;
                    case 5:
                        rentList[4] = Convert.ToInt32(spaceText[5]);
                        break;
                    case 6:
                        rentList[5] = Convert.ToInt32(spaceText[6]);
                        break;
                    case 7:
                        rentList[6] = Convert.ToInt32(spaceText[7]);
                        break;
                }
            }
        }
        propertySpaces[displayedPos].GetComponent<Property>().SetRentList(rentList);
        propertySpaces[displayedPos].GetComponent<Space>().RefreshText(Convert.ToInt32(spaceText[1]));
        print("Space " + propertyPositions[displayedPos] + " data saved!");
    }

    public void ResetText()
    {
        propertySpaces[displayedPos].GetComponent<Space>().ResetName();
        propertySpaces[displayedPos].GetComponent<Property>().ResetPrice();
        propertySpaces[displayedPos].GetComponent<Property>().ResetRentList();
        DisplayText(displayedPos);
        propertySpaces[displayedPos].GetComponent<Space>().RefreshText(Convert.ToInt32(spaceText[1]));
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
