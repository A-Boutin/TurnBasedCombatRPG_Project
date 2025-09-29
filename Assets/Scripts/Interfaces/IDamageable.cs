public interface IDamageable
{
    int Health { get; set; }
    int MaxHealth { get; set; }
    int Damage(int amount, bool magic);
}
