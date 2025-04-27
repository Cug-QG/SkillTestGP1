using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Player")) { return; }

        collision.transform.GetComponent<Player>().Kill();
    }
}
