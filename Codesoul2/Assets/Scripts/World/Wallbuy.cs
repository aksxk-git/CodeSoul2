using UnityEngine;

public class Wallbuy : MonoBehaviour
{
    GameManager gm;
    InteractUI ui;
    public int cost;
    public int ammoCost;
    public Weapon weapon;
    bool inRange;
    string text;

    private void Start()
    {
        // Get script references
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        ui = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<InteractUI>();

        cost = weapon.weaponCost;
        ammoCost = weapon.ammoCost;
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            if (gm.HasPlayerGotThisWeapon(weapon))
            {
                PurchaseAmmo(weapon);
            }
            else
            {
                Purchase(weapon);
            }
        }

        UpdateDisplayText();
    }

    void UpdateDisplayText()
    {
        if (gm.HasPlayerGotThisWeapon(weapon))
        {
            text = "Press and hold F to purchase ammo for " + weapon.weaponName + " for " + cost + " points!";
        }
        else
        {
            text = "Press and hold F to purchase " + weapon.weaponName + " for " + cost + " points!";
        }
    }

    public void Purchase(Weapon weapon)
    {
        gm.playerScore -= weapon.weaponCost;
        weapon.reservedAmmo = weapon.maxReservedAmmo;

        if (!gm.HasPlayerGotASecondary())
        {
            gm.SetPlayerWeapon(1, weapon);
        }
        else
        {
            gm.SetPlayerWeapon(gm.player.GetComponent<WeaponManager>().currentWeaponSlot, weapon);
        }
    }

    public void PurchaseAmmo(Weapon weapon)
    {
        if (gm.player.GetComponent<WeaponManager>().GetCurrentWeapon().reservedAmmo != gm.player.GetComponent<WeaponManager>().GetCurrentWeapon().maxReservedAmmo &&
            gm.player.GetComponent<WeaponManager>().GetCurrentWeapon() == weapon)
        {
            gm.playerScore -= ammoCost;
            weapon.reservedAmmo = weapon.maxReservedAmmo;
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
