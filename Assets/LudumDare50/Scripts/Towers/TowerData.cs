using UnityEngine;

[CreateAssetMenu(fileName = "BaseTower", menuName = "Towers/BaseTower", order = 0)]
public class TowerData : ScriptableObject
{
    public float MaxHealth => _maxHealth;
    public float Cooldown => _cooldown;
    public float Radius => _radius;
    public float FOV => _fov;
    public int Burst => _burst;
    public float Accuracy => _accuracy;
    
    [SerializeField, Min(0.0f)] private float _maxHealth;
    [SerializeField, Min(0.0f)] private float _cooldown = 1.0f;
    [SerializeField, Min(0.0f)] private float _radius;
    [SerializeField, Range(0.0f, 360.0f)] private float _fov;
    [SerializeField, Min(1)] private int _burst = 1;
    [SerializeField, Range(0.0f, 1.0f)] private float _accuracy = 1.0f;
}