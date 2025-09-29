using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] public EnemySO EnemyData;

    private void Start()
    {
        health = EnemyData.health;
        maxHealth = EnemyData.health;
        color = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void OnMouseDown()
    {
        Controls.SelectUnit(this);
    }

    public new int Damage(int amount, bool magic)
    {
        if (magic) amount -= EnemyData.defense.resist.Roll();
        else amount -= EnemyData.defense.block.Roll();

        health -= amount;
        Mathf.Clamp(health, 0, MaxHealth);

        return health;
    }
}
