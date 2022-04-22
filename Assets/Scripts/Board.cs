using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject[] spaces;
    public GameObject[] players;

    void Start()
    {
        spaces = GameObject.FindGameObjectsWithTag("Space");
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject p in players)
        {
            p.transform.position = spaces[0].transform.position;
            p.GetComponent<PlayerController>().OffsetPlayer();
        }

    }
}
