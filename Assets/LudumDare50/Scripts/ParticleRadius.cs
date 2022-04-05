using UnityEngine;

public class ParticleRadius : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    
    public void SetRadius(float radius)
    {
        ParticleSystem.ShapeModule shape = _particles.shape;
        shape.radius = radius;
    }
}