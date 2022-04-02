using System;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    public float Speed => _projectileData.Speed;
    public float Damage => _projectileData.Damage;
    
    [SerializeField] protected ProjectileData _projectileData;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void OnEnable()
    {
        SetPositionAndRotation(transform.position, transform.rotation);
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    public void SetPositionAndRotation(Vector2 position, Quaternion rotation)
    {
        _rigidbody.position = position;
        _rigidbody.SetRotation(rotation);
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddRelativeForce(Vector2.right * Speed, ForceMode2D.Impulse);
    }
}