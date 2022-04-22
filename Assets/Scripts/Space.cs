using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    public int idNum;
    public enum Type { GO, JAIL, PARK, GOJAIL, POT, OPP, PROP, TAX, STAT, UTIL }
    public Type type;
    public string spaceName;

    public string GetType()
    {
        return type.ToString();
    }
    public string GetName()
    {
        return spaceName;
    }
}
