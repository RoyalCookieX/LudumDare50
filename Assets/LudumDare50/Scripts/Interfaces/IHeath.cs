public interface IHeath
{
    public float Health { get; }
    public float MaxHealth { get; }
    public bool IsAlive { get; }

    public void AddHealth(float health);
    public void RemoveHealth(float health);
    public void SetHealth(float health);
}
