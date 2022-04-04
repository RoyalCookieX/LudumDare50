using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToObject : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private Vector2 Offset;

    public void Go()
    {
        gameObject.transform.position = Camera.main.WorldToScreenPoint(targetObject.position);
        gameObject.transform.position += new Vector3(Offset.x, Offset.y, 0f);
    }

}
