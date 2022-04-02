using UnityEngine;

[CreateAssetMenu(fileName = "RadiusTower", menuName = "Towers/RadiusTower", order = 0)]
public class RadiusTowerData : TowerData
{
    public float Radius => _radius;
    
    [SerializeField] private float _radius;
}