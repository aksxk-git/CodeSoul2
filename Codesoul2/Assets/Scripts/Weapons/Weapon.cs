using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/WeaponData")]
public class Weapon : ScriptableObject
{
    // Weapon Properties
    public Sprite weaponSprite;
    public string weaponName;
    public double damage; // Amount of damage that the gun creates
    public int ammoInMag; // How many bullets are currently in the mag
    public int maxMagAmount; // How many bullets can be stored in the gun
    public float reloadTime; // How long it takes to reload the gun
    public float firerate;
    public bool isHandGun; // Determine where we should place the gun on the hip or back of the character
    public float animationSpeed;

    // Weapon Components
    public RuntimeAnimatorController weaponAnimOverride;
}