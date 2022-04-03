using UnityEngine;

[CreateAssetMenu(fileName = "BaseProjectile", menuName = "Projectiles/BaseProjectile", order = 0)]
public class ProjectileData : ScriptableObject
{
    public float Speed => _speed;
    public float Damage => _damage;

    [SerializeField, Min(0.0f), Tooltip("Speed in units per second")] private float _speed;
    [SerializeField, Min(0.0f), Tooltip("How much damage it deals")] private float _damage;
}