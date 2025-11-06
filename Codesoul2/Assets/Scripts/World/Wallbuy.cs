using UnityEngine;

public class Wallbuy : MonoBehaviour
{
    public GameManager gm;
    public InteractUI ui;
    public int cost;
    public int ammoCost;
    public Weapon weapon;
    bool inRange;
    string text;

    private void Start()
    {
        text = "Press and hold F to purchase " + weapon.weaponName + " for " + cost + " points!";
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            if (gm.SearchForWeaponOnPlayer(weapon))
            {
                
            }
            else
            {
                Purchase();
            }
        }
    }

    public void Purchase()
    {
        
    }

    public void PurchaseAmmo(Weapon weapon)
    {
        gm.playerScore -= ammoCost;
        weapon.reservedAmmo = weapon.maxReservedAmmo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
