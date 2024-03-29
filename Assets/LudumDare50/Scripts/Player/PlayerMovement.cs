using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public bool CanMove { get => _canMove; set => _canMove = value; }

    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private int _gridZ;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _minDistance;
    [SerializeField] private ParticleSystem _dust;
    [SerializeField] private bool _canMove;

    public Tilemap Tilemap { get { return _tilemap; } }
    public int GridZ { get { return _gridZ; } }

    private Vector3 _targetPosition;

    private void Awake()
    {
        _targetPosition = transform.position;
        _canMove = true;
    }

    private void FixedUpdate()
    {
        if(((Vector2)_targetPosition - _rigidbody.position).sqrMagnitude > _minDistance * _minDistance)
            _rigidbody.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    public void Move(Vector2 worldPosition)
    {
        if (!_canMove)
            return;
        
        Vector3Int gridPosition = _tilemap.WorldToCell(worldPosition);
        gridPosition.z = _gridZ;
        if (_tilemap.HasTile(gridPosition))
        {
            _targetPosition = worldPosition;

            // play dust effect when player moves
            DustParticles();
        }
    }

    void DustParticles()
    {
        _dust?.Play();
    }
}
