using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Image weaponImage;
    public TMP_Text weaponText;

    public void UpdateWeaponUI(Weapon weapon)
    {
        weaponImage.sprite = weapon.weaponSprite;
        weaponText.text = weapon.weaponName;
    }
}
