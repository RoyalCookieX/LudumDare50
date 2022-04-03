using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private int _gridZ;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _minDistance;

    private Vector3 _targetPosition;

    private void Update()
    {
        if(((Vector2)_targetPosition - _rigidbody.position).sqrMagnitude > _minDistance * _minDistance)
            _rigidbody.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    public void Move(Vector2 worldPosition)
    {
        Vector3Int gridPosition = _tilemap.WorldToCell(worldPosition);
        gridPosition.z = _gridZ;
        if (_tilemap.HasTile(gridPosition))
        {
            _targetPosition = worldPosition;
        }
    }
}