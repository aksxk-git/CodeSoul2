using UnityEngine;

public class WeaponsList : MonoBehaviour
{
    public Weapon[] weaponsList;

    private void Start()
    {
        InitWeapons();
    }

    void InitWeapons()
    {
        for (int i = 0; i < weaponsList.Length; i++)
        {
            weaponsList[i].ammoInMag = weaponsList[i].maxMagAmount;
            weaponsList[i].reservedAmmo = weaponsList[i].maxReservedAmmo;
        }
    }
}
