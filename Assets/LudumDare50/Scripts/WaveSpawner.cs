using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _waveNumber = 1;

    private bool _waitingForEnemies = false;
    private int _spawnedEnemy;
    private float _spawnTime;

    public float EnemiesKilled;
    public float _enemiesSpawned;

    private GameObject _scoreTracker;

    private void Start()
    {
        StartWave(_waveNumber);
    }

    private void Update()
    {
        if(_waveNumber < 5)
        {
            _spawnedEnemy = 0;
        } else if (_waveNumber < 8)
        {
            _spawnedEnemy = Random.Range(0, 2);
        } else
        {
            _spawnedEnemy = Random.Range(0, _enemies.Length);
        }

        if(_waveNumber < 10)
        {
            _spawnTime = 1.2f;
        } else if (_waveNumber < 20)
        {
            _spawnTime = 0.8f;
        } else if (_waveNumber < 30)
        {
            _spawnTime = 0.6f;
        } else
        {
            _spawnTime = 0.3f;
        }

        if(_waitingForEnemies && EnemiesKilled >= _enemiesSpawned)
        {
            _waitingForEnemies = false;
            EnemiesKilled = 0;
            _enemiesSpawned = 0;
            Debug.Log("Wave Over");
            _waveNumber++;
            _scoreTracker = GameObject.FindGameObjectWithTag("ScoreTracker");
            _scoreTracker.GetComponent<ScoreTracker>().IncreaseScore(1000);
            StartWave(_waveNumber);

        }
    }

    public void StartWave(float waveNumber)
    {
        StartCoroutine(SpawnWave(waveNumber));
    }


    private IEnumerator SpawnWave(float WaveNumber)
    {
        for(int i = 0; i < (Mathf.Pow(WaveNumber, 2) / 2); i++)
        {
            
            //Random.Range(0, _enemies.Length);
            Instantiate(_enemies[_spawnedEnemy], _spawnPoint.position, Quaternion.identity);
            _enemiesSpawned++;
            yield return new WaitForSeconds(_spawnTime);
        }


        _waitingForEnemies = true;
        Debug.Log("Enemies Spawned in Wave: " + _enemiesSpawned);

    }


}
