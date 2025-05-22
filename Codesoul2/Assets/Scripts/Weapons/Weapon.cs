using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Weapon Properties
    double damage; // Amount of damage that the gun creates
    int ammoInMag; // How many bullets are currently in the mag
    int maxMagAmount; // How many bullets can be stored in the gun
    float reloadTime; // How long it takes to reload the gun
    bool twoHander; // Does the weapon require two or just one hand
    
    // Weapon Components
    RuntimeAnimatorController weaponAnimOverride;

    private void Start()
    {
        
    }
}
