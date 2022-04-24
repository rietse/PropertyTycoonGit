using System;
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
        board.InitialisePlayerPositions();
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
        CheckMoney();
        playerList[currentPlayer - 1].SetHasMoved(false);

        int lastValidPlayer = currentPlayer;
        bool validPlayer = false;
        while (validPlayer == false) //checks player is still playing - E
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

            validPlayer = CheckBankrupt();

            if (currentPlayer == lastValidPlayer)
            {
                Debug.Log("Win");
                validPlayer = true; //this is just here so I don't get another loop that breaks unity - E
            }
        }
    }

    public void MovePlayer()
    {
        playerList[currentPlayer - 1].Move(currentRoll);
        CheckSpace(playerList[currentPlayer - 1].GetPos());
    }

    public void CheckSpace(int pos)
    {
        GameObject space = board.GetSpace(pos);
        string type = space.GetComponent<Space>().GetType();
        int rent = 0;

        switch (type)
        {
            case "PARK":
                print("PARK");
                //do free parking here - E
                break;
            case "GOJAIL":
                print("GOJAIL");
                //do crime go jail pls - E
                break;
            case "POT":
                print("POT");
                //do potluck card - E
                break;
            case "OPP":
                print("OPP");
                //do an opportunity - E
                break;
            case "PROP":
                if ((board.GetState(pos) != 0) && (board.GetState(pos) != currentPlayer))
                {
                    rent = space.GetComponent<Property>().GetRent();
                    print("Player " + currentPlayer + " owes player " + board.GetState(pos) + " £" + rent + " rent!");
                    playerList[currentPlayer - 1].PayRent(rent);
                    playerList[board.GetState(pos) - 1].RecieveRent(rent);
                }
                break;
            case "TAX":
                if(pos == 4) //income tax position - E
                {
                    print("Player " + currentPlayer + " has to pay £200 in taxes!");
                    playerList[currentPlayer - 1].PayRent(200);
                }
                else if (pos == 38) //super tax position - E
                {
                    print("Player " + currentPlayer + " has to pay £100 in taxes!");
                    playerList[currentPlayer - 1].PayRent(100);
                }
                break;
            case "STAT":
                if ((board.GetState(pos) != 0) && (board.GetState(pos) != currentPlayer))
                {
                    double rentD = 12.5; //since rent is doubled for each station you own, we can be cheeky and start it at half rent as one of the stations must be owned to trigger this, thus moving it to the £25 figure without any trouble - E
                    if (board.GetState(5) == board.GetState(pos)) { rentD = rentD * 2; }
                    if (board.GetState(15) == board.GetState(pos)) { rentD = rentD * 2; }
                    if (board.GetState(25) == board.GetState(pos)) { rentD = rentD * 2; }
                    if (board.GetState(35) == board.GetState(pos)) { rentD = rentD * 2; }
                    rent = Convert.ToInt32(rentD); //just need to borrow a double because ints don't decimal - E
                        
                    print("Player " + currentPlayer + " owes player " + board.GetState(pos) + " £" + rent + " rent!");
                    playerList[currentPlayer - 1].PayRent(rent);
                    playerList[board.GetState(pos) - 1].RecieveRent(rent);
                }
                break;
            case "UTIL":
                if ((board.GetState(pos) != 0) && (board.GetState(pos) != currentPlayer))
                {
                    if (board.GetState(12) == board.GetState(28)) //as we know one is owned by a different player, we can just check rather than making sure it's not unowned - E
                    {
                        rent = currentRoll * 10;
                    } else { rent = currentRoll * 4; }

                    print("Player " + currentPlayer + " owes player " + board.GetState(pos) + " £" + rent + " rent!");
                    playerList[currentPlayer - 1].PayRent(rent);
                    playerList[board.GetState(pos) - 1].RecieveRent(rent);
                }
                break;
            default:
                break;
        }
    }

    public void RollDice()
    {
        //moved the code from MovementController.cs to here in line with the documentation and also having this in a central GSM is easier for me - E
        int d1 = UnityEngine.Random.Range(1, 6);
        int d2 = UnityEngine.Random.Range(1, 6);
        int diceResult = d1 + d2;
        if (d1 == d2)
        {
            playerList[currentPlayer - 1].AddDoublesCounter();
            playerList[currentPlayer - 1].SetReroll(true);
            if (playerList[currentPlayer - 1].GetDoublesCounter() == 3)
            {
                playerList[currentPlayer - 1].transform.position = board.spaces[10].transform.position;
                playerList[currentPlayer - 1].SetReroll(false);
                playerList[currentPlayer - 1].ResetDoublesCounter();
            }
        }
        else
        {
            playerList[currentPlayer - 1].SetReroll(false);
            playerList[currentPlayer - 1].ResetDoublesCounter();
        }
        print(d1.ToString() + ", " + d2.ToString() + " pls merge this with the move button at some point future me, thanks - E");
        currentRoll = diceResult;
    }

    private void CheckMoney()
    {
        int money = playerList[currentPlayer - 1].GetMoney();
        if (money <= 0)
        {
            Bankrupt();
        }
    }

    public bool CheckBankrupt()
    {
        return !(playerList[currentPlayer - 1].GetBankrupt());
    }

    public void Bankrupt()
    {
        playerList[currentPlayer - 1].SetBankrupt();
        print("player " + currentPlayer + " has gone bankrupt");
        NextPlayer();
    }

    public void PurchaseProperty()
    {
        if (playerList[currentPlayer - 1].GetHasPassedGo())
        {
            playerList[currentPlayer - 1].PurchaseProperty(currentPlayer);
        }
    }

    public void SellProperty()
    {
        playerList[currentPlayer - 1].SellProperty(currentPlayer);
    }

    public void MortgageProperty()
    {
        playerList[currentPlayer - 1].MortgageProperty(currentPlayer);
    }
}
