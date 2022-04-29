using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyHouseManager : MonoBehaviour
{
    public int state = 0;
    public GameObject house1, house2, house3, house4, hotel;

    public Vector3 offset; //used to make sure the houses are in their designated location on the space - E

    void Start()
    {
        UpdateHouses(0); //ensures all houses are hidden at the start of the game - E
    }

    //Sets position of house
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos + offset; ;
    }

    //Updates number of houses on a space
    public void UpdateHouses(int x)
    {
        state = x;
        switch(state)
        {
            case 0:
                HideAll();
                break;
            case 1:
                HideAll();
                house1.SetActive(true);
                break;
            case 2:
                HideAll();
                house1.SetActive(true);
                house2.SetActive(true);
                break;
            case 3:
                HideAll();
                house1.SetActive(true);
                house2.SetActive(true);
                house3.SetActive(true);
                break;
            case 4:
                HideAll();
                house1.SetActive(true);
                house2.SetActive(true);
                house3.SetActive(true);
                house4.SetActive(true);
                break;
            case 5:
                HideAll();
                hotel.SetActive(true);
                break;
        }
    }

    //Removes all houses from a space
    void HideAll()
    {
        house1.SetActive(false);
        house2.SetActive(false);
        house3.SetActive(false);
        house4.SetActive(false);
        hotel.SetActive(false);
    }
}
