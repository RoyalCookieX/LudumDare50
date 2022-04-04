using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour, IHealth
{
    [SerializeField] private float _speed;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private int _teamID = 1;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    //[SerializeField] private ParticleSystem _enemyHitParticle;
    [SerializeField] private Transform _effectLocation;
    [SerializeField] protected ExplosionVFX _explosionEffect;

    private bool _dying = false;

    public UnityEvent OnReachedGoal;

    private PathingPoints _pPoints;
    private int _pointIndex;

    private float _randomPath;
    private GameObject _scoreTracker;    

    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public bool IsAlive =>_health > 0;

    public int TeamID => _teamID;

    private void Start()
    {
        _randomPath = Random.Range(0, 2);
        Debug.Log(_randomPath);
        if(_randomPath == 1)
        {
            _pPoints = GameObject.FindGameObjectWithTag("PathingPoints").GetComponent<PathingPoints>();
        } else
        {
            _pPoints = GameObject.FindGameObjectWithTag("Path_2").GetComponent<PathingPoints>();
        }
        
    }

    private void Update()
    {
        MoveToNext();

        if (!(IsAlive) && !_dying)
        {
            _scoreTracker = GameObject.FindGameObjectWithTag("ScoreTracker");
            _scoreTracker.GetComponent<ScoreTracker>().IncreaseScore(100);
            FindObjectOfType<WaveSpawner>().EnemiesKilled++;
            // Trigger Death Animation Here
            _animator?.SetBool("IsAlive", false);
            Destroy(gameObject, 2f);
            _dying = true;
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IHealth IHealth))
        {
            if(IHealth.TeamID != _teamID)
            {
                IHealth.RemoveHealth(1);
            }
        }
    }

    private void MoveToNext()
    {

        // move the enemy to the next checkpoint
        _rb.position = Vector2.MoveTowards(transform.position, _pPoints._pathPoints[_pointIndex].position, _speed * Time.deltaTime);

        // rotate the enemy to face the next checkpoint
        /*Vector3 dir = _pPoints._pathPoints[_pointIndex].position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/


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

        // play hit animation
        _animator?.SetTrigger("Hit");
    }

    public void SetHealth(float health)
    {
        _health = health;
    }

    public void ReachedGoal()
    {
        FindObjectOfType<WaveSpawner>().EnemiesKilled++;
        Destroy(gameObject);
    }

    public void EnemyDeath()
    {
        Instantiate(_explosionEffect, _effectLocation.position, Quaternion.identity);
    }
}
