using UnityEngine;

public class Wallbuy : MonoBehaviour
{
    // Dependency references
    GameManager gm;
    InteractUI ui;
    WeaponManager wm;

    // Wallbuy stats
    [Header("Wallbuy Stats")]
    public Weapon weapon;
    public int cost;
    public int ammoCost;

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

    private void Start()
    {
        // Change chalkoutline
        gameObject.GetComponent<SpriteRenderer>().sprite = weapon.weaponChalkSprite;
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

    public void PurchaseAmmo(Weapon weapon)
    {
        if (wm.GetCurrentWeapon().reservedAmmo != wm.GetCurrentWeapon().maxReservedAmmo && wm.GetCurrentWeapon() == weapon)
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
