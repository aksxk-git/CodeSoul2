using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    // Primary Weapon
    public Image weaponSprite;
    public TMP_Text weaponName;
    public TMP_Text weaponMagCount;
    public TMP_Text weaponReserveCount;

    private void Update()
    {
        
    }

    public void UpdatePrimary(Weapon weapon)
    {
        weaponName.text = weapon.weaponName;
        weaponSprite.sprite = weapon.weaponSprite;
        weaponMagCount.text = weapon.ammoInMag.ToString();
        weaponReserveCount.text = weapon.reservedAmmo.ToString();
    }
}
