using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardPopup : MonoBehaviour
{
    public TextMeshProUGUI cardTitle, cardDescription;
    public GameObject cardPanel;
    public GameObject latestCard;

    //Updates latest card pulled and updates text
    public void LatestCard(GameObject card)
    {
        latestCard = card;
        DisplayText();
    }

    //Updates card UI text
    void DisplayText()
    {
        if (latestCard.GetComponent<Card>().GetType() == "OPP")
        {
            cardTitle.text = "Opportunity Knocks!";
        }
        else
        {
            cardTitle.text = "Pot Luck!";
        }

        cardDescription.text = latestCard.GetComponent<Card>().GetDescription();
    }

    //Sets card popup window to be active
    public void Popup()
    {
        cardPanel.SetActive(true);
    }

    //Sets card popup window to be disabled
    public void HidePopup()
    {
        cardPanel.SetActive(false);
    }

    public int[] GetCardEffects()
    {
        return latestCard.GetComponent<Card>().GetEffects();
    }

    public GameObject GetCard()
    {
        return latestCard;
    }

    //Checks if new draw is valid
    public bool ValidNewDraw()
    {
        if (GetNewDraw() == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public int GetNewDraw()
    {
        return (latestCard.GetComponent<Card>().GetEffects())[0];
    }
}
