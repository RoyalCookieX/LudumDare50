using System;
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
    private Vector2 _lastInputCursorScreenPosition;

    public Vector2 CursorScreenPosition { set => _lastInputCursorScreenPosition = value; }
    private Camera _mainCamera;
    
    private void Awake()
    {
        _playerTargetPosition = gameObject.transform.position;
        _lastInputCursorScreenPosition = gameObject.transform.position;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _playerTargetPosition, _speed);

    }

    public void SetMoveStartToCurrentLoc()
    {
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(_lastInputCursorScreenPosition);
        Vector3Int gridPosition = _map.WorldToCell(mouseWorldPosition);

        _playerTargetPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, mouseWorldPosition.z + 1);

        Debug.Log(mouseWorldPosition.ToString());

        if (_map.HasTile(gridPosition))
        {
            _playerTargetPosition = mouseWorldPosition;
            Debug.Log(mouseWorldPosition.ToString());
        }
    }
}