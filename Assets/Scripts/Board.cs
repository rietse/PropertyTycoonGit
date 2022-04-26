using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject[] spaces;
    public GameObject[] players;
    public List<GameObject> oppCardList;
    public List<GameObject> potCardList;
    public int[] spaceStates = new int[40]; //Key: 0 - unowned, 1 to 5 - owned by said player, 6 - special space cannot be buy - E

    void Start()
    {
        spaces = GameObject.FindGameObjectsWithTag("Space");
        players = GameObject.FindGameObjectsWithTag("Player");
        oppCardList = new List<GameObject>(GameObject.FindGameObjectsWithTag("OppCard"));
        potCardList = new List<GameObject>(GameObject.FindGameObjectsWithTag("PotCard"));
        InitialiseSpaceStates();
        oppCardList = ShuffleCards(oppCardList);
        potCardList = ShuffleCards(potCardList);
    }

    public void InitialisePlayerPositions()
    {
        foreach (GameObject p in players)
        {
            p.transform.position = spaces[0].transform.position;
            p.GetComponent<PlayerController>().OffsetPlayer();
        }
    }

    void InitialiseSpaceStates()
    {
        for (int i = 0; i < 40; i++)
        {
            spaces[i].GetComponent<Space>().InitialiseText();
            if ((spaces[i].GetComponent<Space>().GetType() != "PROP") && (spaces[i].GetComponent<Space>().GetType() != "UTIL") && (spaces[i].GetComponent<Space>().GetType() != "STAT"))
            {
                spaceStates[i] = 6;
            }
            else if (spaces[i].GetComponent<Space>().GetType() == "PROP")
            {
                spaces[i].GetComponent<Property>().InitialiseRentList(); //see Property.cs if you want something monotonous to do - E
                spaces[i].GetComponent<Property>().InitialiseUpgradeCost();
                spaces[i].GetComponent<Property>().InitialiseHousePositions();
            }
        }
    }

    List<GameObject> ShuffleCards(List<GameObject> deck)
    { //Fisher-Yates shuffle algorithm, grabs a random object, puts it at the 'end' of the list then repeats until it reaches the start - E
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
        return cardEffect;
    }

    public int GetState(int i)
    {
        return spaceStates[i];
    }

    public void SetState(int i, int state)
    {
        spaceStates[i] = state;
    }

    public GameObject GetSpace(int i)
    {
        return spaces[i];
    }

    public int GetHouses(int i)
    {
        int devLevel = spaces[i].GetComponent<Property>().GetDevelopmentLevel();

        if((devLevel > 0) && (devLevel < 5)) //devLevels 1-4 = the number of houses on the spot which is pretty convenient, 0 or 5 indicates nothing or a hotel so no need to return a value - E
        {
            return devLevel;
        }
        else
        {
            return 0;
        }
    }

    public int GetHotels(int i)
    {
        if (spaces[i].GetComponent<Property>().GetDevelopmentLevel() == 5) //development level 5 is a hotel so we just need to check that - E
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
