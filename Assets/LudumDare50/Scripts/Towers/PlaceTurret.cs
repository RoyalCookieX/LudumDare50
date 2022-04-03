using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlaceTurretData",menuName = "PlaceTurret/Turret")]
public class PlaceTurret : ScriptableObject
{
    [SerializeField] private GameObject _turret;
    
    private PlayerMovement _playerMovement;

    public void SetPlayerMovementScript(PlayerMovement pMovement)
    {
        _playerMovement = pMovement;
    }

    public bool Place(Vector2 worldPosition)
    {
        Debug.Log("Placing?");
        Vector3Int gridPosition = _playerMovement.Tilemap.WorldToCell(worldPosition);
        gridPosition.z = _playerMovement.GridZ;
        if (_playerMovement.Tilemap.HasTile(gridPosition))
        {
            Instantiate(_turret, new Vector3(worldPosition.x, worldPosition.y, -1), Quaternion.identity);
            return true;
        }
        else
        {
            return false;
        }
    }
}
