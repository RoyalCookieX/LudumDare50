using UnityEngine;

public class ShotgunTower : BaseTower
{
    [SerializeField, Min(0.0f)] private float _radius;
    [SerializeField, Min(0.0f)] private float _fov;
    [SerializeField] private float _angle;

    private void Start()
    {
        SetHealth(MaxHealth);
    }

    protected override bool IsInRange(Vector2 point)
    {
        Vector2 displacement = point - (Vector2)transform.position;
        float targetAngle = Mathf.Atan2(displacement.y, displacement.x);
        float minAngle = _angle - _fov / 2.0f;
        float maxAngle = _angle + _fov / 2.0f;
        return targetAngle > minAngle && targetAngle < maxAngle;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _radius);
        Quaternion minRotation = Quaternion.Euler(Vector3.forward * (_angle - _fov / 2.0f));
        Quaternion maxRotation = Quaternion.Euler(Vector3.forward * (_angle + _fov / 2.0f));
        Vector2 minDirection = minRotation * Vector2.right;
        Vector2 maxDirection = maxRotation * Vector2.right;
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, maxDirection * _radius);
        Gizmos.DrawRay(transform.position, minDirection * _radius);
    }
}
