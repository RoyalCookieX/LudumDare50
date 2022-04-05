using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> _pool;

    public ObjectPool(T prefab, int maxPoolSize)
    {
        Allocate(prefab, maxPoolSize);
    }

    public void Allocate(T prefab, int maxPoolSize)
    {
        if (_pool == null)
            _pool = new Queue<T>();
        else
            _pool.Clear();
        
        for (int i = 0; i < maxPoolSize; i++)
        {
            T instance = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            instance.gameObject.SetActive(false);
            _pool.Enqueue(instance);
        }
    }

    public void Free()
    {
        while (_pool.Count > 0)
        {
            T instance = _pool.Dequeue();
            if(instance == null)
                continue;
            Object.Destroy(instance.gameObject);
        }
    }

    public T Instantiate(Vector3 position, Quaternion rotation)
    {
        T instance = _pool.Dequeue();
        instance.gameObject.SetActive(false);
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.gameObject.SetActive(true);
        _pool.Enqueue(instance);
        return instance;
    }
}