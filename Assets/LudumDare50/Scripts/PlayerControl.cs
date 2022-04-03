using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableLayers;

    private Player _player;
    private Vector2 _mousePosition;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void OnCursorMove(InputValue input)
    {
        _mousePosition = input.Get<Vector2>();
    }

    public void OnMove(InputValue input)
    {
        _player.CursorScreenPosition = _mousePosition;
        _player.SetMoveStartToCurrentLoc();
        Debug.Log("Move Bitch");
    }

    public void OnInteract(InputValue input)
    {
        Debug.Log("Something Happened");
    }

    void Update()
    {
        
    }
}
