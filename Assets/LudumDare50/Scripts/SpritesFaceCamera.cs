using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesFaceCamera : MonoBehaviour
{
    [SerializeField] private bool _flip = false;
    private void Awake()
    {
        Vector3 worldUp;

        if (!_flip) worldUp = Vector3.up;
        else worldUp = -Vector3.up;

        transform.LookAt(Camera.main.transform.position, worldUp);
    }
}
