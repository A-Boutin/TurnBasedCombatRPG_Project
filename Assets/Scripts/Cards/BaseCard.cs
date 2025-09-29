using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Base Card")]
public class BaseCard : ScriptableObject
{
    public enum Slots
    {
        None = 0,         // 000
        OneHanded = 1,    // 001
        TwoHanded = 2,    // 010
        Weapon = 3,       // 011
        Armour = 4,       // 100
    }

    /*
    [Serializable]
    public class Action
    {
        [Range(0, 11)][SerializeField] public int staminaCost;
        [SerializeField] public DiceSet dmgSet;
        [Range(-1, 6)][SerializeField] public int range;
        //INCLUDE CONDITION STUFF HERE AS WELL AS OTHER THINGS LIKE ACTUALLY HURTING THE ENEMY/PLAYER LOGIC
    }
    */

    [SerializeField] public string cardName;
    [Space]
    [TextArea][SerializeField] public string description;
    //public Image illustration;
    [SerializeField] public Stats statRequirement;
}
