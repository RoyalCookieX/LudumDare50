using System;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    public float Speed => _projectileData.Speed;
    public float Damage => _projectileData.Damage;
    
    [SerializeField] protected ProjectileData _projectileData;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody.AddRelativeForce(Vector2.right * Speed, ForceMode2D.Impulse);
    }
}