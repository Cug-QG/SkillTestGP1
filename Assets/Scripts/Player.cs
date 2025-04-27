using System.Collections;
using UnityEngine;

public class Player : Character
{
    [SerializeField] float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private bool shielded;
    private AnimationState state;

    void Update()
    {
        Move(transform.right * Input.GetAxis("Horizontal"));
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) { Jump(); }
        float inputX = Input.GetAxisRaw("Horizontal");

        if (inputX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputX < 0)
        {
            spriteRenderer.flipX = true;
        }


        if (IsGrounded())
        {
            if (rb.linearVelocityX != 0) { SetState(AnimationState.Run); }
            else { SetState(AnimationState.Idle); }
        }
        else 
        {
            if (rb.linearVelocityY < 0) { SetState(AnimationState.Fall); }
            if (rb.linearVelocityY > 0) { SetState(AnimationState.Jump); }
        }
    }

    private void SetState(AnimationState input)
    {
        if (input == state) return;
        print(input);
        switch (input)
        {
            case AnimationState.Idle:
                state = AnimationState.Idle;
                ResetAnimatorParameters();
                break;
            case AnimationState.Run:
                state = AnimationState.Run;
                ResetAnimatorParameters();
                animator.SetBool("Running", true);
                break;
            case AnimationState.Jump:
                state = AnimationState.Jump;
                ResetAnimatorParameters();
                animator.SetBool("Jump", true);
                break;
            case AnimationState.Fall:
                state = AnimationState.Fall;
                ResetAnimatorParameters();
                animator.SetBool("Fall", true);
                break;
        }
    }

    void ResetAnimatorParameters()
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            switch (param.type)
            {
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(param.name, false);
                    break;
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(param.name, 0f);
                    break;
                case AnimatorControllerParameterType.Int:
                    animator.SetInteger(param.name, 0);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    animator.ResetTrigger(param.name);
                    break;
            }
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    protected override void PerformAction(Transform target)
    {
        target.GetComponent<Enemy>().ChangeHealth(-damage);
        Jump();
    }

    protected override void Die()
    {
        GameManager.Instance.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Enemy")) { return; }

        ContactPoint2D contact = collision.contacts[0];
        Vector2 normal = contact.normal;

        if (normal.y > 0.5f) { PerformAction(collision.transform); }
    }

    public bool GetFlipStatus() { return spriteRenderer.flipX; }

    public override void ChangeHealth(float input)
    {
        if (shielded && input<=0) { return; }
        base.ChangeHealth(input);
        UIManager.Instance.SetHP(health.currentHealth);
    }

    public void Kill()
    {
        base.ChangeHealth(-health.maxHealth);
    }

    private Coroutine currentShieldRoutine;
    public void SetShield(float time)
    {
        if (currentShieldRoutine != null) { StopCoroutine(currentShieldRoutine); }
        currentShieldRoutine = StartCoroutine(SetShieldRoutine(time));
    }

    IEnumerator SetShieldRoutine(float time)
    {
        shielded = true;
        yield return new WaitForSeconds(time);
        shielded = false;
    }

    private Coroutine currentDMGBoostRoutine;
    public void SetDMGBoost(float time)
    {
        if (currentDMGBoostRoutine != null) { StopCoroutine(currentDMGBoostRoutine); }
        currentShieldRoutine = StartCoroutine(SetDMGBoostRoutine(time));
    }

    IEnumerator SetDMGBoostRoutine(float time)
    {
        damage = data.Dmg * 2;
        yield return new WaitForSeconds(time);
        damage = data.Dmg;
    }
}