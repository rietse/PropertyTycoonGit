using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject[] spaces;
    public GameObject[] players;
    public List<GameObject> oppCardList;
    public List<GameObject> potCardList;
    public GameManager gameManager;
    public int[] spaceStates = new int[40]; //Key: 0 - unowned, 1 to 5 - owned by said player, 6 - special space cannot be buy
    public BoardSpaceCustomisation boardSpaceCustomisation;
    public CardPopup cardPopup;

    //Initialises all the data used by the board
    void Start()
    {
        spaces = GameObject.FindGameObjectsWithTag("Space");
        players = GameObject.FindGameObjectsWithTag("Player");
        oppCardList = new List<GameObject>(GameObject.FindGameObjectsWithTag("OppCard"));
        potCardList = new List<GameObject>(GameObject.FindGameObjectsWithTag("PotCard"));
        InitialiseSpaceStates();
        oppCardList = ShuffleCards(oppCardList);
        potCardList = ShuffleCards(potCardList);
        boardSpaceCustomisation.InitialisePropertyList();
    }

    //Sets all of the player positions to Go
    public void InitialisePlayerPositions()
    {
        foreach (GameObject p in players)
        {
            p.transform.position = spaces[0].transform.position;
            //Offsets the tokens so they don't intersect each other
            p.GetComponent<PlayerController>().OffsetPlayer();
        }
    }

    //Initialises the space data by setting variables of each instance of the "Space" class
    void InitialiseSpaceStates()
    {
        int price = 0;
        for (int i = 0; i < 40; i++)
        {
            spaces[i].GetComponent<Space>().SetDefaultName();
            spaces[i].GetComponent<Space>().InitialiseText();
            if ((spaces[i].GetComponent<Space>().GetType() != "PROP") && (spaces[i].GetComponent<Space>().GetType() != "UTIL") && (spaces[i].GetComponent<Space>().GetType() != "STAT"))
            {
                spaceStates[i] = 6;
            }
            else
            {
                switch (spaces[i].GetComponent<Space>().GetType())
                {
                    case "PROP":
                        spaces[i].GetComponent<Property>().InitialiseUpgradeCost();
                        spaces[i].GetComponent<Property>().InitialiseHousePositions();
                        price = spaces[i].GetComponent<Property>().GetPrice();
                        break;
                    case "STAT":
                        price = spaces[i].GetComponent<Station>().GetPrice();
                        break;
                    case "UTIL":
                        price = spaces[i].GetComponent<Utility>().GetPrice();
                        break;
                }
                spaces[i].GetComponent<Space>().InitialisePriceText(price);
            }
        }
    }

    List<GameObject> ShuffleCards(List<GameObject> deck)
    { //Fisher-Yates shuffle algorithm, grabs a random object, puts it at the 'end' of the list then repeats until it reaches the start
        List<GameObject> tempDeck = new List<GameObject>(deck);
        deck.Clear();

        int rdmNo, j = 0;
        for (int i = (tempDeck.Count - 1); i >= 0; i--)
        {
            rdmNo = UnityEngine.Random.Range(0, tempDeck.Count);
            deck.Add(tempDeck[rdmNo]);
            j += 1;
            tempDeck.RemoveAt(rdmNo);
        }
        return deck;
    }

    //Removes a card from either list of cards depending on whether it is an Opportunity card or Pot luck and returns the effect of the selected card
    public int[] DrawCard(string c)
    {
        int[] cardEffect = null;
        GameObject card = null;
        switch(c)
        {
            case "OPP":
                card = oppCardList[0];
                oppCardList.RemoveAt(0);
                oppCardList.Add(card);
                break;
            case "POT":
                card = potCardList[0];
                potCardList.RemoveAt(0);
                potCardList.Add(card);
                break;
            default:
                break;
        }

        print(card.GetComponent<Card>().GetDescription());
        cardEffect = card.GetComponent<Card>().GetEffects();
        cardPopup.LatestCard(card);
        return cardEffect;
    }

    //Triggers the UI element representing the chosen card to become active
    public void TriggerLatestCard(bool drawNew)
    {

        if (drawNew == true)
        {
            if(cardPopup.GetNewDraw() == 1)
            {
                DrawCard("OPP");
            }
            else
            {
                DrawCard("POT");
            }
            cardPopup.Popup();
        }
        else
        {
            int[] effect = cardPopup.GetCardEffects();
            gameManager.TriggerCardEffect(effect);
        }
    }

    //Returns whether a new card has been drawn
    public bool CheckDrawNew()
    {
        return cardPopup.ValidNewDraw();
    }

    //Returns a specified integer from the spaceStates array
    public int GetState(int i)
    {
        return spaceStates[i];
    }

    //Changes the value of one specific integer from the spaceStates array
    public void SetState(int i, int state)
    {
        spaceStates[i] = state;
    }

    //Returns a specified Space GameObject from the spaces array
    public GameObject GetSpace(int i)
    {
        return spaces[i];
    }

    //Returns the development level (i.e. number of houses) of a specified property
    public int GetHouses(int i)
    {
        int devLevel = spaces[i].GetComponent<Property>().GetDevelopmentLevel();

        if((devLevel > 0) && (devLevel < 5)) //devLevels 1-4 = the number of houses on the spot which is pretty convenient, 0 or 5 indicates nothing or a hotel so no need to return a value
        {
            return devLevel;
        }
        else
        {
            return 0;
        }
    }

    //Returns a value indicating if a specified property has a hotel or not
    public int GetHotels(int i)
    {
        if (spaces[i].GetComponent<Property>().GetDevelopmentLevel() == 5) //development level 5 is a hotel so we just need to check that
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
