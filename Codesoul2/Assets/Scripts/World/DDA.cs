using UnityEngine;

public class DDA : MonoBehaviour
{
    // Enable
    public bool activated;
    // Player reference
    Player player;
    WeaponManager weapon;
    // Player variables
    int playerHP;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


}
