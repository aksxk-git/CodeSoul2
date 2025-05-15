using UnityEngine;

public class Player : Entity
{
    // Movement Vector
    Vector2 movement;

    private void Start()
    {
        SetHealth(100);
        SetSpeed(100);
    }

    private void Update()
    {
        Animate();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        movement.x = Input.GetAxis("Horizontal");
        rb2D.linearVelocity = new Vector2(movement.x * GetSpeed() * Time.deltaTime, 0);
    }

    void Animate()
    {
        if (movement.x > 0 || movement.x < 0) animator.SetBool("IsMoving", true);
        else animator.SetBool("IsMoving", false);
    }

    void FlipSelf()
    {
        
    }
}
