using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Vector2 _playerTargetPosition = Vector2.zero;

    public Vector2 PlayerTargetPosition { set => _playerTargetPosition = value; }

    private void FixedUpdate()
    {
        Vector2 playerPosition = gameObject.transform.position;

        if(playerPosition != _playerTargetPosition)
        {
            //TODO: Move to location
            gameObject.transform.position = _playerTargetPosition;
        }
    }
}