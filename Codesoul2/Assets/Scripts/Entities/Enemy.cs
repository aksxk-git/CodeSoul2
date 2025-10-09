using UnityEngine;

public class Enemy : Entity
{
    private void FixedUpdate()
    {
        FollowTarget(GameObject.FindGameObjectWithTag("Player"));
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
}
