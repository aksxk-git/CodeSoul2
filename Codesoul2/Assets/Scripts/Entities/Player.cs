using Unity.Multiplayer.Center.Common;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Player : Entity
{
    // Movement Vector
    Vector2 movement;
    bool facingRight = true;
    bool sprinting = false;
    bool weaponEquipped = false;
    bool walkingBackward = false;

    // Blinking
    float blinkTime = 5; // Every 5 seconds the player blinks
    float blinkTimer; // Track time elapsed

    Vector3 mousePosition;
    Vector3 direction;

    // Player components
    [SerializeField] GameObject head;
    [SerializeField] GameObject arms;
    [SerializeField] GameObject weaponInHand;
    [SerializeField] GameObject weaponOnBack;

    private void Start()
    {
        SetHealth(100);
        SetSpeed(100);
    }

    private void Update()
    {
        Animate();
        FlipSelf();
        MoveHead();

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - transform.position;

        if (weaponEquipped) MoveGunAndArms();

        if (facingRight && movement.x < 0 || !facingRight && movement.x > 0)
        {
            walkingBackward = true;
        }
        else
        {
            walkingBackward = false;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        // Move player side to side
        movement.x = Input.GetAxis("Horizontal");
        rb2D.linearVelocity = new Vector2(movement.x * GetSpeed() * Time.deltaTime, 0);

        // Sprinting
        if(Input.GetKey(KeyCode.LeftShift) && IsMoving())
        {
            sprinting = true;
            SetSpeed(200);
        }
        else
        {
            sprinting = false;
            SetSpeed(100);
        }

        // Slow Down
        if(walkingBackward)
        {
            SetSpeed(50);
        }
    }

    void Animate()
    {
        // Update blink timer
        if (blinkTimer > blinkTime)
        {
            blinkTimer = 0;
        }
        else
        {
            blinkTimer += Time.deltaTime;
        }

        // Update animator 
        animator.SetBool("IsMoving", IsMoving());
        animator.SetBool("IsSprinting", sprinting);
        animator.SetBool("IsWalkingBackward", walkingBackward);
        animator.SetFloat("BlinkTimer", blinkTimer);

        // Set animator layer weights
        if (Input.GetKey(KeyCode.Alpha1))
        {
            animator.SetLayerWeight(1, 1);
            weaponEquipped = true;

            // Show weapon in hand and hide back weapon
            weaponInHand.SetActive(true);
            weaponOnBack.SetActive(false);
        }
            
        if (Input.GetKey(KeyCode.Alpha2))
        {
            animator.SetLayerWeight(1, 0);
            weaponEquipped = false;

            // Show weapon in hand and hide back weapon
            weaponInHand.SetActive(false);
            weaponOnBack.SetActive(true);
        }
    }

    void FlipSelf()
    {
        // Check which direction the player is facing
        if (direction.x > 0)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }

        // Then flip the player, arms and weapon
        if (facingRight)
        {
            transform.localScale = new Vector2(1, 1);
            head.transform.localScale = new Vector2(1, 1);

            if (weaponEquipped)
            {
                arms.transform.localScale = new Vector2(1, 1);
            }
            
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
            head.transform.localScale = new Vector2(-1, -1);
            
            if (weaponEquipped)
            {
                arms.transform.localScale = new Vector2(-1, -1);
            }
            else
            {
                arms.transform.localScale = new Vector2(1, 1);
            }
        }

        // Reset arm rotation when unequipped
        if (!weaponEquipped)
        {
            arms.transform.rotation = Quaternion.identity;
        }
        
    }

    bool IsMoving()
    {
        if (movement.x > 0 || movement.x < 0) return true;
        else return false;
    }

    void MoveGunAndArms()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        head.transform.rotation = Quaternion.Euler(0, 0, angle);
        arms.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void MoveHead()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        head.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
