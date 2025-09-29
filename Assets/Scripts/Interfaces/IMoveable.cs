using System;
using Unity.VisualScripting;
using UnityEngine;

public interface IMoveable
{
    int Speed { get; set; }
    Node CurrentPosition { get; set; }

    void MoveTo(Node target);
}
