using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Game stats
    public int playerScore;
    public int wave;
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

    public void SetPlayerWeapon(int slot, Weapon weapon)
    {
        player.GetComponent<WeaponManager>().weapons[slot] = weapon;
    }

    public bool HasPlayerGotThisWeapon(Weapon weapon)
    {
        for (int i = 0; i < player.GetComponent<WeaponManager>().weapons.Length; i++)
        {
            if (player.GetComponent<WeaponManager>().weapons[i] == weapon)
            {
                return true; 
            }
        }

        return false;
    }

    public bool HasPlayerGotASecondary()
    {
        if (player.GetComponent<WeaponManager>().weapons[1] != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RewardPointsOnHit()
    {
        playerScore += 10;
    }

    public void RewardPointsOnKill()
    {
        playerScore += 60; 
    }
}
