using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _speed = 1;

    private Vector3 _playerTargetPosition = Vector3.zero;
    private Vector3 _playerMoveStartLoc = Vector3.zero;
    private Vector2 _lastInputCursorScreenPosition = Vector2.zero;
    private float _moveIncrement = 0;
    private float _movementPercentCompletion = 0f;

    public Vector2 CursorScreenPosition { set => _lastInputCursorScreenPosition = value; }

    private void FixedUpdate()
    {
        Vector2 playerPosition = gameObject.transform.position;

        _movementPercentCompletion += _moveIncrement;
        gameObject.transform.position = Vector3.Lerp(_playerMoveStartLoc, _playerTargetPosition, _movementPercentCompletion);

        if (playerPosition != _lastInputCursorScreenPosition)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(_lastInputCursorScreenPosition);

            if (Physics.Raycast(mouseRay, out RaycastHit hit, _groundLayer)) { 
                _playerTargetPosition = hit.point;
                Debug.Log(_playerTargetPosition.ToString());
                _moveIncrement = _speed / (_playerTargetPosition - _playerMoveStartLoc).magnitude;
            }
        }
    }

    public void SetMoveStartToCurrentLoc()
    {
        _playerMoveStartLoc = gameObject.transform.position;
        _movementPercentCompletion = 0;
        _moveIncrement = 0;
    }
}