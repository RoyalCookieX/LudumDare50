using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private int _gridZ;
    [SerializeField] private Rigidbody2D _rigidbody;

    public void Move(Vector2 worldPosition)
    {
        Vector3Int gridPosition = _tilemap.WorldToCell(worldPosition);
        gridPosition.z = _gridZ;
        if (_tilemap.HasTile(gridPosition))
        {
            _rigidbody.position = worldPosition;
        }
    }
}
