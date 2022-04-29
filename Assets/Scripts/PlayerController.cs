using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Board board;
    private Vector3 offset = new Vector3(0.0f,0.0f,0.0f);
    private Vector3 jailOffset = new Vector3(6.0f, 0.0f, -6.0f);

    public List<GameObject> model = new List<GameObject>();
    public int currentModel;

    private string currPlayerNo = "1";
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
    public bool inJail = false;
    public int jailCounter = 0;

    //Initialises player money
    void Start()
    {
        currentMoney = 1500;
        SetMoneyText(currPlayerNo);
    }

    //Disables the selected player token model
    void DisableModel()
    {
        model[currentModel].SetActive(false);
    }

    //Activates a different model depending on specified ID number
    public void SetActiveModel(int x)
    {
        model[currentModel].SetActive(false);
        currentModel = x;
        model[x].SetActive(true);
    }

    //Returns ID of the current player positon
    public int GetPos()
    {
        return currentPos;
    }

    //Changes the current player position depending on specified ID number
    public void SetPos(int x)
    {
        currentPos = x;
        transform.position = board.spaces[x].transform.position + offset;
    }

    //Returns player money
    public int GetMoney()
    {
        return currentMoney;
    }

    //Removes rent money from payee
    public void PayRent(int rent)
    {
        currentMoney = currentMoney - rent;
        SetMoneyText(currPlayerNo);
    }

    //Adds rent money to reciever
    public void RecieveRent(int rent)
    {
        currentMoney = currentMoney + rent;
    }

    //Sets bankrupt bool and disables bankrupt player model
    public void SetBankrupt()
    {
        isBankrupt = true;
        DisableModel();
    }

    //Returns whether player is bankrupt or not
    public bool GetBankrupt()
    {
        return isBankrupt;
    }

    //Sets player model offset so models don't intersect
    public void SetOffset(float x, float z)
    {
        offset.x = x;
        offset.z = z;
    }

    //Applies offset to character model
    public void OffsetPlayer()
    {
        transform.position += offset;
    }

    //Updates UI text displaying player money
    public void SetMoneyText(string playerNo)
    {
        currPlayerNo = playerNo;
        moneyText.text = "Player " + currPlayerNo + " Current Money: £" + currentMoney.ToString();
    }

    //Changes has rolled bool depending on specified bool value
    public void SetHasRolled(bool rolled)
    {
        hasRolled = rolled;
    }

    //Returns bool value of has rolled
    public bool GetHasRolled()
    {
        return hasRolled;
    }

    //Changes has moved bool depending on specified bool value
    public void SetHasMoved(bool moved)
    {
        hasMoved = moved;
    }

    //Returns bool value of has moved
    public bool GetHasMoved()
    {
        return hasMoved;
    }

    //Changes reroll bool depending on specified bool value
    public void SetReroll(bool reroll)
    {
        canReroll = reroll;
    }

    //returns bool value of can reroll
    public bool GetReroll()
    {
        return canReroll;
    }

    //Initialises doubles counter
    public void AddDoublesCounter()
    {
        doublesCounter += 1;
    }

    //Returns int value of doubles counter
    public int GetDoublesCounter()
    {
        return doublesCounter;
    }

    //Resets value of doubles counter to 0
    public void ResetDoublesCounter()
    {
        doublesCounter = 0;
    }

    //Moves current player's position
    public void Move(int d, bool overrideMove)
    {
        if ((!IsInJail()) && (((!GetHasMoved()) && GetHasRolled()) || (overrideMove == true)))
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
            SetHasMoved(true);
            if (GetReroll()) { 
               SetHasRolled(false); 
            }
            RotatePlayer();
        }
    }

    //Rotates player depending on board location
    void RotatePlayer()
    {
        float r = 0.0f;
        if (currentPos == 0) { r = 315.0f; } //player actually rotates around the board based on location - E
        else if (currentPos > 0 && currentPos <= 9) { r = 0.0f; }
        else if (currentPos == 10) { r = 45.0f; }
        else if (currentPos > 10 && currentPos <= 19) { r = 90.0f; }
        else if (currentPos == 20) { r = 135.0f; }
        else if (currentPos > 20 && currentPos <= 29) { r = 180.0f; }
        else if (currentPos == 30) { r = 225.0f; }
        else if (currentPos > 30 && currentPos <= 39) { r = 270.0f; }

        Quaternion rotation = Quaternion.Euler(0.0f, r, 0.0f);
        print("rotate val: " + r);
        transform.rotation = rotation;
    }

    //Changes has passed go value depending on specified bool value
    public void SetHasPassedGo(bool passed)
    {
        hasPassedGo = passed;
    }

    //Returns bool has passed go
    public bool GetHasPassedGo()
    {
        return hasPassedGo;
    }

    //Triggers the game rule effects of passing go for the first time
    public void PassGo()
    {
        currentMoney += 200;
        print("Player " + currPlayerNo + " has passed GO!");
        SetMoneyText(currPlayerNo);
        SetHasPassedGo(true);
    }

    //Sets the current property to be owned by the current player and removes the property price from player money
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
            else if (hasMoved == false)
            {
                print("Player " + player + " cannot buy a property before they move!");
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

    //Sets the selected property to not be owned by the current player and adds selling price of property to player money
    public void SellProperty(int player, int pos)
    {
        currPlayerNo = player.ToString(); //might as well keep this updated ig - E

        GameObject property = board.GetSpace(pos); //grabs the space - E
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
        if (board.GetState(pos) == player && CheckUndeveloped(property) == true)
        {
            print("Player " + player + " has sold " + property.GetComponent<Space>().GetName());
            currentMoney = currentMoney + price;
            board.SetState(pos, 0); //assigns board space back to unowned and gives money - E
            SetMoneyText(currPlayerNo);
        }
        else print("Can't sell the space " + pos + "!");
    }

    //Sets the selected property to be mortgaged
    public void MortgageProperty(int player, int pos)
    {
        currPlayerNo = player.ToString(); //might as well keep this updated ig - E

        GameObject property = board.GetSpace(pos); //grabs the space - E
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

        if (board.GetState(pos) == player && CheckUndeveloped(property) == true)
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
            print("Can't mortgame/unmortgage space " + pos + "!");
        }
    }

    //Checks if a property has been developed
    bool CheckUndeveloped(GameObject s)
    {
        if (s.GetComponent<Space>().GetType() == "PROP")
        {
            if (s.GetComponent<Property>().GetDevelopmentLevel() > 0) return false; //checks if the property has any houses/hotels on it - E
        }
        return true;
    }

    //Upgrades the development level of a property
    public void UpgradeProperty(int player, int pos)
    {
        currPlayerNo = player.ToString(); //might as well keep this updated ig - E

        GameObject property = board.GetSpace(pos); //grabs the space - E
        int price = 0;

        if (CheckMonopoly(property.GetComponent<Property>().GetColour(), pos) == true)
        {
            if (property.GetComponent<Space>().GetType() == "PROP" && board.GetState(pos) == player && property.GetComponent<Property>().GetDevelopmentLevel() < 5) //checks which space it is, if it can be upgraded, and if it's owned by the player - E
            {
                price = property.GetComponent<Property>().GetUpgradeCost();
                if (price > currentMoney)
                {
                    print("Not enough money!");
                }
                else if (hasMoved == false)
                {
                    print("Player " + player + " cannot upgrade a property before they move!"); //also need to set up code to check which phase of the turn a player is in, as the spec says upgrades/downgrades cannot happen until movement and buying/selling property does - E
                }
                else
                {
                    currentMoney = currentMoney - price;
                    property.GetComponent<Property>().UpgradeProperty();
                    SetMoneyText(currPlayerNo);
                    print("Player " + player + " has upgraded " + property.GetComponent<Space>().GetName() + " to development level " + property.GetComponent<Property>().GetDevelopmentLevel());
                }
            }
            else
            {
                print("Can't upgrade this space!");
            }
        }
    }

    //Downgrades the development level of a property
    public void DegradeProperty(int player, int pos)
    {
        currPlayerNo = player.ToString(); //might as well keep this updated ig - E

        GameObject property = board.GetSpace(pos); //grabs the space - E
        int price = 0;

        if (property.GetComponent<Space>().GetType() == "PROP" && board.GetState(pos) == player && property.GetComponent<Property>().GetDevelopmentLevel() > 0) //checks which space it is, if it's got a house/hotel on it, and if it's owned by the player - E
        {
            price = property.GetComponent<Property>().GetUpgradeCost();
            if (hasMoved == true)
            {
                currentMoney = currentMoney + price;
                property.GetComponent<Property>().DegradeProperty();
                SetMoneyText(currPlayerNo);
                print("Player " + player + " has degraded " + property.GetComponent<Space>().GetName() + " to development level " + property.GetComponent<Property>().GetDevelopmentLevel());
            } else print("Player " + player + " cannot upgrade a property before they move!");
        }
        else
        {
            print("Can't degrade this space!");
        }
    }

    //Checks if a player has Monopoly
    public bool CheckMonopoly(string colour, int pos)
    {
        for (int i = 0; i < 40; i++)
        {
            if (board.GetSpace(i).GetComponent<Space>().GetType() == "PROP") //checks if property so unity doesn't get funny about calling property methods in non property spaces - E
            {
                if ((board.GetSpace(i).GetComponent<Property>().GetColour() == colour) && (board.GetState(i) != board.GetState(pos))) //checks if each space is the same colour AND not owned by the current player, if any spaces fit this criteria, we don't have a monopoly on the colour - E
                {
                    print("Player " + currPlayerNo + "'s Monopoly check on " + colour + " failed!");
                    return false;
                }
            }
        }
        print("Player " + currPlayerNo + "'s Monopoly check on " + colour + " suceeded!");
        return true;
    }

    //Returns the total number of houses owned by a player
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

    //Returns the total number of hotels owned by a player
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

    //Gives the player a get out of jail free card
    public void RecieveFreeJailCard()
    {
        jailFreeCards += 1;
    }

    //Uses a get out of jail free card
    public void UseFreeJailCard()
    {
        LeaveJail();
        jailFreeCards -= 1;
    }

    //Returns any get out of jail free cards
    public int GetFreeJailCards()
    {
        return jailFreeCards;
    }

    //Sets the in jail bool to specified bool value
    public void SetInJail(bool jailed)
    {
        inJail = jailed;
    }

    //returns bool value of in jail
    public bool IsInJail()
    {
        return inJail;
    }

    //Increments jail counter
    public void AddJailCounter()
    {
        jailCounter += 1;
    }

    //Resets jail counter to 0
    public void ResetJailCounter()
    {
        jailCounter = 0;
    }

    //Returns int value of jail counter
    public int GetJailCounter()
    {
        return jailCounter;
    }

    //Sends player to jail
    public void GoToJail()
    {
        SetPos(10);
        transform.position = board.spaces[10].transform.position + offset + jailOffset;
        currentPos = 10;
        hasMoved = true;
        inJail = true;
    }

    //Removes money from player as a jail fine
    public void JailFine()
    {
        currentMoney -= 50;
        SetMoneyText(currPlayerNo);
        LeaveJail();
    }

    //Removes player from jail
    void LeaveJail()
    {
        inJail = false;
        hasMoved = false;
        transform.position = board.spaces[10].transform.position + offset;
        jailCounter = 0;
    }
}
