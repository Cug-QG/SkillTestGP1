using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    protected float speed;

    [SerializeField] protected CharacterData data;

    protected struct Health
    {
        public float maxHealth;
        public float currentHealth;
        public bool invulnerable;

        public Health(float maxHP)
        {
            maxHealth = maxHP;
            currentHealth = maxHealth;
            invulnerable = false;
        }
    }

    protected Health health;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        speed = data.Speed;
        health = new Health(data.MaxHP);
    }

    public void ChangeHealth(float input)
    {
        if (input < 0f && health.invulnerable) { return; }
        health.currentHealth += input;
        if (health.currentHealth <= 0f) { Die(); }
        if (health.currentHealth > health.maxHealth) { health.currentHealth = health.maxHealth; }
    }

    protected void Move(Vector2 direction) 
    {
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
    }

    protected abstract void Die();

    protected abstract void PerformAction(Transform target);
}
