using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunTower", menuName = "Towers/ShotgunTower", order = 0)]
public class ShotgunTowerData : TowerData
{
    public float Radius => _radius;
    public float FOV => _fov;
    
    [SerializeField, Min(0.0f)] private float _radius;
    [SerializeField, Min(0.0f)] private float _fov;
}