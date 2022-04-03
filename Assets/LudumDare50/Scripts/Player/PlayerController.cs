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

    private void OnInteract(Vector2 mousePosition)
    {
        if (!_player.IsPlacingTurret) return;
        
        else if (_player.IsPlacingTurretType == "Radius") _radiusTurret.Place(mousePosition);
        else if (_player.IsPlacingTurretType == "Shotgun") _shotgunTurret.Place(mousePosition);

    }
}
