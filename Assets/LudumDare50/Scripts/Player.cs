using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Player : MonoBehaviour
{
    [SerializeField] private Tilemap _map;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _speed = 1;

    private Vector3 _playerTargetPosition;
    private Vector3 _playerMoveStartLoc;
    private Vector2 _lastInputCursorScreenPosition;
    private float _moveIncrement = 0;
    private float _movementPercentCompletion = 0f;

    public Vector2 CursorScreenPosition { set => _lastInputCursorScreenPosition = value; }

    private void Awake()
    {
        _playerMoveStartLoc = gameObject.transform.position;
        _playerTargetPosition = gameObject.transform.position;
        _lastInputCursorScreenPosition = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(gameObject.transform.position, _playerTargetPosition) > 0.1f)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = (_playerTargetPosition - gameObject.transform.position).normalized * _speed;
        }
        else gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void SetMoveStartToCurrentLoc()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(_lastInputCursorScreenPosition);
        Vector3Int gridPosition = _map.WorldToCell(mouseWorldPosition);

        if (_map.HasTile(gridPosition))
        {
            _playerTargetPosition = mouseWorldPosition;
        }
    }
}