using UnityEngine;

public class Pistol : Weapon
{
    // Weapon Override
    [SerializeField] RuntimeAnimatorController controller;

    private void Start()
    {
        SetWeaponOverride(controller);
        SetWeaponDamage(5);
        SetWeaponMaxMagAmount(8);
        SetWeaponReloadTime(1);
    }
}
