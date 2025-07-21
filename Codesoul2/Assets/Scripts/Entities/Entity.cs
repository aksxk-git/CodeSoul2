using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Entity Components
    protected Rigidbody2D rb2D;
    public Animator animator;
    public GameObject groundCheckerRight;
    public GameObject groundCheckerLeft;

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

    protected void Damage(float damage)
    {
        this.health -= damage;
    }

    protected void CheckGround(bool facingRight)
    {
        if(facingRight)
        {
            float rayLength = 1f; // Set a fixed length for your ray
            RaycastHit2D hit = Physics2D.Raycast(groundCheckerRight.transform.position, Vector2.right * rayLength, rayLength, LayerMask.GetMask("Ground"));

            if (hit)
            {
                Debug.Log(hit.transform.name);
                gameObject.transform.position += new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + hit.point.y, 0) * Time.deltaTime;
            }

        }
        else
        {
            float rayLength = 1f; // Set a fixed length for your ray
            RaycastHit2D hit = Physics2D.Raycast(groundCheckerLeft.transform.position, -Vector2.right * rayLength, rayLength, LayerMask.GetMask("Ground"));

            if (hit)
            {
                Debug.Log(hit.transform.name);
                gameObject.transform.position += new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - hit.point.y, 0) * Time.deltaTime;
            }
        }
    }
}
