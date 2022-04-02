using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour, IHeath
{
    [SerializeField] private float _speed;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;

    public UnityEvent OnReachedGoal;

    private PathingPoints _pPoints;
    private int _pointIndex;
    

    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public bool IsAlive =>_health > 0;

    private void Start()
    {
        _pPoints = GameObject.FindGameObjectWithTag("PathingPoints").GetComponent<PathingPoints>();
    }

    private void Update()
    {
        MoveToNext();

        if (!(IsAlive))
        {
            FindObjectOfType<WaveSpawner>().EnemiesKilled++;
            Destroy(gameObject);
        }
    }

    private void MoveToNext()
    {

        // move the enemy to the next checkpoint
        transform.position = Vector2.MoveTowards(transform.position, _pPoints._pathPoints[_pointIndex].position, _speed * Time.deltaTime);

        // rotate the enemy to face the next checkpoint
        Vector3 dir = _pPoints._pathPoints[_pointIndex].position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        if (Vector2.Distance(transform.position, _pPoints._pathPoints[_pointIndex].position) < 0.1f)
        {
            if (_pointIndex < _pPoints._pathPoints.Length - 1)
            {
                _pointIndex++;
            }
            else
            {
                OnReachedGoal.Invoke();
            }
        }
    }

    public void AddHealth(float health)
    {
        _health += health;
    }

    public void RemoveHealth(float health)
    {
        _health -= health;
    }

    public void SetHealth(float health)
    {
        _health = health;
    }
}
