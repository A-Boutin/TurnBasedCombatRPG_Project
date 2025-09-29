using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Behaviour
{
    [SerializeReference, SubclassSelector] public List<Action> actions;
    [Range(1,5)][SerializeField] public int repeats = 1;
}
