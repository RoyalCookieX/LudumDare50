using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool IsPlacingTower => _towerPrefab != null;
    
    [SerializeField] private List<Tower> _towerPrefabs;
    [SerializeField] private LayerMask _towerLayerMask;

    private Tower _towerPrefab;
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    public bool TryGetTower(string towerPrefabName)
    {
        Tower towerPrefab = _towerPrefabs.Find(tower => tower.name == towerPrefabName);
        if (!towerPrefab)
            return false;

        // TODO: check/update inventory if you can place the tower; return false the player can't buy the tower
        // TODO: use _towerPrefab.ItemCost to get the cost of the tower

        _towerPrefab = towerPrefab;
        return true;
    }

    public Tower TryPlaceTower()
    {
        if (!IsPlacingTower)
            return null;

        Collider2D coll = Physics2D.OverlapCircle(transform.position, _towerPrefab.PlacementRadius, _towerLayerMask);
        if (coll)
            return null;
        
        Tower instance = Instantiate(_towerPrefab, transform.position, Quaternion.identity);

        _towerPrefab = null;
        return instance;
    }
    
    
    private void OnDrawGizmos()
    {
        if (!IsPlacingTower)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _towerPrefab.PlacementRadius);
    }
}