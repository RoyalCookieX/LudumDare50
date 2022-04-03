using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    
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
}
