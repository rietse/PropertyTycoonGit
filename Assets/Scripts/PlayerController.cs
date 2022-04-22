using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Board board;
    private Vector3 offset = new Vector3(0.0f,0.0f,0.0f);
    private string currPlayerNo = "1"; //need to change this when we have roll dice to determine order, for now this just stops it being blank - E
    private int currentPos;
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
}
