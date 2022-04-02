using UnityEngine;

[CreateAssetMenu(fileName = "BaseTower", menuName = "Towers/BaseTower", order = 0)]
public class TowerData : ScriptableObject
{
    public float MaxHealth => _maxHealth;
    public int FireRate => _fireRate;
    public float Radius => _radius;
    public float FOV => _fov;
    
    [SerializeField, Min(0.0f)] private float _maxHealth;
    [SerializeField, Min(1)] private int _fireRate;
    [SerializeField, Min(0.0f)] private float _radius;
    [SerializeField, Range(0.0f, 180.0f)] private float _fov;
}