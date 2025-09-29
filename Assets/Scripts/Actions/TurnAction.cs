using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class TurnAction : Action
{
    [SerializeField] public int turnAmount;

    public IEnumerator Perform(Encounter encounter, Unit performer)
    {
        Debug.Log("Turn Action");
        // turn amount
        // 1 * 90 = 90 turn right;
        // -1 * 90 = 90 turn left;
        // 2 * 90 = 180 turn right;
        yield return null;
    }
}
