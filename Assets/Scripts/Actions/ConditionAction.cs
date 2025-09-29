using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class ConditionAction : Action
{
    [SerializeReference, SubclassSelector] public Condition condition;

    public IEnumerator Perform(Encounter encounter, Unit performer)
    {
        Debug.Log("Condition Action");
        // Check if anyone got Hit from a previous action & if so, apply condition to them
        // Loop through all units currently on the board. If state is Hit or Pushed, then apply condition to them.
        yield return null;
    }
}
