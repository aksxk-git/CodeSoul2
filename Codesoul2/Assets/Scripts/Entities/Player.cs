using Unity.Multiplayer.Center.Common;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Player : Entity
{
    // Movement
    Vector2 movement;
    bool facingRight = true;
    bool sprinting = false;
    bool weaponEquipped = false;
    bool walkingBackward = false;
    bool crouched = false;

    // Blinking
    float blinkTime = 5; // Every 5 seconds the player blinks
    float blinkTimer; // Track time elapsed

    Vector3 mousePosition;
    Vector3 direction;

    // Weapon slots
    [SerializeField] Weapon oneHandedGun;
    [SerializeField] Weapon twoHandedGun;

    // Player components
    [SerializeField] GameObject head;
    [SerializeField] GameObject arms;

    // Weapon placements
    [SerializeField] GameObject weaponInHand; // Show current equipped weapon
    [SerializeField] GameObject weaponOnBack; // For larger two handed weapons
    [SerializeField] GameObject weaponOnHip; // For one handed small weapons

    // Default animator
    RuntimeAnimatorController defaultController;

    private void Start()
    {
        SetHealth(100);
        SetSpeed(100);

        defaultController = animator.runtimeAnimatorController;

    }

    private void Update()
    {
        Animate();
        FlipSelf();
        MoveHead();
        UpdateWeaponry();
        WalkBackwards();

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - transform.position;

        if (weaponEquipped) MoveGunAndArms();

        

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsShooting", true);

            float rayLength = 15f; // Set a fixed length for your ray
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction * rayLength, rayLength, LayerMask.GetMask("Shootable"));

            // If it hits something...
            if (hit)
            {
                Debug.Log(hit.collider.name);
                Debug.DrawRay(transform.position, direction * hit.distance, Color.green, 0.1f);
            }
        }
        else
        {
            animator.SetBool("IsShooting", false);
        }

        if(Input.GetKeyDown(KeyCode.LeftControl) && !crouched)
        {
            crouched = true;
            Crouch();
        }
        else if(Input.GetKeyDown(KeyCode.LeftControl) && crouched)
        {
            crouched = false;
            UnCrouch();
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
        if (Input.GetKey(KeyCode.LeftShift) && IsMoving())
        {
            sprinting = true;
            SetSpeed(200);
        }
        else
        {
            sprinting = false;
            SetSpeed(100);
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
        if (Input.GetKey(KeyCode.Alpha1) && DoesPlayerHaveAOneHandedWeapon())
        {
            EquipOneHandedWeapon();
        }
            
        if (Input.GetKey(KeyCode.Alpha2) && DoesPlayerHaveATwoHandedWeapon())
        {
            EquipTwoHandedWeapon();
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            DeEquip();
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

    void WalkBackwards()
    {
        // Check if player is walking backwards
        if (facingRight && movement.x < 0 || !facingRight && movement.x > 0)
        {
            walkingBackward = true;
        }
        else
        {
            walkingBackward = false;
        }
        // Slow down player if walking backwards
        if (walkingBackward)
        {
            SetSpeed(50);
        }
    }

    void MoveGunAndArms()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arms.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void MoveHead()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        head.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void UpdateWeaponry()
    {
        // Set weapon sprites
        if (DoesPlayerHaveAOneHandedWeapon())
        {
            weaponOnHip.GetComponent<SpriteRenderer>().sprite = oneHandedGun.weaponSprite;
        }
        if (DoesPlayerHaveATwoHandedWeapon())
        {
            weaponOnBack.GetComponent<SpriteRenderer>().sprite = twoHandedGun.weaponSprite;
        }
    }

    bool DoesPlayerHaveAWeapon()
    {
        if (oneHandedGun != null || twoHandedGun != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool DoesPlayerHaveATwoHandedWeapon()
    {
        if (twoHandedGun != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool DoesPlayerHaveAOneHandedWeapon()
    {
        if (oneHandedGun != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void EquipOneHandedWeapon()
    {
        animator.SetLayerWeight(2, 1);
        weaponEquipped = true;    
        weaponInHand.SetActive(true);
        weaponOnBack.SetActive(true);
        weaponInHand.GetComponent<SpriteRenderer>().sprite = oneHandedGun.weaponSprite;
        weaponOnHip.SetActive(false);
        animator.runtimeAnimatorController = oneHandedGun.weaponAnimOverride;
    }

    void EquipTwoHandedWeapon()
    {
        animator.SetLayerWeight(2, 1);
        weaponEquipped = true;
        weaponInHand.SetActive(true);
        weaponOnBack.SetActive(false);
        weaponInHand.GetComponent<SpriteRenderer>().sprite = twoHandedGun.weaponSprite;
        weaponOnHip.SetActive(true);
        animator.runtimeAnimatorController = twoHandedGun.weaponAnimOverride;
    }

    void DeEquip()
    {
        animator.SetLayerWeight(2, 0);
        weaponEquipped = false;
        weaponInHand.SetActive(false);
        weaponOnBack.SetActive(true);
        weaponOnHip.SetActive(true);
        //animator.runtimeAnimatorController = defaultController;
    }

    void Crouch()
    {
        animator.SetLayerWeight(1, 1);
    }

    void UnCrouch()
    {
        animator.SetLayerWeight(1, 0);
    }
}
