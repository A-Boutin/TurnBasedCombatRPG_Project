using System;
using UnityEngine;

[Serializable]
public class Defense
{
    [SerializeField] public DiceSet block;
    [Space]
    [SerializeField] public DiceSet resist;
    [Space]
    [SerializeField] public DiceSet dodge;

    public Defense()
    {
        block = new DiceSet();
        resist = new DiceSet();
        dodge = new DiceSet();
    }
}
