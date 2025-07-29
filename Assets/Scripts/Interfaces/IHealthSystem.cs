public interface IHealthSystem
{
    float CurrentHealth { get; }
    void TakeDamage(float amount);
}