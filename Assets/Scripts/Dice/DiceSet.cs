using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class DiceSet
{
    [DiceIDSelection][SerializeField] public List<string> dice;
    [Range(-10, 10)][SerializeField] public int modifier = 0;

    public DiceSet()
    {
        dice = new List<string>();
    }

    public int Roll()
    {
        return RollDice() + modifier;
    }

    public int RollDice()
    {
        int total = 0;

        if (dice.Any())
        {
            foreach (string d in dice)
            {
                Die die = DiceDatabase.Instance.GetDie(d);
                total += die.RollDie();
            }
        }

        return total;
    }

    public void AddDiceSet(DiceSet die)
    {
        dice.AddRange(die.dice);
        modifier += die.modifier;
    }
}
