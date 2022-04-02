using UnityEngine;

public abstract class BaseTower : MonoBehaviour, IHeath
{
    public float Health => _health;
    public float MaxHealth => _towerData.MaxHealth;
    public bool IsAlive => _health > 0.0f;

    [SerializeField] protected BaseProjectile _projectilePrefab;
    [SerializeField] protected TowerData _towerData;
    [SerializeField] protected Transform _projectileStart;
    [SerializeField] protected float _health;

    protected abstract bool IsInRange(Vector2 point);
    
    public void AddHealth(float health)
    {
        _health += health;
    }

    public void RemoveHealth(float health)
    {
        _health -= health;
    }

    public void SetHealth(float health)
    {
        _health = health;
    }

    public void Fire(Vector2 point)
    {
        if (!IsInRange(point))
            return;

        Vector2 displacement = point - (Vector2)transform.position;
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0.0f, 0.0f, angle);
        Instantiate(_projectilePrefab, _projectileStart.position, targetRotation);
    }
}
