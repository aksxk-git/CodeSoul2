using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Entity Components
    protected Rigidbody2D rb2D;
    protected Animator animator;
    // Entity Variables
    private float health = 100.0f;
    private float speed = 100.0f;

    private void Awake()
    {
        // Get Components
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    protected void SetHealth(float health)
    {
        this.health = health;
    }

    protected float GetHealth()
    { 
        return this.health;
    }

    protected void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    protected float GetSpeed()
    {
        return this.speed;
    }
}
