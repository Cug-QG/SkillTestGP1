using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    Vector3 newPos;

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}