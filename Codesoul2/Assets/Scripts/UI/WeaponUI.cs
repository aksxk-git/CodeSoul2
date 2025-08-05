using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    // Primary Weapon
    public Image primaryWeaponSprite;
    public TMP_Text primaryWeaponName;

    // Secondary Weapon
    public Image secondaryWeaponSprite;
    public TMP_Text secondaryWeaponName;

    private void Update()
    {
        
    }

    public void UpdatePrimary(Weapon weapon)
    {
        primaryWeaponName.text = weapon.weaponName;
        primaryWeaponSprite.sprite = weapon.weaponSprite;
    }

    public void UpdateSecondary(Weapon weapon)
    {
        secondaryWeaponName.text = weapon.weaponName;
        secondaryWeaponSprite.sprite = weapon.weaponSprite;
    }
}
