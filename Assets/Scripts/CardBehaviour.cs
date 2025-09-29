using System;
using UnityEngine;

[Serializable]
public class CardBehaviour
{
    [Range(0,10)][SerializeField] public int staminaCost;
    [SerializeField] public Behaviour behaviour;
}
