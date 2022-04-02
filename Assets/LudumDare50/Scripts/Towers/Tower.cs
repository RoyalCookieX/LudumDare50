using System;
using UnityEngine;

public class Tower : MonoBehaviour, IHeath
{
    public float Health => _health;
    public float MaxHealth => _towerData.MaxHealth;
    public bool IsAlive => _health > 0.0f;
    public float Radius => _towerData.Radius;
    public float FOV => _towerData.FOV;
    public float Angle { get => _angle; set => _angle = value; }

    [SerializeField] private BaseProjectile _projectilePrefab;
    [SerializeField] private TowerData _towerData;
    [SerializeField] private Transform _projectileStart;
    [SerializeField] private float _health;
    [SerializeField] private float _angle;

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
    
    public void Fire(Vector2 target)
    {
        if (!_projectilePrefab || !_towerData || !IsAlive)
            return;
        
        float angle = GetTargetAngle(target);
        
        // check if tower can fire at target
        float minAngle = _angle - FOV / 2.0f;
        float maxAngle = _angle + FOV / 2.0f;
        if (angle < minAngle && angle > maxAngle)
            return;
        
        // fire at target
        Quaternion targetRotation = Quaternion.Euler(0.0f, 0.0f, angle);
        Instantiate(_projectilePrefab, _projectileStart.position, targetRotation);
    }

    private float GetTargetAngle(Vector2 target)
    {
        Vector2 displacement = target - (Vector2)transform.position;
        return Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
    }

    private void OnDrawGizmos()
    {
        if (!_towerData)
            return;

        if (Mathf.Approximately(FOV, 180.0f))
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
        else
        {
            Gizmos.color = Color.cyan;
            float minAngle = _angle - FOV / 2.0f;
            float maxAngle = _angle + FOV / 2.0f;
            Quaternion minRotation = Quaternion.Euler(Vector3.forward * minAngle);
            Quaternion maxRotation = Quaternion.Euler(Vector3.forward * maxAngle);
            Vector2 minDirection = minRotation * Vector2.right;
            Vector2 maxDirection = maxRotation * Vector2.right;
            Gizmos.DrawRay(transform.position, minDirection * Radius);
            Gizmos.DrawRay(transform.position, maxDirection * Radius);
        }
        
    }
}
