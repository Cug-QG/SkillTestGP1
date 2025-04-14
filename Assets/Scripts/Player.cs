using UnityEngine;

public class Player : Character
{
    protected override void PerformAction()
    {
        throw new System.NotImplementedException();
    }

    [SerializeField] float jumpForce;

    void Update()
    {
        Move(transform.right * Input.GetAxisRaw("Horizontal"));
        if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}
