using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Entity Components
    protected Rigidbody2D rb2D;
    public Animator animator;
    public GameObject groundCheckerRight;
    public GameObject groundCheckerLeft;
    public GameObject groundCheckerDown;
    public GameObject rig;

    // Entity Variables
    private float health = 100.0f;
    private float speed = 100.0f;
    protected bool facingRight = true;

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

    protected void SetFacingRight(bool isFacingRight)
    {
        this.facingRight = isFacingRight;
    }

    protected bool GetFacingRight()
    {
        return this.facingRight;
    }

    protected void Damage(float damage)
    {
        this.health -= damage;
    }

    protected void CheckGround(bool facingRight)
    {
        // Create Downwards Ray
        float rayLengthDown = 0.01f;
        RaycastHit2D hitDown = Physics2D.Raycast(groundCheckerDown.transform.position, Vector2.down * rayLengthDown, rayLengthDown, LayerMask.GetMask("Ground"));

        if (hitDown)
        {
            //Debug.Log("Touching ground");

            // Create Right Ray
            float rayLengthRight = 0.5f;
            RaycastHit2D hitRight = Physics2D.Raycast(groundCheckerRight.transform.position, Vector2.right * rayLengthRight, rayLengthRight, LayerMask.GetMask("Ground"));

            if (hitRight && facingRight)
            {
                //Debug.Log(hitRight.transform.name);
                gameObject.transform.position += new Vector3(25, gameObject.transform.position.y + hitRight.point.y + 25, 0) * Time.deltaTime;
            }

            // Create Left Ray
            float rayLengthLeft = 0.5f;
            RaycastHit2D hitLeft = Physics2D.Raycast(groundCheckerLeft.transform.position, Vector2.left * rayLengthLeft, rayLengthLeft, LayerMask.GetMask("Ground"));

            if (hitLeft && !facingRight)
            {
                //Debug.Log(hitLeft.transform.name);
                gameObject.transform.position += new Vector3(-25, gameObject.transform.position.y + hitLeft.point.y + 25, 0) * Time.deltaTime;
            }
        }
    }
}
