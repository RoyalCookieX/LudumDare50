using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _waveNumber = 1;

    private bool _waitingForEnemies = false;

    public float EnemiesKilled;
    public float _enemiesSpawned;

    private GameObject _scoreTracker;

    private void Start()
    {
        StartWave(_waveNumber);
    }

    private void Update()
    {
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
        for(int i = 0; i < Mathf.Pow(WaveNumber, 2); i++)
        {
            Instantiate(_enemies[Random.Range(0, _enemies.Length)], _spawnPoint.position, Quaternion.identity);
            _enemiesSpawned++;
            yield return new WaitForSeconds(Random.Range(0.5f, 0.8f));
        }


        _waitingForEnemies = true;
        Debug.Log("Enemies Spawned in Wave: " + _enemiesSpawned);

    }


}
