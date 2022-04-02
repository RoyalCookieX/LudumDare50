using UnityEngine;

public class RadiusTower : BaseTower<RadiusTowerData>
{
    public float Radius => _towerData.Radius;
    
    private void Start()
    {
        SetHealth(MaxHealth);
    }

    protected override bool IsInRange(Vector2 point)
    {
        Vector2 displacement = point - (Vector2)transform.position;
        return displacement.sqrMagnitude <= Radius * Radius;
    }

    private void OnDrawGizmos()
    {
        if (!_towerData)
            return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
