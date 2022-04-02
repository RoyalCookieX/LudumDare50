using UnityEngine;

public class RadiusTower : BaseTower
{
    [SerializeField, Min(0.0f)] private float _radius;

    private void Start()
    {
        SetHealth(MaxHealth);
    }

    protected override bool IsInRange(Vector2 point)
    {
        Vector2 displacement = point - (Vector2)transform.position;
        return displacement.sqrMagnitude <= _radius * _radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
