using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPerkData", menuName = "ScriptableObjects/PerkData")]
public class Perk : ScriptableObject
{
    [Header("Perk Properties")]
    public Sprite perkIcon;
    public string perkName;
}
