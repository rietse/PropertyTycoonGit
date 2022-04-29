using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardPopup : MonoBehaviour
{
    public TextMeshProUGUI cardTitle, cardDescription;
    public GameObject cardPanel;
    public GameObject latestCard;

    public void LatestCard(GameObject card)
    {
        latestCard = card;
        DisplayText();
    }

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

    public void Popup()
    {
        cardPanel.SetActive(true);
    }

    public void HidePopup()
    {
        cardPanel.SetActive(false);
    }
}
