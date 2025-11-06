using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Game stats
    public int playerScore;
    // Game rules
    public int startingScore;
    public int perkLimit;
    // Player
    public Player player;

    private void Start()
    {
        playerScore = startingScore;
    }

    public Weapon GetPlayerWeapon(int slot)
    {
        return player.GetComponent<WeaponManager>().weapons[slot];
    }

    public bool SearchForWeaponOnPlayer(Weapon weapon)
    {
        for (int i = 0; i < player.GetComponent<WeaponManager>().weapons.Length; i++)
        {
            if (weapon.weaponName == player.GetComponent<WeaponManager>().weapons[i].weaponName)
            {
                return true;
            }
        }

        return false;
    }

    public bool CompareWeapon(Weapon weapon1, Weapon weapon2)
    {
        if (weapon1 == weapon2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetPlayerWeapon(int slot, Weapon weapon)
    {
        player.GetComponent<WeaponManager>().weapons[slot] = weapon;
    }
}
