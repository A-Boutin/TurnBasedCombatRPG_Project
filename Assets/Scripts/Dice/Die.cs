using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Die
{
    [SerializeField] private string type;
    public string Type => type;
    [SerializeField] private int[] sides;
    public int[] Sides => sides;

    [HideInInspector]
    [SerializeField]
    private string id;
    public string ID => id;

    public void GenerateNewID()
    {
        id = Guid.NewGuid().ToString();
    }

    public Die(string type, int[] sides)
    {
        this.type = type;
        this.sides = sides;
    }

    public int RollDie()
    {
        return sides[UnityEngine.Random.Range(0, sides.Length)];
    }
}
