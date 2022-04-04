using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour, IHealth
{
    public float Health => _health;
    public float MaxHealth => _towerData.MaxHealth;
    public Vector2Int DegradationRange => _towerData.DegradationRange;
    public bool IsAlive => _health > 0.0f;
    public float SearchRadius => _towerData.SearchRadius;
    public float PlacementRadius => _towerData.PlacementRadius;
    public float FOV => _towerData.FOV;
    public float BurstCooldown => _towerData.BurstCooldown;
    public int BurstSize => _towerData.BurstSize;
    public float BulletCooldown => _towerData.BulletCooldown;
    public float Accuracy => _towerData.Accuracy;
    public float Angle { get => _angle; set => _angle = value; }
    public int TeamID => _teamID;
    public IReadOnlyList<ItemCost> ItemCost => _towerData.ItemCost;

    [SerializeField] private UnityEvent<float> _onHealthUpdated;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private TowerData _towerData;
    [SerializeField] private Transform _projectileStart;
    [SerializeField] private float _health;
    [SerializeField] private int _teamID = 2;
    [SerializeField] private float _angle;
    [SerializeField, Min(1)] private int _maxTargets = 10;
    [SerializeField] private LayerMask _enemyLayerMask;
    
    private ObjectPool<Projectile> _projectilePool;
    private Coroutine _towerRoutine;
    private Coroutine _bulletRoutine;
    private Collider2D[] _targets;
    
    private void OnEnable()
    {
        _targets = new Collider2D[_maxTargets];
        SetHealth(MaxHealth);

        _projectilePool = new ObjectPool<Projectile>(_projectilePrefab, BurstSize);

        if(_towerRoutine != null)
            StopCoroutine(_towerRoutine);
        _towerRoutine = StartCoroutine(TowerRoutine());
    }

    private void OnDisable()
    {
        _projectilePool.Free();
    }

    public void AddHealth(float health)
    {
        _health += health;
        _onHealthUpdated?.Invoke(Health / MaxHealth);
    }

    public void RemoveHealth(float health)
    {
        _health -= health;
        _onHealthUpdated?.Invoke(Health / MaxHealth);
    }

    public void SetHealth(float health)
    {
        _health = health;
        _onHealthUpdated?.Invoke(Health / MaxHealth);
    }
    
    public void Fire(Vector2 target)
    {
        if (!_projectilePrefab || !_towerData || !IsAlive)
            return;
        
        Vector2 displacement = target - (Vector2)_projectileStart.position;
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;

        // check if tower can fire at target
        float minAngle = _angle - FOV / 2.0f;
        float maxAngle = _angle + FOV / 2.0f;
        if (angle < minAngle || angle > maxAngle)
            return;
        
        // fire at target
        if(_bulletRoutine != null)
            StopCoroutine(_bulletRoutine);
        _bulletRoutine = StartCoroutine(BulletRoutine(angle));
        
        // degrade health
        RemoveHealth(Random.Range(DegradationRange.x, DegradationRange.y + 1));
    }

    private IEnumerator TowerRoutine()
    {
        while (gameObject.activeSelf)
        {
            // find all targets
            Collider2D target = null;
            var result = Physics2D.OverlapCircleNonAlloc(_projectileStart.position, SearchRadius, _targets, _enemyLayerMask);
            if (result > 0)
            {
                while (target == null)
                    target = _targets[Random.Range(0, _targets.Length)];
                    
                Fire(target.transform.position);
            }
            yield return new WaitForSeconds(BurstCooldown);
        }
        yield return null;
    }

    private IEnumerator BulletRoutine(float angle)
    {
        for (int i = 0; i < BurstSize; i++)
        {
            float scaledAccruacy = (1.0f - Accuracy) * 180.0f;
            float adjustment = Random.Range(-scaledAccruacy, scaledAccruacy);
            Quaternion targetRotation = Quaternion.Euler(0.0f, 0.0f, angle + adjustment);
            _projectilePool.Instantiate(_projectileStart.position, targetRotation);
            yield return new WaitForSeconds(BulletCooldown);
        }
    }

    private void OnDrawGizmos()
    {
        if (!_towerData)
            return;

        if (Mathf.Approximately(FOV, 360.0f))
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(_projectileStart.position, SearchRadius);
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
            Gizmos.DrawRay(_projectileStart.position, minDirection * SearchRadius);
            Gizmos.DrawRay(_projectileStart.position, maxDirection * SearchRadius);
        }
    }
}
