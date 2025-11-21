using UnityEngine;

public class Enemy : Entity
{
    GameManager gm;
    public float hp;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        hp = 100;
    }

    private void Update()
    {
        FlipSelf(GameObject.FindGameObjectWithTag("Player"));
    }

    private void FixedUpdate()
    {
        if (FindDistanceBetweenVectors(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) > 2)
        {
            FollowTarget(GameObject.FindGameObjectWithTag("Player"));
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        
        CheckGround(isFacingRight);
    }

    void FollowTarget(GameObject target)
    {
        animator.SetBool("IsMoving", true);
        if (transform.position.x > target.transform.position.x)
        {
            rb2D.linearVelocity = new Vector2(-GetSpeed() * Time.deltaTime, rb2D.linearVelocity.y);
        }
        else
        {
            rb2D.linearVelocity = new Vector2(GetSpeed() * Time.deltaTime, rb2D.linearVelocity.y);
        }
    }

    void FlipSelf(GameObject target)
    {
        // Check which direction the player is facing
        if (transform.position.x > target.transform.position.x)
        {
            SetFacingRight(false);
        }
        else
        {
            SetFacingRight(true);
        }

        // Then flip the player, arms and weapon
        if (GetFacingRight())
        {
            rig.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            rig.transform.localScale = new Vector2(-1, 1);
        }
    }

    float FindDistanceBetweenVectors(Vector2 v1, Vector2 v2)
    {
        float distance = 0;
        float x;
        float y;
        x = v1.x - v2.x;
        y = v1.y - v2.y;
        x *= x;
        y *= y;
        x += y;
        distance = Mathf.Sqrt(x);
        return distance;
    }

    public void Hurt(float damage)
    {
        hp -= damage;
        gm.RewardPointsOnHit();
        Die();
    }

    void Die()
    {
        if(hp <= 0)
        {
            gm.RewardPointsOnKill();
            Destroy(gameObject);
        }
    }
}
