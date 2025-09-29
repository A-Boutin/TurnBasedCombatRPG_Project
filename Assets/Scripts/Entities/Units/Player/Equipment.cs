using System;

[Serializable]
public class Equipment
{
    public EquipmentCard Armour;
    public EquipmentCard RightHand;
    public EquipmentCard LeftHand;
    public EquipmentCard Backup;

    public Defense GetDefense()
    {
        Defense defense = new Defense();

        if (Armour) defense.block.AddDiceSet(Armour.defense.block);
        if (RightHand) defense.block.AddDiceSet(RightHand.defense.block);
        if (LeftHand) defense.block.AddDiceSet(LeftHand.defense.block);

        if (Armour) defense.resist.AddDiceSet(Armour.defense.block);
        if (RightHand) defense.resist.AddDiceSet(RightHand.defense.block);
        if (LeftHand) defense.resist.AddDiceSet(LeftHand.defense.block);

        if (Armour) defense.dodge.AddDiceSet(LeftHand.defense.block);
        if (RightHand) defense.dodge.AddDiceSet(LeftHand.defense.block);
        if (LeftHand) defense.dodge.AddDiceSet(LeftHand.defense.block);

        return defense;
    }
}
