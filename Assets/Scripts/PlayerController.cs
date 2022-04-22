using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Board board;
    private Vector3 offset = new Vector3(0.0f,0.0f,0.0f);
    private string currPlayerNo = "1"; //need to change this when we have roll dice to determine order, for now this just stops it being blank - E
    public int currentPos;
    public int currentMoney;
    public TextMeshProUGUI moneyText;
    public string name;
    public bool isBankrupt = false;

    void Start()
    {
        currentMoney = 1500;
        SetMoneyText(currPlayerNo);
    }


    void Update()
    {
        
    }

    public int GetPos()
    {
        return currentPos;
    }

    public int GetMoney()
    {
        return currentMoney;
    }

    public void PayRent(int rent)
    {
        currentMoney = currentMoney - rent;
        SetMoneyText(currPlayerNo);
    }

    public void RecieveRent(int rent)
    {
        currentMoney = currentMoney + rent;
    }

    public void SetBankrupt()
    {
        isBankrupt = true;
    }

    public bool GetBankrupt()
    {
        return isBankrupt;
    }

    public void SetOffset(float x, float z)
    {
        offset.x = x;
        offset.z = z;
    }

    public void OffsetPlayer()
    {
        transform.position += offset;
    }

    public void SetMoneyText(string playerNo)
    {
        currPlayerNo = playerNo;
        moneyText.text = "Player " + currPlayerNo + " Current Money: £" + currentMoney.ToString();
    }

    public void Move(int d)
    {

        if ((currentPos + d) > 39)
        {
            int i = (currentPos + d) - 40;
            currentPos = i;
            PassGo();
        }
        else
        {
            currentPos += d;
        }
        transform.position = board.spaces[currentPos].transform.position + offset;
    }

    public void PassGo()
    {
        currentMoney += 200;
        SetMoneyText(currPlayerNo);
    }

    public void PurchaseProperty(int player)
    {
        currPlayerNo = player.ToString(); //might as well keep this updated ig - E

        GameObject property = board.GetSpace(currentPos); //grabs the space - E
        int price = 0;

        if (property.GetComponent<Space>().GetType() == "PROP") //checks which space it is - E
        {
            price = property.GetComponent<Property>().GetPrice();
        } else if (property.GetComponent<Space>().GetType() == "UTIL")
        {
            price = property.GetComponent<Utility>().GetPrice();
        } else if (property.GetComponent<Space>().GetType() == "STAT")
        {
            price = property.GetComponent<Station>().GetPrice();
        }

        if (board.GetState(currentPos) == 0)
        {
            if (price > currentMoney)
            {
                print("Not enough money!");
            }
            else
            {
                print("Player " + player + " has bought " + property.GetComponent<Space>().GetName());
                currentMoney = currentMoney - price;
                board.SetState(currentPos, player); //assigns board space to player and deducts money - E
                SetMoneyText(currPlayerNo);
            }
        }
        else
        {
            print("Can't buy this space!");
        }
    }

    public void SellProperty(int player)
    {
        currPlayerNo = player.ToString(); //might as well keep this updated ig - E

        GameObject property = board.GetSpace(currentPos); //grabs the space - E
        int price = 0;

        if (property.GetComponent<Space>().GetType() == "PROP") //checks which space it is - E
        {
            price = property.GetComponent<Property>().GetPrice();
        }
        else if (property.GetComponent<Space>().GetType() == "UTIL")
        {
            price = property.GetComponent<Utility>().GetPrice();
        }
        else if (property.GetComponent<Space>().GetType() == "STAT")
        {
            price = property.GetComponent<Station>().GetPrice();
        }

        if (board.GetState(currentPos) == player)
        {
                print("Player " + player + " has sold " + property.GetComponent<Space>().GetName());
                currentMoney = currentMoney + price;
                board.SetState(currentPos, 0); //assigns board space back to unowned and gives money - E
                SetMoneyText(currPlayerNo);
        }
        else
        {
            print("Can't sell this space!");
        }
    }

    public void MortgageProperty(int player)
    {
        currPlayerNo = player.ToString(); //might as well keep this updated ig - E

        GameObject property = board.GetSpace(currentPos); //grabs the space - E
        int price = 0;
        bool mortgaged = false;

        if (property.GetComponent<Space>().GetType() == "PROP") //checks which space it is - E
        {
            price = property.GetComponent<Property>().GetPrice() / 2;
            mortgaged = property.GetComponent<Property>().GetMortgaged();
        }
        else if (property.GetComponent<Space>().GetType() == "UTIL")
        {
            price = property.GetComponent<Utility>().GetPrice() / 2;
            mortgaged = property.GetComponent<Utility>().GetMortgaged();
        }
        else if (property.GetComponent<Space>().GetType() == "STAT")
        {
            price = property.GetComponent<Station>().GetPrice() / 2;
            mortgaged = property.GetComponent<Station>().GetMortgaged();
        }

        if (board.GetState(currentPos) == player)
        {
            if (mortgaged == true) //check if we need to mortgage or unmortgage this space - E
            {
                if (price > currentMoney)
                {
                    print("Not enough money!");
                }
                else
                {
                    print("Player " + player + " has unmortgaged " + property.GetComponent<Space>().GetName());
                    currentMoney = currentMoney - price;
                    if (property.GetComponent<Space>().GetType() == "PROP")
                    {
                        property.GetComponent<Property>().SetMortgaged(false);
                    }
                    else if (property.GetComponent<Space>().GetType() == "UTIL")
                    {
                        property.GetComponent<Utility>().SetMortgaged(false);
                    }
                    else if (property.GetComponent<Space>().GetType() == "STAT")
                    {
                        property.GetComponent<Station>().SetMortgaged(false);
                    }
                    SetMoneyText(currPlayerNo);
                }
            }
            else
            {
                print("Player " + player + " has mortgaged " + property.GetComponent<Space>().GetName());
                currentMoney = currentMoney + price;
                if (property.GetComponent<Space>().GetType() == "PROP")
                {
                    property.GetComponent<Property>().SetMortgaged(true);
                }
                else if (property.GetComponent<Space>().GetType() == "UTIL")
                {
                    property.GetComponent<Utility>().SetMortgaged(true);
                }
                else if (property.GetComponent<Space>().GetType() == "STAT")
                {
                    property.GetComponent<Station>().SetMortgaged(true);
                }
                SetMoneyText(currPlayerNo);
            }
        }
        else
        {
            print("Can't mortgame/unmortgage this space!");
        }
    }
}
