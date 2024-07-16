public interface IDamageable
{
    float Life { get; }
    float MaxLife { get; }
    void TakeDamage(int damage);
}
