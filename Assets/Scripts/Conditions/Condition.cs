using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
    [HideInInspector] private bool isEffectActive;

    public virtual void ApplyEffect(Unit target)
    {
        isEffectActive = true;

    }

    public virtual void UpdateEffect()
    {

    }
    public virtual void RemoveEffect(Unit target)
    {
        isEffectActive = false;
    }

    public virtual bool CanStatusVisualBeRemoved()
    {
        return !(isEffectActive);
    }
}
