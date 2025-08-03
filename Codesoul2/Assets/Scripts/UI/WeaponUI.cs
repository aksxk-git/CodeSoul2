using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    // Primary Weapon
    public Sprite primaryWeaponSprite;
    public TMP_Text primaryWeaponName;

    // Secondary Weapon
    public Sprite secondaryWeaponSprite;
    public TMP_Text secondaryWeaponName;

    private void Update()
    {
        
    }

    public void UpdatePrimary(Weapon weapon)
    {
        primaryWeaponName.text = weapon.name;
        primaryWeaponSprite = weapon.weaponSprite;
    }

    public void UpdateSecondary(Weapon weapon)
    {
        secondaryWeaponName.text = weapon.name;
        secondaryWeaponSprite = weapon.weaponSprite;
    }
}
