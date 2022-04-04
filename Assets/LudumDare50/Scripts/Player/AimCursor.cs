using UnityEngine;

public class AimCursor : MonoBehaviour
{
    [SerializeField] private Transform _arrow;
    
    public void SetAngle(float angle)
    {
        _arrow.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
