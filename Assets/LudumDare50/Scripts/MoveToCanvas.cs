using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCanvas : MonoBehaviour
{
    [SerializeField] bool _activeAtStart = false;
    void Start()
    {
        gameObject.SetActive(_activeAtStart);
        gameObject.transform.SetParent(FindObjectOfType<CanvasBars>().transform);
        GetComponent<UIToObject>().Go();
    }
}
