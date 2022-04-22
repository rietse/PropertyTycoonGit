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

    void Start()
    {
        currentMoney = 1500;
        SetMoneyText(currPlayerNo);
    }


    void Update()
    {
        
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
        }
        else
        {
            print("Can't sell this space!");
        }

    }
}
