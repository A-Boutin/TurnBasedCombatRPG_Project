using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Unit : Entity, IDamageable
{
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;

    [HideInInspector] public bool selectable = false;
    protected Color color;

    public int Health
    {
        get => health;
        set => health = value;
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    [SerializeField] protected int speed;
    public int Speed
    {
        get => speed;
        set => speed = value;
    }

    private void Start()
    {
        color = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void FixedUpdate()
    {
        if (selectable)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnMouseDown()
    {
        Controls.SelectUnit(this);
    }

    public void MoveTo(Node target)
    {
        if (target)
        {
            if (target != GetCurrentPosition())
            {
                List<Node> path = Controls.AStar(GetCurrentPosition(), target);
                if (path.Any())
                {
                    if (path.Count <= Speed)
                    {
                        Map.MoveUnit(target, this);
                    }
                }
            }
        }
    }

    public Node GetCurrentPosition()
    {
        return transform.parent.GetComponent<Node>();
    }

    public int Damage(int amount, bool magic)
    {
        Debug.Log("UNIT DAMAGE: " + amount);

        health -= amount;
        Mathf.Clamp(health, 0, maxHealth);

        return health;
    }
}
