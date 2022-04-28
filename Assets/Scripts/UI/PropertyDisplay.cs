using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PropertyDisplay : MonoBehaviour
{
    public Board board;
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI displayTitle;

    public void SetDisplay(GameObject space)
    {
        string type = space.GetComponent<Space>().GetType();
        string body = "";
        displayTitle.text = space.GetComponent<Space>().GetName(); //sets title, duh - E

        switch (type) //*slaps switch case* this bad boy can hold so much parsing - E
        {
            case "GO":
                body = "Pass 'GO' to collect £200, you know the drill.";
                break;
            case "JAIL":
                body = "You don't wanna be here... Pay £50, use a 'Get Out Of Jail Free' Card, or wait for 3 turns to be released!";
                break;
            case "PARK":
                body = "Land here to collect the 'Free Parking' fund! Profit from your friends' misfortune!";
                break;
            case "GOJAIL":
                body = "You know what makes someone a criminal in 'Property Tycoon'? Landing here of course, how could you do such a thing?";
                break;
            case "POT":
                body = "Land here to draw a 'Pot Luck Card'! Are you feeling lucky? No? Well too bad, take one anyway!";
                break;
            case "OPP":
                body = "Land here to draw an 'Opportunity Knocks Card'! Gamble away your future with the draw of a card!";
                break;
            case "PROP":
                body = "Buy, sell, mortgage, or upgrade 'Property' spaces on the board and force your loved ones into bankruptcy with extortionate rent prices! It's the capitalist dream! Players must pass 'GO' once before they can own property.";
                break;
            case "TAX":
                body = "Taxes! There's no tax evasion here, so pay up buddy! Otherwise the taxman will repossess your soul!";
                break;
            case "STAT":
                body = "Public transport! Same rules as regular property but without upgrades! Don't look at me, blame the budget cuts!";
                break;
            case "UTIL":
                body = "There's no bills included option here buddy! Actually... it's all bills here!";
                break;
            default:
                print("well, I'm not quite sure how we got here, how does this space not have a type?? - E");
                break;
        }
        displayText.text = body;
    }
}
