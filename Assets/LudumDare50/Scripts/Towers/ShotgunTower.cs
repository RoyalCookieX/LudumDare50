using UnityEngine;

public class ShotgunTower : BaseTower<ShotgunTowerData>
{
    public float Radius => _towerData.Radius;
    public float FOV => _towerData.FOV;
    public float Angle { get => _angle; set => _angle = value; }
    
    [SerializeField] private float _angle;

    private void Start()
    {
        SetHealth(MaxHealth);
    }

    protected override bool IsInRange(Vector2 point)
    {
        Vector2 displacement = point - (Vector2)transform.position;
        float targetAngle = Mathf.Atan2(displacement.y, displacement.x);
        float minAngle = _angle - FOV / 2.0f;
        float maxAngle = _angle + FOV / 2.0f;
        return targetAngle > minAngle && targetAngle < maxAngle;
    }

    private void OnDrawGizmos()
    {
        if (!_towerData)
            return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, Radius);
        Quaternion minRotation = Quaternion.Euler(Vector3.forward * (_angle - FOV / 2.0f));
        Quaternion maxRotation = Quaternion.Euler(Vector3.forward * (_angle + FOV / 2.0f));
        Vector2 minDirection = minRotation * Vector2.right;
        Vector2 maxDirection = maxRotation * Vector2.right;
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, maxDirection * Radius);
        Gizmos.DrawRay(transform.position, minDirection * Radius);
    }
}
