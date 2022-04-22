using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject[] spaces;
    public GameObject[] players;
    public int[] spaceStates = new int[40]; //Key: 0 - unowned, 1 to 5 - owned by said player, 6 - special space cannot be buy - E

    void Start()
    {
        spaces = GameObject.FindGameObjectsWithTag("Space");
        players = GameObject.FindGameObjectsWithTag("Player");
        InitialiseSpaceStates();
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
            if ((spaces[i].GetComponent<Space>().GetType() != "PROP") && (spaces[i].GetComponent<Space>().GetType() != "UTIL") && (spaces[i].GetComponent<Space>().GetType() != "STAT"))
            {
                spaceStates[i] = 6;
            }
            else
            {
                spaces[i].GetComponent<Property>().InitialiseRentList(); //see Property.cs if you want something monotonous to do - E
            }
        }
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
}
