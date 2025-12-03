using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/WeaponData")]
public class Weapon : ScriptableObject
{
    // Weapon Properties
    [Header("Weapon Properties")]
    public Sprite weaponSprite;
    public Sprite weaponChalkSprite; // Sprite used for chalk outline on wallbuys
    public string weaponName;
    public float damage; // Amount of damage that the gun creates
    public int ammoInMag; // How many bullets are currently in the mag
    public int maxMagAmount; // How many bullets can be stored in the gun
    public int reservedAmmo; // How many bullets that can be reloaded into the gun
    public int maxReservedAmmo;
    public float reloadTime; // How long it takes to reload the gun
    public float firerate;
    public bool isOneHanded; // Determine where we should place the gun on the hip or back of the character
    public bool isSemiAutomatic;
    public float animationSpeed; // Set the animation speed for the weapon animations

    // Weapon Components
    [Header("Weapon Components")]
    public GameObject projectile;
    public RuntimeAnimatorController weaponAnimOverride;

    // Audio
    [Header("Audio")]
    public AudioClip shotSFX; // Sound effect for the gun when shot
    public AudioClip noAmmoSFX; // Sound effect for the gun when theres no ammo and you try to shoot
}