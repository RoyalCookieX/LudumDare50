using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed => _projectileData.Speed;
    public float Damage => _projectileData.Damage;
    
    [SerializeField] protected ProjectileData _projectileData;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private int _teamID;

    private void OnEnable()
    {
        SetPositionAndRotation(transform.position, transform.rotation);
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHealth health) && health.TeamID != _teamID)
        {
            health.RemoveHealth(Damage);
            gameObject.SetActive(false);
        }
    }

    private void SetPositionAndRotation(Vector2 position, Quaternion rotation)
    {
        _rigidbody.position = position;
        _rigidbody.SetRotation(rotation);
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddRelativeForce(Vector2.right * Speed, ForceMode2D.Impulse);
    }
}