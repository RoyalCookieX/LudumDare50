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
    public float Angle => _angle;
    public int TeamID => _teamID;
    public IReadOnlyList<ItemCost> ItemCost => _towerData.ItemCost;
    
    [SerializeField] private UnityEvent<float> _onHealthUpdated;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private TowerData _towerData;
    [SerializeField] private Transform _projectileStart;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private Sprite _upSprite;
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

        _projectilePool = new ObjectPool<Projectile>(_projectilePrefab, BurstSize * 3 / 2);

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
        OnHealthUpdated();
    }

    public void RemoveHealth(float health)
    {
        _health -= health;
        OnHealthUpdated();
    }

    public void SetHealth(float health)
    {
        _health = health;
        OnHealthUpdated();
    }

    public float GetTargetAngle(Vector2 target)
    {
        Vector2 displacement = target - (Vector2)_projectileStart.position;
        return Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
    }
    
    public bool TryFire(Vector2 target)
    {
        if (!_projectilePrefab || !_towerData || !IsAlive)
            return false;

        float angle = GetTargetAngle(target);

        // check if tower can fire at target
        float minAngle = _angle - FOV / 2.0f;
        float maxAngle = _angle + FOV / 2.0f;
        if (angle < minAngle || angle > maxAngle)
            return false;
        
        // fire at target
        if(_bulletRoutine != null)
            StopCoroutine(_bulletRoutine);
        _bulletRoutine = StartCoroutine(BulletRoutine(angle));
        
        // degrade health
        RemoveHealth(Random.Range(DegradationRange.x, DegradationRange.y + 1));
        return true;
    }

    public void Aim(float angle)
    {
        _angle = angle;

        float radians = angle * Mathf.Deg2Rad;
        bool flipX = (Mathf.Sign(Mathf.Cos(radians)) > 0);
        bool useUp = (Mathf.Sign(Mathf.Sin(radians)) > 0);
        _renderer.flipX = flipX;
        _renderer.sprite = useUp ? _upSprite : _downSprite;
    }

    private void OnHealthUpdated()
    {
        _onHealthUpdated?.Invoke(_health / MaxHealth);
        if (IsAlive)
            return;
        
        Destroy(gameObject);
    }

    private IEnumerator TowerRoutine()
    {
        while (gameObject.activeSelf)
        {
            // try hit first target available
            int targetCount = Physics2D.OverlapCircleNonAlloc(_projectileStart.position, SearchRadius, _targets, _enemyLayerMask);
            for (int i = 0; i < targetCount; i++)
            {
                if (TryFire(_targets[i].transform.position))
                    break;
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
