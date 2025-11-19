using System.Collections;
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
    public Weapon[] weapons;
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
        currentHeldWeapon = weapons[0];

        EquipWeapon(0);
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
        player.animator.SetBool("IsShooting", true);
        DepleteAmmo(currentHeldWeapon);

        GameObject defaultBullet = Instantiate(currentHeldWeapon.projectile, weaponFirepoint.transform.position, Quaternion.identity);
        defaultBullet.GetComponent<DefaultBullet>().velocity = new Vector2(transform.position.x - player.mousePosition.x, transform.position.y - player.mousePosition.y);

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

        // Update UI
        if (DoesPlayerHaveAWeaponInSlot(0))
        {
            weaponUI.UpdatePrimary(currentHeldWeapon);
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
        
        if (Input.GetKeyDown(KeyCode.R) && currentHeldWeapon.ammoInMag < currentHeldWeapon.maxMagAmount && currentHeldWeapon.reservedAmmo != 0)
        {
            isReloading = true;
            player.animator.SetBool("IsReloading", true);
        }

        if (isReloading)
        {
            gunReloadTimer += Time.deltaTime;
        }
        else
        {
            gunReloadTimer = 0;
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

    public Weapon GetCurrentWeapon()
    {
        return currentHeldWeapon;
    }
    
}
