using UnityEngine;

public class PerkMachine : MonoBehaviour
{
    // Dependency references
    GameManager gm;
    InteractUI ui;
    WeaponManager wm;

    // Wallbuy stats
    [Header("Perk Machine Config")]
    public Perk perk;
    public int cost;

    // Interaction
    bool inRange;
    string text;

    private void Awake()
    {
        // Get script references
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        ui = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<InteractUI>();
        wm = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponManager>();
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            // if player has perk already
            //Purchase();
        }

        UpdateDisplayText();
    }

    void UpdateDisplayText()
    {
        /*if (gm.HasPlayerGotThisWeapon(weapon))
        {
            text = "Press and hold F to purchase ammo for " + weapon.weaponName + " for " + cost + " points!";
        }
        else
        {
            text = "Press and hold F to purchase " + weapon.weaponName + " for " + cost + " points!";
        }*/
    }

    public void Purchase(Weapon weapon)
    {
        gm.playerScore -= cost;
        weapon.reservedAmmo = weapon.maxReservedAmmo;

        if (!gm.HasPlayerGotASecondary())
        {
            gm.SetPlayerWeapon(1, weapon);
        }
        else
        {
            gm.SetPlayerWeapon(wm.currentWeaponSlot, weapon);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            ui.ShowPrompt(text);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            ui.ShowPrompt(text);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            ui.HidePrompt();
        }
    }
}
