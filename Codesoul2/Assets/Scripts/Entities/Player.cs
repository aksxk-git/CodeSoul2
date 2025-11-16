using Unity.Multiplayer.Center.Common;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Player : Entity
{
    // Movement
    Vector2 movement;
    public bool isSprinting = false;
    public bool isWalkingBackward = false;
    public bool isCrouched = false;
    public bool isWeaponEquipped = false;
    public Vector3 mousePosition;
    Vector3 direction;
    // Player limbs
    [SerializeField] GameObject head;
    [SerializeField] GameObject arms;

    private void Start()
    {
        SetHealth(100);
        SetSpeed(100);
    }

    private void Update()
    {
        FlipSelf();
        MoveHead();
        WalkBackwards();

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - transform.position;

        if(Input.GetKeyDown(KeyCode.LeftControl) && !isCrouched)
        {
            isCrouched = true;
        }
        else if(Input.GetKeyDown(KeyCode.LeftControl) && isCrouched)
        {
            isCrouched = false;
        }

        if (isWeaponEquipped)
        {
            MoveGunAndArms();
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
            isSprinting = true;
            SetSpeed(200);
        }
        else
        {
            isSprinting = false;
            SetSpeed(100);
        }

        CheckGround(GetFacingRight());
    }

    void FlipSelf()
    {
        // Check which direction the player is facing
        if (direction.x > 0)
        {
            SetFacingRight(true);
        }
        else
        {
            SetFacingRight(false);
        }

        // Then flip the player, arms and weapon
        if (GetFacingRight())
        {
            if (isWeaponEquipped)
            {
                arms.transform.localScale = new Vector2(1, 1);
            }
            rig.transform.localScale = new Vector2(1, 1);
            head.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            if (isWeaponEquipped)
            {
                arms.transform.localScale = new Vector2(-1, -1);
            }
            else
            {
                arms.transform.localScale = new Vector2(1, 1);
            }
            rig.transform.localScale = new Vector2(-1, 1);
            head.transform.localScale = new Vector2(-1, -1);
        }

        // Reset arm rotation when unequipped
        if (!isWeaponEquipped)
        {
            arms.transform.rotation = Quaternion.identity;
        }
    }

    public bool IsMoving()
    {
        if (movement.x > 0 || movement.x < 0) return true;
        else return false;
    }

    void WalkBackwards()
    {
        // Check if player is walking backwards
        if (GetFacingRight() && movement.x < 0 || !GetFacingRight() && movement.x > 0)
        {
            isWalkingBackward = true;
        }
        else
        {
            isWalkingBackward = false;
        }
        // Slow down player if walking backwards
        if (isWalkingBackward)
        {
            SetSpeed(80);
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
}
