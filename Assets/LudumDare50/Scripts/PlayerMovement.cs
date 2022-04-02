using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(Mouse.current.position);
    }
}
