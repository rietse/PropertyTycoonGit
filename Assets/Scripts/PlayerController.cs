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
    public int jailFreeCards = 0;
    public TextMeshProUGUI moneyText;
    public string name;
    public bool isBankrupt = false;
    public bool hasRolled = false;
    public bool hasMoved = false;
    public bool hasPassedGo = false;
    public bool canReroll = false;
    public int doublesCounter = 0;

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

    public void SetPos(int x)
    {
        currentPos = x;
        transform.position = board.spaces[x].transform.position + offset;
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

    public void SetHasMoved(bool moved)
    {
        hasMoved = moved;
    }

    public bool GetHasMoved()
    {
        return hasMoved;
    }

    public void SetReroll(bool reroll)
    {
        canReroll = reroll;
    }

    public bool GetReroll()
    {
        return canReroll;
    }

    public void AddDoublesCounter()
    {
        doublesCounter += 1;
    }

    public int GetDoublesCounter()
    {
        return doublesCounter;
    }

    public void ResetDoublesCounter()
    {
        doublesCounter = 0;
    }

    public void Move(int d, bool overrideMove)
    {
        if ((!GetHasMoved()) || (overrideMove == true))
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
            if (!GetReroll()) { 
                SetHasMoved(true); 
            }
        }
    }

    public void SetHasPassedGo(bool passed)
    {
        hasPassedGo = passed;
    }

    public bool GetHasPassedGo()
    {
        return hasPassedGo;
    }

    public void PassGo()
    {
        currentMoney += 200;
        print("Player " + currPlayerNo + " has passed GO!");
        SetMoneyText(currPlayerNo);
        SetHasPassedGo(true);
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

    public int GetTotalOwnedHouses(int player)
    {
        int houses = 0;

        for (int i = 0; i < 40; i++)
        {
            if ((board.GetSpace(i).GetComponent<Space>().GetType() == "PROP") && (board.GetState(i) == player))
            {
                houses += board.GetHouses(i);
            }
        }
        houses = 7; //temp so we actually can have some values as no development is there yet so the above will just be 0 which is a bit boring - E
        return houses; 
    }

    public int GetTotalOwnedHotels(int player)
    {
        int hotels = 0;

        for (int i = 0; i < 40; i++)
        {
            if ((board.GetSpace(i).GetComponent<Space>().GetType() == "PROP") && (board.GetState(i) == player))
            {
                hotels += board.GetHotels(i);
            }
        }
        hotels = 2; //same as GetTotalOwnedHouses(), but for hotels - E
        return hotels;
    }

    public void RecieveFreeJailCard()
    {
        jailFreeCards += 1;
    }
}
