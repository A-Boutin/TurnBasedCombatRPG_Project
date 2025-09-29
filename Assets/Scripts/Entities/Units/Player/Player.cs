using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Unit
{
    [Space]
    [SerializeField] public Equipment equipment;
    [Space]
    [SerializeField] protected int stamina;
    [Space]
    [SerializeField] public MoveAction moveAction;
    [Space]
    [SerializeField] public int aggro;

    public int Stamina
    {
        get => stamina;
        set => stamina = value;
    }
    private void OnMouseDown()
    {
        Controls.SelectUnit(this);
    }

    public new int Damage(int amount, bool magic)
    {
        Defense defSet = equipment.GetDefense();
        int dmg = amount;
        if (equipment != null && defSet != null)
        {
            if (magic) dmg -= defSet.resist.Roll();
            else dmg -= defSet.block.Roll();
        }
        Mathf.Clamp(dmg, 0, amount);

        health += dmg;
        Mathf.Clamp(health, 0, MaxHealth);

        return health;
    }
}
