public interface IProjectile
{
    int Damage { get; set; }
    float Speed { get; }
    float LifeTime { get; }

    void Travel();

}
