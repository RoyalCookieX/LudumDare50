using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToObject : MonoBehaviour
{
    [SerializeField] private Transform targetObject;

    void Start()
    {
        gameObject.transform.position = Camera.main.WorldToScreenPoint(targetObject.position);
    }

}
