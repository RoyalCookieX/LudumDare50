using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerInteraction _playerInteraction;

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

    private void OnInteract(InputValue value)
    {
        if (_playerInteraction.IsPlacingTower)
        {
            _playerInteraction.PlaceTower(value.Get<Vector2>());
        }
        else
        {
            // TODO?: add player collecting resource here
        }
    }
}
