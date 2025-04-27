using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    
    Vector3 dir = Vector3.right;
    private void Update()
    {
        Move(dir);
    }
    private void Awake()
    {
        StartCoroutine(ChangeDir(Random.Range(0,10)));
    }
    IEnumerator ChangeDir(float time)
    {
        yield return new WaitForSeconds(time);
        dir *= -1;
        spriteRenderer.flipX = !spriteRenderer.flipX;
        StartCoroutine(ChangeDir(Random.Range(0, 10)));
    }

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
