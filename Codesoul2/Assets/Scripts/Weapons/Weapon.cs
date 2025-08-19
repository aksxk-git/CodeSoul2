using Unity.VisualScripting;
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
    public int reservedAmmo; // How many bullets that can be reloaded into the gun
    public float reloadTime; // How long it takes to reload the gun
    public float firerate;
    public bool isOneHanded; // Determine where we should place the gun on the hip or back of the character
    public bool isSemiAutomatic;
    public float animationSpeed; // Set the animation speed for the weapon animations

    // Weapon Components
    public RuntimeAnimatorController weaponAnimOverride;
}