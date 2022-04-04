using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionVFX;
    // Start is called before the first frame update
    void Start()
    {
        // explosion particle effect
        var em = _explosionVFX.emission; // _deathVFX is a particle system with a child
        var dur = _explosionVFX.main.duration;

        em.enabled = true;
        _explosionVFX = Instantiate(_explosionVFX, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
        _explosionVFX.Play();
        
        Invoke(nameof(DestroyObj), dur);
    }

    private void DestroyObj()
    {
        Destroy(_explosionVFX.gameObject);
        Destroy(gameObject);
    }
}
