using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "TowerData", order = 0)]
public class TowerData : ScriptableObject
{
    public float MaxHealth => _maxHealth;
    
    [SerializeField, Min(0.0f)] private float _maxHealth;
}