using UnityEngine;
using UnityEngine.UI;

public class PerkUI : MonoBehaviour
{
    public Perk perk;
    public GameObject perkIcon;

    private void Start()
    {
        AddPerkIconToUI(perk);
    }

    public void AddPerkIconToUI(Perk perk)
    {
        Instantiate(perkIcon, gameObject.transform);
        perkIcon.GetComponent<Image>().sprite = perk.perkIcon;
    }
}
