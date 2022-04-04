using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    enum TowerPlaceState
    {
        None,
        Aim
    }

    [SerializeField] private UnityEvent<float> _onAimTower;
    [SerializeField] private UnityEvent _onEndAimTower;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerInteraction _playerInteraction;

    private Camera _mainCamera;
    private Vector2 _cursorPosition;
    private TowerPlaceState _placeState;
    private Tower _currentTower;

    private void Start()
    {
        _mainCamera = Camera.main;
        _placeState = TowerPlaceState.None;

    }

    private void Update()
    {
        if (_placeState == TowerPlaceState.Aim)
        {
            Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(_cursorPosition);
            float angle = _currentTower.GetTargetAngle(worldPosition);
            _currentTower.Aim(angle);
            _onAimTower?.Invoke(angle);
        }
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
        switch (_placeState)
        {
            case TowerPlaceState.None:
            {
                if (_playerInteraction.IsPlacingTower)
                {
                    _currentTower = _playerInteraction.TryPlaceTower();
                    if (_currentTower == null)
                        return;

                    if (!Mathf.Approximately(_currentTower.FOV, 360.0f))
                    {
                        _placeState = TowerPlaceState.Aim;
                        _playerMovement.CanMove = false;
                        _currentTower.CanFire = false;
                    }
                    else
                    {
                        _currentTower.CanFire = true;
                    }
                }
                else
                {
                    // TODO?: add player collecting resource here
                }
            } break;
            case TowerPlaceState.Aim:
            {
                _placeState = TowerPlaceState.None;
                _playerMovement.CanMove = true;
                _currentTower.CanFire = true;
                _onEndAimTower?.Invoke();
            } break;
        }
    }
}
