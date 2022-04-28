using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PropertyTile : MonoBehaviour
{
    public TextMeshProUGUI tileName;

    public void SetName(string s)
    {
        tileName.text = s;
    }
}
