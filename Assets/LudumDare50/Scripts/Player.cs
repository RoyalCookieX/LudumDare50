using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    private Vector2 _playerTargetPosition = Vector2.zero;

    public Vector2 PlayerTargetPosition { set => _playerTargetPosition = value; }

    private void FixedUpdate()
    {
        Vector2 playerPosition = gameObject.transform.position;

        if (playerPosition != _playerTargetPosition)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(_playerTargetPosition);

            if (Physics.Raycast(mouseRay, out RaycastHit hit, _groundLayer)) { 
                gameObject.transform.position = hit.point;
            }
        }
    }
}