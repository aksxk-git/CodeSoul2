using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Reference to UI
    [SerializeField] WeaponUI weaponUI;

    // Entity
    [SerializeField] Player player;
    RuntimeAnimatorController defaultController;

    // Weapon components
    [SerializeField] GameObject weaponFirepoint;

    // Weapon equip placements
    [SerializeField] GameObject weaponInHand; // Show current equipped weapon
    [SerializeField] GameObject weaponOnBack; // For larger two handed weapons
    [SerializeField] GameObject weaponOnHip; // For one handed small weapons

    // Weapon slots
    [SerializeField] Weapon currentHeldWeapon;
    [SerializeField] Weapon oneHandedGun;
    [SerializeField] Weapon twoHandedGun;
    [SerializeField] Weapon[] weapons;

    // Player limbs
    [SerializeField] GameObject head;
    [SerializeField] GameObject arms;

    // Float
    float gunTimer;

    private void Start()
    {
        defaultController = player.animator.runtimeAnimatorController;
    }

    private void Update()
    {
        UpdateWeaponry();

        if (currentHeldWeapon != null)
        {
            if (gunTimer > currentHeldWeapon.firerate)
            {
                gunTimer = currentHeldWeapon.firerate;
            }
            else
            {
                gunTimer += Time.deltaTime;
            }

            if (Input.GetMouseButton(0) && gunTimer >= currentHeldWeapon.firerate)
            {
                gunTimer = 0;
                FireWeapon();
            }
            else
            {
                player.animator.SetBool("IsShooting", false);
            }
        }
    }

    public void FireWeapon()
    {
        player.animator.SetBool("IsShooting", true);

        float rayLength = 15f; // Set a fixed length for your ray
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - weaponFirepoint.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(weaponFirepoint.transform.position, direction * rayLength, rayLength, LayerMask.GetMask("Shootable"));

        if (hit)
        {
            Debug.Log(hit.collider.name);
            Debug.DrawRay(weaponFirepoint.transform.position, direction * rayLength, Color.green, 0.1f);
        }
    }

    void UpdateWeaponry()
    {
        // Swap weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DeEquip();
        }
        // Set weapon sprites
        if (DoesPlayerHaveAOneHandedWeapon())
        {
            weaponOnHip.GetComponent<SpriteRenderer>().sprite = oneHandedGun.weaponSprite;
        }
        if (DoesPlayerHaveATwoHandedWeapon())
        {
            weaponOnBack.GetComponent<SpriteRenderer>().sprite = twoHandedGun.weaponSprite;
        }
        // Update UI
        weaponUI.UpdatePrimary(weapons[0]);
        weaponUI.UpdateSecondary(weapons[1]);
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

    void EquipWeapon(int slot)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == slot)
            {
                currentHeldWeapon = weapons[i];
            }
        }
        // Setting variables
        player.isWeaponEquipped = true;
        gunTimer = currentHeldWeapon.firerate;
        // Set and hide sprites
        weaponInHand.GetComponent<SpriteRenderer>().sprite = weapons[slot].weaponSprite;
        // Animation handling
        player.animator.SetLayerWeight(2, 1);
        player.animator.SetFloat("ShootingSpeed", weapons[slot].animationSpeed);
        player.animator.runtimeAnimatorController = weapons[slot].weaponAnimOverride;
    }

    void DeEquip()
    {
        player.animator.SetLayerWeight(2, 0);
        player.isWeaponEquipped = false;
        player.animator.runtimeAnimatorController = defaultController;
    }

    
}
