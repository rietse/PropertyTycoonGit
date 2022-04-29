using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{
    public GameManager gm;
    public TextMeshProUGUI winText;
    public GameObject winScreen;

    public void ShowScreen()
    {
        SetWinText();
        winScreen.SetActive(true);
    }

    public void SetWinText()
    {
        winText.text = "Player " + gm.currentPlayer + " is the winner of Property Tycoon!";
    }
}
