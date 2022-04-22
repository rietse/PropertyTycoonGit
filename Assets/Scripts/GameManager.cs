using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player1, player2, player3, player4, player5;
    public GameObject activePlayer = null;
    public int currentPlayer = 1;
    public int noOfPlayers = 0;
    public List<GameObject> playerList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (noOfPlayers == 0)
        {
            //get number of players once we can choose how many are gonna play, for now I'm just setting it to two so I can test this works, we are also gonna chuck this somewhere else, it's just here to remind us to code this lol - E
            noOfPlayers = 5;
        }
        InitialisePlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            NextPlayer();
        }
        if (Input.GetKeyDown("w"))
        {
            MovePlayer(RollDice());
        }
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
        player1.GetComponent<PlayerController>().SetOffset(0.0f, 0.0f);
        player2.GetComponent<PlayerController>().SetOffset(2.0f, 0.0f);
        player3.GetComponent<PlayerController>().SetOffset(-2.0f, 0.0f);
        player4.GetComponent<PlayerController>().SetOffset(0.0f, 2.0f);
        player5.GetComponent<PlayerController>().SetOffset(0.0f, -2.0f);
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

    void NextPlayer()
    {
        if (currentPlayer >= noOfPlayers)
        {
            SetCurrentPlayer(1);
        }
        else
        {
            SetCurrentPlayer((GetCurrentPlayer() + 1));
        }
        playerList[currentPlayer - 1].GetComponent<PlayerController>().SetMoneyText(currentPlayer.ToString());
    }

    void MovePlayer(int d)
    {
        playerList[currentPlayer - 1].GetComponent<PlayerController>().Move(d);
    }

    public int RollDice()
    {
        //moved the code from MovementController.cs to here in line with the documentation and also having this in a central GSM is easier for me - E
        int d1 = Random.Range(1, 6);
        int d2 = Random.Range(1, 6);
        int diceResult = d1 + d2;
        print(diceResult);
        return diceResult;
    }
}
