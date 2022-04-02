using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IHeath
{
    public float Health => _health;
    public float MaxHealth => _towerData.MaxHealth;
    public bool IsAlive => _health > 0.0f;
    public float Cooldown => _towerData.Cooldown;
    public float Radius => _towerData.Radius;
    public float FOV => _towerData.FOV;
    public int Burst => _towerData.Burst;
    public float Accuracy => _towerData.Accuracy;
    public float Angle { get => _angle; set => _angle = value; }

    [SerializeField] private BaseProjectile _projectilePrefab;
    [SerializeField] private TowerData _towerData;
    [SerializeField] private Transform _projectileStart;
    [SerializeField] private float _health;
    [SerializeField] private float _angle;

    private ObjectPool<BaseProjectile> _projectilePool;
    private Coroutine _current;
    
    private void Start()
    {
        SetHealth(MaxHealth);

        _projectilePool = new ObjectPool<BaseProjectile>(_projectilePrefab, Burst);

        _current = StartCoroutine(FireRoutine());
    }
    
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
        if (angle < minAngle || angle > maxAngle)
            return;
        
        // fire at target
        for (int i = 0; i < Burst; i++)
        {
            float scaledAccruacy = (1.0f - Accuracy) * 180.0f;
            float adjustment = Random.Range(-scaledAccruacy, scaledAccruacy);
            Quaternion targetRotation = Quaternion.Euler(0.0f, 0.0f, angle + adjustment);
            _projectilePool.Instantiate(_projectileStart.position, targetRotation);
        }
    }

    private float GetTargetAngle(Vector2 target)
    {
        Vector2 displacement = target - (Vector2)_projectileStart.position;
        return Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
    }

    private IEnumerator FireRoutine()
    {
        while (gameObject.activeSelf)
        {
            // TODO: Find target
            Fire(Vector2.zero);
            yield return new WaitForSeconds(Cooldown);
            
        }
        yield return null;
    }

    private void OnDrawGizmos()
    {
        if (!_towerData)
            return;

        if (Mathf.Approximately(FOV, 360.0f))
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(_projectileStart.position, Radius);
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
            Gizmos.DrawRay(_projectileStart.position, minDirection * Radius);
            Gizmos.DrawRay(_projectileStart.position, maxDirection * Radius);
        }
        
    }
}
