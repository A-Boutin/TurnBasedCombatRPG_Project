using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Target
{
    Any,
    Nearest,
    Aggro,
}
public interface Action
{
    IEnumerator Perform(Encounter encounter, Unit performer);
}
