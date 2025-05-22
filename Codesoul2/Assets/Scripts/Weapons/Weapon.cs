using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Weapon Properties
    protected double damage; // Amount of damage that the gun creates
    protected int ammoInMag; // How many bullets are currently in the mag
    protected int maxMagAmount; // How many bullets can be stored in the gun
    protected float reloadTime; // How long it takes to reload the gun
    
    // Weapon Components
    protected RuntimeAnimatorController weaponAnimOverride;

    protected void SetWeaponDamage(float damage)
    {
        this.damage = damage;
    }

    protected double GetWeaponDamage()
    {
        return this.damage;
    }

    protected void SetWeaponMaxMagAmount(int maxMagAmount)
    {
        this.maxMagAmount = maxMagAmount;
        this.ammoInMag = maxMagAmount;
    }

    protected void SetWeaponReloadTime(float reloadTime)
    {
        this.reloadTime = reloadTime;
    }

    protected float GetWeaponReloadTime()
    {
        return this.reloadTime;
    }

    protected void SetWeaponOverride(RuntimeAnimatorController weaponAnimOverride)
    {
        this.weaponAnimOverride = weaponAnimOverride;
    }

    protected RuntimeAnimatorController GetWeaponOverride()
    {
        return this.weaponAnimOverride;
    }
}
