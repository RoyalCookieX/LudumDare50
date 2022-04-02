using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> _pool;

    public ObjectPool(T prefab, int maxPool)
    {
        _pool = new Queue<T>();
        for (int i = 0; i < maxPool; i++)
        {
            T instance = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            instance.gameObject.SetActive(false);
            _pool.Enqueue(instance);
        }
    }

    public T Instantiate(Vector3 position, Quaternion rotation)
    {
        T instance = _pool.Dequeue();
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.gameObject.SetActive(true);
        _pool.Enqueue(instance);
        return instance;
    }
}