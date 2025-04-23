using UnityEngine;

public class FloatingFollower : MonoBehaviour
{
    [SerializeField] private Transform target;          // Il player
    [SerializeField] private Vector3 offset = new Vector3(-1f, 1f, 0f); // Distanza rispetto al player
    [SerializeField] private float smoothTime = 0.3f;   // Ritardo del movimento

    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float floatAmplitude = 0.5f;
    [SerializeField] private float floatFrequency = 2f;

    void Update()
    {
        bool facingRight = !target.GetComponent<Player>().GetFlipStatus();
        float facingDir = facingRight ? 1 : -1;
        GetComponent<SpriteRenderer>().flipX = facingRight ? false : true;
        Vector3 dynamicOffset = new Vector3(offset.x * facingDir, offset.y, offset.z);

        Vector3 baseTargetPos = target.position + dynamicOffset;

        // Oscillazione verticale
        float floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        baseTargetPos.y += floatOffset;

        // Smooth follow
        transform.position = Vector3.SmoothDamp(transform.position, baseTargetPos, ref velocity, smoothTime);
    }
}