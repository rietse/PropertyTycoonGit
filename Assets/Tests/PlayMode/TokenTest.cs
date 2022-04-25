using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TokenTest
{

    [UnityTest]
    public IEnumerator TokenTestWithEnumeratorPasses()
    {
        var gameObject = new GameObject();
        var roll = gameObject.AddComponent<GameManager>();
        var player = gameObject.AddComponent<PlayerController>();
         
        yield return null;

      //  Assert.AreEqual(player.GetPos(), gameObject.transform.position);
    }
}
