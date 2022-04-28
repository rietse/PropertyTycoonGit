using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PropertyDisplay : MonoBehaviour
{
    public Board board;
    public GameManager gameManager;
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI displayTitle;

    public void SetDisplay(GameObject space, int pos)
    {
        string type = space.GetComponent<Space>().GetType();
        string body = "Position: " + (pos + 1) + "\n";
        displayTitle.text = space.GetComponent<Space>().GetName(); //sets title, duh - E

        switch (type) //*slaps switch case* this bad boy can hold so much parsing - E
        {
            case "GO":
                body += "\nPass 'GO' to collect �200, you know the drill.";
                break;
            case "JAIL":
                body += "\nYou don't wanna be here... Pay �50, use a 'Get Out Of Jail Free' Card, or wait for 3 turns to be released!";
                break;
            case "PARK":
                body += "Current Pot: " + gameManager.GetFreeParking() + "\n";
                body += "\nLand here to collect the 'Free Parking' fund! Profit from your friends' misfortune!";
                break;
            case "GOJAIL":
                body += "\nYou know what makes someone a criminal in 'Property Tycoon'? Landing here of course, how could you do such a thing?";
                break;
            case "POT":
                body += "\nLand here to draw a 'Pot Luck Card'! Are you feeling lucky? No? Well too bad, take one anyway!";
                break;
            case "OPP":
                body += "\nLand here to draw an 'Opportunity Knocks Card'! Gamble away your future with the draw of a card!";
                break;
            case "PROP":
                List<int> rent = space.GetComponent<Property>().GetRentList();
                body += "Owned By: " + GetSpaceOwner(pos) + "\n";
                body += "Development Level: " + space.GetComponent<Property>().GetDevelopmentLevel() + "\n\n";
                body += "Cost: �" + space.GetComponent<Property>().GetPrice() + "\n";
                body += "Rent: �" + rent[0] + " (Base)\n";
                body += "              �" + rent[1] + " (1 House)\n";
                body += "              �" + rent[2] + " (2 Houses)\n";
                body += "              �" + rent[3] + " (3 Houses)\n";
                body += "              �" + rent[4] + " (4 Houses)\n";
                body += "              �" + rent[5] + " (Hotel)\n";
                body += "\nBuy, sell, mortgage, or upgrade 'Property' spaces on the board and force your loved ones into bankruptcy with extortionate rent prices! It's the capitalist dream! Players must pass 'GO' once before they can own property.";
                break;
            case "TAX":
                if(pos == 4)
                {
                    body += "Tax: �200\n";
                }
                else
                {
                    body += "Tax: �100\n";
                }
                body += "\nTaxes! There's no tax evasion here, so pay up buddy! Otherwise the taxman will repossess your soul!";
                break;
            case "STAT":
                body += "Owned By: " + GetSpaceOwner(pos) + "\n\n";
                body += "Cost: �" + space.GetComponent<Station>().GetPrice() + "\n";
                body += "Rent: �25 (1 'Station' owned)\n";
                body += "              �50 (2 'Stations' owned)\n";
                body += "              �100 (3 'Stations' owned)\n";
                body += "              �200 (4 'Stations' owned)\n";
                body += "\nPublic transport! Same rules as regular property but without upgrades! Don't look at me, blame the budget cuts!";
                break;
            case "UTIL":
                body += "Owned By: " + GetSpaceOwner(pos) + "\n\n";
                body += "Cost: �" + space.GetComponent<Utility>().GetPrice() + "\n";
                body += "Rent: 4 x dice total (1 'Utility' owned)\n";
                body += "Rent: 10 x dice total (2 'Utilities' owned)\n";
                body += "\nThere's no bills included option here buddy! Actually... it's all bills here!";
                break;
            default:
                print("well, I'm not quite sure how we got here, how does this space not have a type?? - E");
                break;
        }
        displayText.text = body;
    }

    string GetSpaceOwner(int pos)
    {
        switch(board.GetState(pos))
        {
            case 0:
                return "Unowned";
                break;
            case 1:
                return "Player 1";
                break;
            case 2:
                return "Player 2";
                break;
            case 3:
                return "Player 3";
                break;
            case 4:
                return "Player 4";
                break;
            case 5:
                return "Player 5";
                break;
            default:
                print("Somehow this has been called for an unownable position?? - E");
                break;
        }
        return ("GetSpaceOwner(" + pos + ") failed!");
    }
}
