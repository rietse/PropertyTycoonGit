using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Board board;
    public MovementController mc;
    private Vector3 offset = new Vector3(0.0f,0.0f,0.0f);
    private int currentPos;
    private int currentMoney;
    public TextMeshProUGUI moneyText;
    public string name;

    void Start()
    {
        currentMoney = 1500;
        SetMoneyText();
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

    void SetMoneyText()
    {
        moneyText.text = "Current Money: £" + currentMoney.ToString();
    }

    public void Move()
    {

        if ((currentPos + mc.diceResult) > 39)
        {
            int i = (currentPos + mc.diceResult) - 40;
            currentPos = i;
            PassGo();
        }
        else
        {
            currentPos += mc.diceResult;
        }
        transform.position = board.spaces[currentPos].transform.position + offset;
    }

    public void PassGo()
    {
        currentMoney += 200;
        SetMoneyText();
    }
}
