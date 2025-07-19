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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipOneHandedWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipTwoHandedWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DeEquip();
        }

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
        player.animator.SetLayerWeight(2, 1);
        player.isWeaponEquipped = true;
        weaponInHand.GetComponent<SpriteRenderer>().sprite = oneHandedGun.weaponSprite;
        // Show weapons on player
        weaponInHand.SetActive(true);
        weaponOnBack.SetActive(true);
        weaponOnHip.SetActive(false);
        player.animator.runtimeAnimatorController = oneHandedGun.weaponAnimOverride;
        // Set held weapon
        currentHeldWeapon = oneHandedGun;
        // Update UI
        weaponUI.UpdateWeaponUI(currentHeldWeapon);
        gunTimer = currentHeldWeapon.firerate;
        player.animator.SetFloat("ShootingSpeed", 1f);
    }

    void EquipTwoHandedWeapon()
    {
        player.animator.SetLayerWeight(2, 1);
        player.isWeaponEquipped = true;
        weaponInHand.GetComponent<SpriteRenderer>().sprite = twoHandedGun.weaponSprite;
        // Show weapons on player
        weaponInHand.SetActive(true);
        weaponOnBack.SetActive(false);
        weaponOnHip.SetActive(true);
        player.animator.runtimeAnimatorController = twoHandedGun.weaponAnimOverride;
        // Set held weapon
        currentHeldWeapon = twoHandedGun;
        // Update UI
        weaponUI.UpdateWeaponUI(currentHeldWeapon);
        gunTimer = currentHeldWeapon.firerate;
        player.animator.SetFloat("ShootingSpeed", 3.5f);
    }

    void DeEquip()
    {
        player.animator.SetLayerWeight(2, 0);
        player.isWeaponEquipped = false;
        // Show weapons on player
        weaponInHand.SetActive(false);
        weaponOnBack.SetActive(true);
        weaponOnHip.SetActive(true);

        player.animator.runtimeAnimatorController = defaultController;
    }

    
}
