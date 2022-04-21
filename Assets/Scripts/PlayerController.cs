using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Board board;
    public MovementController mc;
    private int currentPos;
    private int currentMoney;
    public TextMeshProUGUI moneyText;

    void Start()
    {
        currentMoney = 1500;
        SetMoneyText();
    }


    void Update()
    {
        
    }
    void SetMoneyText()
    {
        moneyText.text = "Current Money:" + currentMoney.ToString();
    }

    public void Move()
    {

        if ((currentPos + mc.diceResult) > 39)
        {
            int i = (currentPos + mc.diceResult) - 40;
            currentPos = i;
        }
        else
        {
            currentPos += mc.diceResult;
        }
        transform.position = board.spaces[currentPos].transform.position;
    }

}
