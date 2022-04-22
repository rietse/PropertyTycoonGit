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

    public int GetMoney()
    {
        return currentMoney;
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
        CheckSpace();
    }

    public void CheckSpace()
    {
        GameObject space = board.GetSpace(currentPos);
        string type = space.GetComponent<Space>().GetType();

        /*switch(type)
        {
            case "":
                //GO, JAIL, PARK, GOJAIL, POT, OPP, PROP, TAX, STAT, UTIL
        }*/
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

        if ((property.GetComponent<Space>().GetType() == "PROP") && (board.GetState(currentPos) == 0)) //checks if it's property and unowned - E
        {
            if (property.GetComponent<Property>().GetPrice() > currentMoney) //checks if player can afford - E
            {
                print("Not enough money!");
            }
            else
            {
                print("Player " + player + " has bought " + property.GetComponent<Space>().GetName());
                currentMoney = currentMoney - property.GetComponent<Property>().GetPrice();
                board.SetState(currentPos, player); //assigns board space to player and deducts money - E

                SetMoneyText(currPlayerNo);
            }
        } else if ((property.GetComponent<Space>().GetType() == "UTIL") && (board.GetState(currentPos) == 0)) //same but deals with utilities, I forgot these existed - E
        {
            if (property.GetComponent<Utility>().GetPrice() > currentMoney) //checks if player can afford - E
            {
                print("Not enough money!");
            }
            else
            {
                print("Player " + player + " has bought the utility " + property.GetComponent<Space>().GetName());
                currentMoney = currentMoney - property.GetComponent<Utility>().GetPrice();
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

        if ((property.GetComponent<Space>().GetType() == "PROP") && (board.GetState(currentPos) == player)) //checks if it's property and owned by the player - E
        { 
            print("Player " + player + " has sold " + property.GetComponent<Space>().GetName());
            currentMoney = currentMoney + property.GetComponent<Property>().GetPrice();
            board.SetState(currentPos, 0); //assigns board space back to unowned and gives player money - E

            SetMoneyText(currPlayerNo);
        } else if ((property.GetComponent<Space>().GetType() == "UTIL") && (board.GetState(currentPos) == player)) //same but deals with utilities, I forgot these existed - E
        {
            print("Player " + player + " has sold the utility " + property.GetComponent<Space>().GetName());
            currentMoney = currentMoney + property.GetComponent<Utility>().GetPrice();
            board.SetState(currentPos, 0); //assigns board space back to unowned and gives player money - E

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

        if ((property.GetComponent<Space>().GetType() == "PROP") && (board.GetState(currentPos) == player)) //checks if it's property and owned by the player - E
        {
            if (property.GetComponent<Property>().GetMortgaged() == false) //checks if mortgaged or not - E
            {
                print("Player " + player + " has mortgaged " + property.GetComponent<Space>().GetName());
                currentMoney = currentMoney + (property.GetComponent<Property>().GetPrice() / 2); //gives half money - E
                property.GetComponent<Property>().SetMortgaged(true);
            }
            else
            {
                print("Player " + player + " has unmortgaged " + property.GetComponent<Space>().GetName());
                currentMoney = currentMoney - (property.GetComponent<Property>().GetPrice() / 2); //takes half money - E
                property.GetComponent<Property>().SetMortgaged(false);
            }
            SetMoneyText(currPlayerNo);
        }
        else if ((property.GetComponent<Space>().GetType() == "UTIL") && (board.GetState(currentPos) == player)) //same but deals with utilities, I forgot these existed - E
        {
            if (property.GetComponent<Utility>().GetMortgaged() == false) //checks if mortgaged or not - E
            {
                print("Player " + player + " has mortgaged the utility " + property.GetComponent<Space>().GetName());
                currentMoney = currentMoney + (property.GetComponent<Utility>().GetPrice() / 2); //gives half money - E
                property.GetComponent<Utility>().SetMortgaged(true);
            }
            else
            {
                print("Player " + player + " has unmortgaged the utility " + property.GetComponent<Space>().GetName());
                currentMoney = currentMoney - (property.GetComponent<Utility>().GetPrice() / 2); //takes half money - E
                property.GetComponent<Utility>().SetMortgaged(false);
            }
            SetMoneyText(currPlayerNo);
        }
        else
        {
            print("Can't mortgage/unmortgage this space!");
        }
    }
}
