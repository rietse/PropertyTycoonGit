using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTest : MonoBehaviour
{
    public BoardTESTONLY board;
    public PlayerControllerTESTONLY player1, player2, player3, player4, player5; //max 5 players, so might as well hook up all these guys - E
    public CameraController cameraController;
    public PropertyDisplay propertyDisplay;
    public int currentPlayer = 1;
    public int noOfPlayers = 0;
    public int freeParking = 0;
    public List<PlayerControllerTESTONLY> playerList = new List<PlayerControllerTESTONLY>();

    private int[] currentCard;
    private int currentRoll;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitialiseGame()
    {
        if (noOfPlayers == 0)
        {
            //get number of players once we can choose how many are gonna play, for now I'm setting it to all 5 so I can test this works, we might also  chuck this somewhere else, it's just here as a reminder to code this lol - E
            noOfPlayers = 5;
        }
        InitialisePlayers();
        board.InitialisePlayerPositions();
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
    }

    void SetOffsets()
    {
        //so I can actually tell whats happening and the spheres aren't inside each other - E
        player1.SetOffset(0.0f, 0.0f);
        player2.SetOffset(4.0f, 0.0f);
        player3.SetOffset(-4.0f, 0.0f);
        player4.SetOffset(0.0f, 4.0f);
        player5.SetOffset(0.0f, -4.0f);
    }

    public int GetFreeParking()
    {
        return freeParking;
    }

    int GetNoOfPlayers()
    {
        return noOfPlayers;
    }

    void SetNoOfPlayers(int n)
    {
        noOfPlayers = n;
    }

    public int GetCurrentPlayer()
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

            if ((playerList[currentPlayer - 1]).IsInJail())
            {
                playerList[currentPlayer - 1].AddJailCounter();
                if (playerList[currentPlayer - 1].GetJailCounter() == 3)
                {
                    playerList[currentPlayer - 1].SetInJail(false);
                    playerList[currentPlayer - 1].SetPos(10);
                    playerList[currentPlayer - 1].ResetJailCounter();
                }
            }

            if (currentPlayer == lastValidPlayer)
            {
                print("Win"); //we need to do win code here eventally, a single line saying "Win" isn't that entertaining - E
                validPlayer = true; //this is just here so I don't get another loop that breaks unity - E
            }
        }
    }

    public void MovePlayer()
    {
        playerList[currentPlayer - 1].Move(currentRoll, false);
        CheckSpace(playerList[currentPlayer - 1].GetPos());
    }

    public void CheckSpace(int pos)
    {
        GameObject space = board.GetSpace(pos);
        string type = space.GetComponent<Space>().GetType();
        int rent = 0;
        int[] cardEffect = null;

        switch (type)
        {
            case "PARK":
                playerList[currentPlayer - 1].RecieveRent(freeParking);
                print("Player " + currentPlayer + " recieved ?" + freeParking + " from free parking!");
                freeParking = 0;
                break;
            case "GOJAIL":
                print("GOJAIL");
                GoToJail();
                break;
            case "POT":
                cardEffect = board.DrawCard("POT");
                break;
            case "OPP":
                cardEffect = board.DrawCard("OPP");
                break;
            case "PROP":
                if ((board.GetState(pos) != 0) && (board.GetState(pos) != currentPlayer))
                {
                    if ((space.GetComponent<Property>().GetMortgaged() == false) && (playerList[board.GetState(pos) - 1].IsInJail() == false))
                    {
                        rent = space.GetComponent<Property>().GetRent();
                        if (CheckUndevelopedMonopoly(board.GetState(pos)) == true) { rent = rent * 2; }
                        print("Player " + currentPlayer + " owes player " + board.GetState(pos) + " ?" + rent + " rent!");
                        playerList[currentPlayer - 1].PayRent(rent);
                        playerList[board.GetState(pos) - 1].RecieveRent(rent);
                    }
                    else print("This space is mortgaged, no rent for you!");

                }
                break;
            case "TAX":
                if (pos == 4) //income tax position - E
                {
                    print("Player " + currentPlayer + " has to pay ?200 in taxes!");
                    playerList[currentPlayer - 1].PayRent(200);
                }
                else if (pos == 38) //super tax position - E
                {
                    print("Player " + currentPlayer + " has to pay ?100 in taxes!");
                    playerList[currentPlayer - 1].PayRent(100);
                }
                break;
            case "STAT":
                if ((board.GetState(pos) != 0) && (board.GetState(pos) != currentPlayer))
                {
                    if (playerList[board.GetState(pos) - 1].IsInJail() == false) {
                        double rentD = 12.5; //since rent is doubled for each station you own, we can be cheeky and start it at half rent as one of the stations must be owned to trigger this, thus moving it to the ?25 figure without any trouble - E
                        if (board.GetState(5) == board.GetState(pos)) { rentD = rentD * 2; }
                        if (board.GetState(15) == board.GetState(pos)) { rentD = rentD * 2; }
                        if (board.GetState(25) == board.GetState(pos)) { rentD = rentD * 2; }
                        if (board.GetState(35) == board.GetState(pos)) { rentD = rentD * 2; }
                        rent = Convert.ToInt32(rentD); //just need to borrow a double because ints don't decimal - E

                        print("Player " + currentPlayer + " owes player " + board.GetState(pos) + " ?" + rent + " rent!");
                        playerList[currentPlayer - 1].PayRent(rent);
                        playerList[board.GetState(pos) - 1].RecieveRent(rent);
                    }
                }
                break;
            case "UTIL":
                if ((board.GetState(pos) != 0) && (board.GetState(pos) != currentPlayer))
                {
                    if (playerList[board.GetState(pos) - 1].IsInJail() == false) {
                        if (board.GetState(12) == board.GetState(28)) //as we know one is owned by a different player, we can just check rather than making sure it's not unowned - E
                        {
                            rent = currentRoll * 10;
                        }
                        else { rent = currentRoll * 4; }

                        print("Player " + currentPlayer + " owes player " + board.GetState(pos) + " ?" + rent + " rent!");
                        playerList[currentPlayer - 1].PayRent(rent);
                        playerList[board.GetState(pos) - 1].RecieveRent(rent);
                    }
                }
                break;
            default:
                break;
        }

        if (cardEffect != null)
        {
            TriggerCardEffect(cardEffect);
        }
    }

    bool CheckUndevelopedMonopoly(int player)
    {
        int pos = playerList[currentPlayer - 1].GetPos();
        if (playerList[currentPlayer - 1].CheckMonopoly(board.GetSpace(pos).GetComponent<Property>().GetColour(), pos) == true) //checks if we have a monopoly first, no point doing the rest otherwise - E
        {
            for (int i = 0; i < 40; i++)
            {
                GameObject space = board.GetSpace(i);
                if (space.GetComponent<Space>().GetName() == "PROP") //checks if the space is a property before doing property unique checks on it - E
                {
                    if (space.GetComponent<Property>().GetDevelopmentLevel() != 0 && space.GetComponent<Property>().GetType() == board.GetSpace(pos).GetComponent<Property>().GetType()) //checks if the dev level isn't 0 and it's a part of the checked monopoly - E
                    {
                        return false;
                    }
                }
            }
        }
        else { return false; }
        return true; //if all checks suceed, we have determined it's an undeveloped monopoly, so double rent! - E
    }

    void TriggerCardEffect(int[] cardEffect)
    {
        int money, moveVal = 0;

        if (cardEffect[0] == 1) //checks if the card lets you draw an OPP card instead of triggering said card - E
        {
            //we actually need to check if the player wants to though, another to add to the "do later" pile - E
            board.DrawCard("OPP");
            print("draw a new OPP card code should trigger?");
        }
        else if (cardEffect[0] == 2) //checks if the card lets you draw a POT card instead of triggering said card - E
        {
            //same as above, do this pls future me - E
            board.DrawCard("POT");
            print("draw a new POT card code should trigger?");
        }
        else //now we know the card effect is actually gonna happen we can go through them and activate them here... ugh so many IF statements this is so bad... - E
        {
            //all effects involving transferring money - E
            if (cardEffect[4] == 1) { money = (playerList[currentPlayer - 1].GetTotalOwnedHouses(currentPlayer) * 40) + (playerList[currentPlayer - 1].GetTotalOwnedHotels(currentPlayer) * 115); } //checks what money value needs to be handled here, so we don't have to figure it out every single transaction - E
            else if (cardEffect[4] == 2) { money = (playerList[currentPlayer - 1].GetTotalOwnedHouses(currentPlayer) * 25) + (playerList[currentPlayer - 1].GetTotalOwnedHotels(currentPlayer) * 100); }
            else { money = cardEffect[5]; } //if it's not using the number of houses or hotels, we default to the regular money value in the card's data - E

            if (cardEffect[1] == 1)
            {
                playerList[currentPlayer - 1].RecieveRent(money);
                print("Player " + currentPlayer + " recieved ?" + money + " from a card!");
                playerList[currentPlayer - 1].SetMoneyText(currentPlayer.ToString());
            }
            if (cardEffect[2] == 1)
            {
                playerList[currentPlayer - 1].PayRent(money);
                print("Player " + currentPlayer + " paid ?" + money + " from a card!");
                playerList[currentPlayer - 1].SetMoneyText(currentPlayer.ToString());
            }
            if (cardEffect[3] == 1)
            {
                freeParking += money;
                print("Player " + currentPlayer + "'s fine of ?" + money + " went to the free parking fund (total: ?" + freeParking + ")!");
            }

            //all movement effects go here - E
            if (cardEffect[7] == 1)
            {
                if (cardEffect[6] == 1) //check if we moving to a specific space... - E
                {
                    moveVal = cardEffect[8] - playerList[currentPlayer - 1].GetPos(); //checks the distance needed to move to the desired location - E
                    if (moveVal <= 0) { moveVal += 40; } //does a loop around if the space is 'behind' the player - E
                    print("Current location: " + playerList[currentPlayer - 1].GetPos() + " Desired Location: " + cardEffect[8] + " Spaces needed to get there: " + moveVal);
                    playerList[currentPlayer - 1].Move(moveVal, true); //override the movement checks to send the player where they need to be as if they moved normally - E
                }
                else if (cardEffect[6] == 2) //...or dynamically based on the current position - E
                {
                    playerList[currentPlayer - 1].Move((playerList[currentPlayer - 1].GetPos() + cardEffect[8]), true); //same as above, but grabs the current position and moves forward based on cardEffect[8], should only be for forwards movement btw - E
                }
            }
            else
            {
                if (cardEffect[6] == 1) //check if we moving to a specific space... - E
                {
                    playerList[currentPlayer - 1].SetPos(cardEffect[8]); //override the movement checks to send the player to a specific space, use for sending players "back" or where they don't collect their GO money - E
                }
                else if (cardEffect[6] == 2) //...or dynamically based on the current position - E
                {
                    moveVal = playerList[currentPlayer - 1].GetPos() + cardEffect[8]; //might as well only calculate this once seeing as we use this value a few times and it looks messy pasting it everywhere - E
                    if (moveVal < 0) //if moving backwards puts us into the negatives (the game doesn't like this at ALL), we can just move forwards instead to find our space) - E
                    {
                        moveVal = 40 + cardEffect[8];
                    }
                    playerList[currentPlayer - 1].SetPos(moveVal); //same as above, but for moving forwards or back x places, usually the latter - E
                }
            }

            //all jail effects go here - E
            if (cardEffect[9] == 1)
            {
                GoToJail();
            }
            if (cardEffect[10] == 1)
            {
                playerList[currentPlayer - 1].RecieveFreeJailCard();
                print("Player " + currentPlayer + " recieved a get out of jail free card!");
            }

            if (cardEffect[11] == 1) //happy birthday card effect my despised. Imagine having to create something just to deal with a single card... - E
            {
                for (int i = 1; i <= noOfPlayers; i++)
                {
                    if (i != currentPlayer)
                    {
                        playerList[i - 1].PayRent(money); //takes birthday money from every other player first... - E
                    }
                }
                playerList[currentPlayer - 1].RecieveRent(money * noOfPlayers); //...then gives the birthday money * the number of players after - E
                playerList[currentPlayer - 1].SetMoneyText(currentPlayer.ToString());
                print("Player " + currentPlayer + " recieved ?" + money + " from each player for their birthday!");
            }
        }
    }

    //TO-DO: right now you can keep rolling infinitely BEFORE moving...i should fix that - R
    //literally why I've been reminding myself in the logs to merge the roll with the move buttons so this can't happen, so instead we have a button the does the roll, then the move, but don't do this until the game is ready to game because it good for testies - E
    //also the player can escape jail the turn after by pressing move as it doesn't do a jail check, I'mma add this in, if it still happens here is a comment to hopefully remind me maybe - E
    public int RollDice()
    {
        //moved the code from MovementController.cs to here in line with the documentation and also having this in a central GSM is easier for me - E
        if (!playerList[currentPlayer - 1].IsInJail())
        {
            if (playerList[currentPlayer - 1].GetReroll())
            {
                playerList[currentPlayer - 1].SetHasMoved(false);
            }
            int d1 = UnityEngine.Random.Range(1, 7);
            int d2 = UnityEngine.Random.Range(1, 7);
            int diceResult = d1 + d2;
            if (d1 == d2)
            {
                playerList[currentPlayer - 1].AddDoublesCounter();
                playerList[currentPlayer - 1].SetReroll(true);
                if (playerList[currentPlayer - 1].GetDoublesCounter() == 3)
                {
                    playerList[currentPlayer - 1].GoToJail();
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
            playerList[currentPlayer - 1].SetHasRolled(true);
            return currentRoll;
        }
        return 0;
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

    public void UpgradeProperty()
    {
        if (playerList[currentPlayer - 1].GetHasPassedGo())
        {
            playerList[currentPlayer - 1].UpgradeProperty(currentPlayer);
        }
    }

    public void DegradeProperty()
    {
        if (playerList[currentPlayer - 1].GetHasPassedGo())
        {
            playerList[currentPlayer - 1].DegradeProperty(currentPlayer);
        }
    }

    public void JailFine()
    {
        // the player can't just pay the fine immediately after being sent, the next time it's their turn they can pay - R
        if (playerList[currentPlayer - 1].GetJailCounter() >= 1)
        {
            if (playerList[currentPlayer - 1].GetMoney() >= 50)
            {
                playerList[currentPlayer - 1].JailFine();
                freeParking += 50;
            }
            else print("Player " + currentPlayer + " cannot afford to leave jail!");
        }
        else print("Player " + currentPlayer + " must wait a turn to leave jail!");
    }

    public void UseJailCard()
    {
        if (playerList[currentPlayer - 1].GetFreeJailCards() > 0)
        {
            playerList[currentPlayer - 1].UseFreeJailCard();
            print("Player " + currentPlayer + " used a get out of jail free card!");
        }
        else print("Player " + currentPlayer + " doesn't have a get out of jail free card!");
    }

    public void GoToJail()
    {
        playerList[currentPlayer - 1].GoToJail();
    }
}
