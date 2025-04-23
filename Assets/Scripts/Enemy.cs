using UnityEngine;

public class Enemy : Character
{
    protected override void Die()
    {
        Destroy(gameObject);
    }

    protected override void PerformAction(Transform target)
    {
        target.GetComponent<Player>().ChangeHealth(-data.Dmg);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Player")) { return; }

        ContactPoint2D contact = collision.contacts[0];
        Vector2 normal = contact.normal;

        if (normal.y > -0.5f) { PerformAction(collision.transform); }
    }
}
