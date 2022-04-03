using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBase : MonoBehaviour, IHealth
{
    [SerializeField] private float _health = 50;
    [SerializeField] private int _teamID = 0;

    public UnityEvent OnDeath;

    public float Health => _health;

    public float MaxHealth => Health;

    public bool IsAlive => _health < 0;

    public int TeamID => _teamID;

    public void AddHealth(float health)
    {
        _health += health;
    }

    public void RemoveHealth(float health)
    {
        _health -= health;
        if (_health <= 0) OnDeath.Invoke();
    }

    public void SetHealth(float health)
    {
        _health = health;
        if (_health <= 0) OnDeath.Invoke();
    }

}
