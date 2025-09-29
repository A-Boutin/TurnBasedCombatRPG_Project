using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Card", menuName = "Card/Equipment Card")]
public class EquipmentCard : BaseCard
{

    [Serializable]
    public class Action
    {
        [Range(0, 11)][SerializeField] public int staminaCost;
        [SerializeField] public DiceSet dmgSet;
        [Range(-1, 6)][SerializeField] public int range;
        //INCLUDE CONDITION STUFF HERE AS WELL AS OTHER THINGS LIKE ACTUALLY HURTING THE ENEMY/PLAYER LOGIC
    }

    [Space]
    [SerializeField] public List<CardBehaviour> actions;
    [Space]
    [SerializeField] public Slots equipmentSlot;
    [Space]
    [Range(-1,6)][SerializeField] public int range = 0;
    [Space]
    [Range(0, 3)][SerializeField] public int maxUpgrades;
    [SerializeField] public List<UpgradeCard> upgrades;
    [Space]
    [SerializeField] public Defense defense;

    /*
    public Die GetBlockDieData()
    {
        return DiceDatabase.Instance.GetDie(block);
    }
    */

    public void AddUpgrade(UpgradeCard upgrade)
    {
        if(upgrades.Count < maxUpgrades)
        {
            upgrades.Add(upgrade);
        }
        else
        {
            // Allow for swapping of upgrades instead
        }
    }

    public int RollBlock()
    {
        return RollAllDice(defense.block);
    }

    public int RollResist()
    {
        return RollAllDice(defense.resist);
    }

    public int RollDodge()
    {
        return RollAllDice(defense.dodge);
    }

    public int RollAllDice(DiceSet diceSet)
    {
        int total = 0;

        total += diceSet.Roll();

        return total;
    }
}
