using UnityEngine;

public class Wallbuy : MonoBehaviour
{
    public InteractUI ui;

    public int cost;
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
            Purchase();
        }
    }

    public void Purchase()
    {
        Debug.Log("Purchased " + weapon.weaponName + " for " + cost + " points!");
    }

    public void PurchaseAmmo()
    {

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
