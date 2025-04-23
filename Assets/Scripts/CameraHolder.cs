using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private Transform target;
    Vector3 newPos;

    void LateUpdate()
    {
        newPos = transform.position;
        newPos.x = target.position.x;
        transform.position = newPos;
    }
}