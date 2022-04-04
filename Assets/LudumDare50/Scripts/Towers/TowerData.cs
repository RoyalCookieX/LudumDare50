using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemCost
{
    public string Name => _name;
    public int Count => _count;
    
    [SerializeField] private string _name;
    [SerializeField, Min(1)] private int _count;
}

[CreateAssetMenu(fileName = "BaseTower", menuName = "Towers/BaseTower", order = 0)]
public class TowerData : ScriptableObject
{
    public float MaxHealth => _maxHealth;
    public Vector2Int DegradationRange => _degradationRange;
    public float SearchRadius => _searchRadius;
    public float PlacementRadius => _placementRadius;
    public float FOV => _fov;
    public float BurstCooldown => _burstCooldown;
    public int BurstSize => _burstSize;
    public float BulletCooldown => _bulletCooldown;
    public float Accuracy => _accuracy;
    public IReadOnlyList<ItemCost> ItemCost => _itemCost;

    [Header("Health")]
    [SerializeField, Min(0.0f)] private float _maxHealth;
    [SerializeField] private Vector2Int _degradationRange;
    [Header("Fire")]
    [SerializeField, Min(0.0f)] private float _searchRadius;
    [SerializeField, Min(0.0f)] private float _placementRadius;
    [SerializeField, Range(0.0f, 360.0f)] private float _fov;
    [SerializeField, Min(0.0f)] private float _burstCooldown = 1.0f;
    [SerializeField, Min(1)] private int _burstSize = 1;
    [SerializeField, Min(0.0f)] private float _bulletCooldown;
    [SerializeField, Range(0.0f, 1.0f)] private float _accuracy = 1.0f;
    [Header("Cost")]
    [SerializeField] private List<ItemCost> _itemCost;
}