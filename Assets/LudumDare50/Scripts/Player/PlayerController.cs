using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    [Header("Place Turret Scriptable Objects")]
    [SerializeField] private PlaceTurret _radiusTurret;
    [SerializeField] private PlaceTurret _shotgunTurret;

    private Player _player;
    private Camera _mainCamera;
    private Vector2 _cursorPosition;

    private void Start()
    {
        _mainCamera = Camera.main;
        _player = GetComponent<Player>();
    }

    private void OnCursorMove(InputValue value)
    {
        _cursorPosition = value.Get<Vector2>();
    }

    private void OnMove(InputValue value)
    {
        Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(_cursorPosition);
        _playerMovement.Move(worldPosition);
    }

    private void OnInteract()
    {
        Debug.Log("Interacted");
        Debug.Log(_player.IsPlacingTurret);
        if (_player.IsPlacingTurret)
        {
            Debug.Log("It Hates me");
            _player.CoordinatePassthrough(_cursorPosition);
        }
    }
}
