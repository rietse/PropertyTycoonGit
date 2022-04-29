using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : Space
{
    public enum Colour { BROWN, BLUE, PURPLE, ORANGE, RED, YELLOW, GREEN, DBLUE }
    public Colour colour;

    public PropertyHouseManager propertyHouseManager;

    public bool isMortgaged = false;
    public int upgradeCost;
    public int price;
    public List<int> rentList = new List<int>();
    public int developmentLevel = 0;

    private List<int> defaultRentList;
    private int defaultPrice;

    //Initialises rent list and price list
    void Start()
    {
        defaultRentList = rentList;
        defaultPrice = price;
    }

    public void SetPrice(int p)
    {
        price = p;
    }

    //Resets property price to its default price
    public void ResetPrice()
    {
        price = defaultPrice;
    }

    //Resets list of rent prices to default rent list
    public void ResetRentList()
    {
        rentList = defaultRentList;
    }

    //Sets upgrade costs based on property colour
    public void InitialiseUpgradeCost()
    {
        switch(colour)
        {
            case Colour.BROWN:
            case Colour.BLUE:
                upgradeCost = 50;
                break;
            case Colour.PURPLE:
            case Colour.ORANGE:
                upgradeCost = 100;
                break;
            case Colour.RED:
            case Colour.YELLOW:
                upgradeCost = 150;
                break;
            case Colour.GREEN:
            case Colour.DBLUE:
                upgradeCost = 200;
                break;
        }
    }

    //Sets position of houses on the board
    public void InitialiseHousePositions()
    {
        propertyHouseManager.SetPosition(transform.position);
    }

    public string GetColour()
    {
        return colour.ToString();
    }

    public int GetUpgradeCost()
    {
        return upgradeCost;
    }

    public void SetMortgaged(bool m)
    {
        isMortgaged = m;
    }

    public bool GetMortgaged()
    {
        return isMortgaged;
    }

    public int GetPrice()
    {
        return price;
    }

    public int GetRent()
    {
        return rentList[developmentLevel];
    }

    public List<int> GetRentList()
    {
        return rentList;
    }

    public void SetRentList(List<int> r)
    {
        rentList = r;
    }

    public int GetDevelopmentLevel()
    {
        return developmentLevel;
    }

    //Increases development level by 1 and places a house on the intended property
    public void UpgradeProperty()
    {
        developmentLevel += 1;
        propertyHouseManager.UpdateHouses(developmentLevel);
    }

    //Decreases development level by 1 and removes a house from the intended property
    public void DegradeProperty()
    {
        developmentLevel += -1;
        propertyHouseManager.UpdateHouses(developmentLevel);
    }
}
