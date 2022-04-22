using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board;
    public PlayerController player1, player2, player3, player4, player5;
    public int currentPlayer = 1;
    public int noOfPlayers = 0;
    public List<PlayerController> playerList = new List<PlayerController>();
    
    public enum SpaceStatus { p1, p2, p3, p4, p5, unowned, special}
    public List<SpaceStatus> spaceStatus = new List<SpaceStatus>();

    private int currentRoll;

    // Start is called before the first frame update
    void Start()
    {
        if (noOfPlayers == 0)
        {
            //get number of players once we can choose how many are gonna play, for now I'm setting it to all 5 so I can test this works, we might also  chuck this somewhere else, it's just here as a reminder to code this lol - E
            noOfPlayers = 5;
        }
        InitialisePlayers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitialisePlayers()
    {
        //always gonna be at least 2 players including AI, you can't play Property Tycoon on your own - E
        playerList.Add(player1);
        playerList.Add(player2);

        if (noOfPlayers >= 3)
        {
            playerList.Add(player3);
        }
        if (noOfPlayers >= 4)
        {
            playerList.Add(player4);
        }
        if (noOfPlayers == 5)
        {
            playerList.Add(player5);
        }
        SetOffsets();
    }

    void SetOffsets()
    {
        //so I can actually tell whats happening and the spheres aren't inside each other - E
        player1.SetOffset(0.0f, 0.0f);
        player2.SetOffset(2.0f, 0.0f);
        player3.SetOffset(-2.0f, 0.0f);
        player4.SetOffset(0.0f, 2.0f);
        player5.SetOffset(0.0f, -2.0f);
    }

    int GetNoOfPlayers()
    {
        return noOfPlayers;
    }

    void SetNoOfPlayers(int n)
    {
        noOfPlayers = n;
    }

    int GetCurrentPlayer()
    {
        return currentPlayer;
    }

    void SetCurrentPlayer(int n)
    {
        currentPlayer = n;
    }

    public void NextPlayer()
    {
        if (currentPlayer >= noOfPlayers)
        {
            //if it's the last player, go back to the first one pls and thanks - E
            SetCurrentPlayer(1);
        }
        else
        {
            SetCurrentPlayer((GetCurrentPlayer() + 1));
        }
        playerList[currentPlayer - 1].SetMoneyText(currentPlayer.ToString());
    }

    public void MovePlayer()
    {
        playerList[currentPlayer - 1].Move(currentRoll);
    }

    public void RollDice()
    {
        //moved the code from MovementController.cs to here in line with the documentation and also having this in a central GSM is easier for me - E
        int d1 = Random.Range(1, 6);
        int d2 = Random.Range(1, 6);
        int diceResult = d1 + d2;
        print(diceResult);
        currentRoll = diceResult;
    }

    public void PurchaseProperty()
    {
        playerList[currentPlayer - 1].PurchaseProperty();
    }
}
