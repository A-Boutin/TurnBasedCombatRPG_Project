using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Card", menuName = "Card/Upgrade Card")]
public class UpgradeCard : BaseCard
{
    [SerializeField] public Slots type;
    // Also Need Logic For Upgrades
    // The following will utilize a (serializable? abstract? interface?) class called Upgrade which will have the implemented functionality of what the card should do
    // will try to make each upgrade as open as possible so I can have similar upgrades of different intensity
    //[SerializeField] public Upgrade upgrade;
}
