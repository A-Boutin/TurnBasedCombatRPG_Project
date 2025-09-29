using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stats
{
    [Range(0, 40)][SerializeField] public int strength;
    [Range(0, 40)][SerializeField] public int dextreity;
    [Range(0, 40)][SerializeField] public int intelligence;
    [Range(0, 40)][SerializeField] public int faith;
}
