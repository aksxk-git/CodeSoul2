using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Reference to UI
    [SerializeField] WeaponUI weaponUI;

    // Reference to audio manager
    [SerializeField] AudioManager audioManager;

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
    [SerializeField] Weapon[] weapons;

    // Player limbs
    [SerializeField] GameObject head;
    [SerializeField] GameObject arms;

    // Float
    float gunShotTimer;
    float gunReloadTimer;

    // Bool
    bool isReloading;

    private void Start()
    {
        defaultController = player.animator.runtimeAnimatorController;
    }

    private void Update()
    {
        UpdateWeaponry();

        if (currentHeldWeapon != null)
        {
            if (gunShotTimer > currentHeldWeapon.firerate)
            {
                gunShotTimer = currentHeldWeapon.firerate;
            }
            else
            {
                gunShotTimer += Time.deltaTime;
            }

            if (Input.GetMouseButton(0) && gunShotTimer >= currentHeldWeapon.firerate && currentHeldWeapon.ammoInMag > 0 && !isReloading)
            {
                gunShotTimer = 0;
                FireWeapon();
            }
            else if (Input.GetMouseButton(0) && gunShotTimer >= currentHeldWeapon.firerate && currentHeldWeapon.ammoInMag <= 0)
            {
                gunShotTimer = 0;
                audioManager.PlaySoundEffect(currentHeldWeapon.noAmmoSFX);
            }
            else
            {
                player.animator.SetBool("IsShooting", false);
            }
        }
    }

    public void FireWeapon()
    {
        DepleteAmmo(currentHeldWeapon);

        player.animator.SetBool("IsShooting", true);

        float rayLength = 15f; // Set a fixed length for your ray
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - weaponFirepoint.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(weaponFirepoint.transform.position, direction * rayLength, rayLength, LayerMask.GetMask("Shootable"));

        if (hit)
        {
            Debug.Log(hit.collider.name);
            Debug.DrawRay(weaponFirepoint.transform.position, direction * rayLength, Color.green, 0.1f);
        }

        audioManager.PlaySoundEffect(currentHeldWeapon.shotSFX);
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

        // Update UI
        if (DoesPlayerHaveAWeaponInSlot(0))
        {
            weaponUI.UpdatePrimary(weapons[0]);
        }

        if (DoesPlayerHaveAWeaponInSlot(1))
        {
            weaponUI.UpdateSecondary(weapons[1]);
        }

        // Ammo
        if (currentHeldWeapon != null)
        {
            if (gunReloadTimer > currentHeldWeapon.reloadTime)
            {
                gunReloadTimer = currentHeldWeapon.reloadTime;
            }
            else
            {
                gunReloadTimer += Time.deltaTime;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.R) && currentHeldWeapon.ammoInMag < currentHeldWeapon.maxMagAmount)
        {
            isReloading = true;
        }

        if (isReloading)
        {
            gunReloadTimer += Time.deltaTime;
            player.animator.SetBool("IsReloading", true);
        }
        else
        {
            gunReloadTimer = 0;
            player.animator.SetBool("IsReloading", false);
        }

        if (currentHeldWeapon != null)
        {
            if (gunReloadTimer > currentHeldWeapon.reloadTime && isReloading)
            {
                Reload(currentHeldWeapon);
                player.animator.SetBool("IsReloading", false);
                isReloading = false;
            }
        }
        
    }

    void DepleteAmmo(Weapon currentWeapon)
    {
        currentHeldWeapon.ammoInMag--;
    }

    void Reload(Weapon currentWeapon) 
    {
        int neededAmmo = currentWeapon.maxMagAmount - currentWeapon.ammoInMag;
        int ammoToReload = Mathf.Min(neededAmmo, currentWeapon.reservedAmmo);
        currentWeapon.reservedAmmo -= ammoToReload;
        currentWeapon.ammoInMag += ammoToReload;
    }

    bool DoesPlayerHaveAWeaponInSlot(int slot)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == slot)
            {
                if(weapons[i] != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool DoesPlayerHaveATwoHandedWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].isOneHanded)
            {
                return weapons[i].isOneHanded;
            }
        }
        return false;
    }

    void EquipWeapon(int slot)
    {
        if(weapons[slot] != null )
        {
            // Get the weapon
            for (int i = 0; i < weapons.Length; i++)
            {
                if (i == slot)
                {
                    currentHeldWeapon = weapons[i];
                }
            }
            // Setting variables
            player.isWeaponEquipped = true;
            gunShotTimer = currentHeldWeapon.firerate;
            // Set and hide sprites
            weaponInHand.GetComponent<SpriteRenderer>().sprite = weapons[slot].weaponSprite;
            // Animation handling
            player.animator.SetLayerWeight(2, 1);
            player.animator.SetFloat("ShootingSpeed", weapons[slot].animationSpeed);
            player.animator.runtimeAnimatorController = weapons[slot].weaponAnimOverride;
        }
    }

    void DeEquip()
    {
        player.animator.SetLayerWeight(2, 0);
        player.isWeaponEquipped = false;
        player.animator.runtimeAnimatorController = defaultController;
    }

    
}
