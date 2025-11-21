using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // Listener
    [SerializeField] Player player;
    [SerializeField] WeaponManager weaponManager;

    // Default animator
    RuntimeAnimatorController defaultController;

    // Blink
    float blinkTime = 5; // Every 5 seconds the player blinks
    float blinkTimer; // Track time elapsed

    private void Update()
    {
        Animate();
    }

    public void Animate()
    {
        // ANIMATE BLINKING
        if (blinkTimer > blinkTime)
        {
            blinkTimer = 0;
        }
        else
        {
            blinkTimer += Time.deltaTime;
        }

        // Update animator 
        player.animator.SetBool("IsMoving", player.IsMoving());
        player.animator.SetBool("IsSprinting", player.isSprinting);
        player.animator.SetBool("IsWalkingBackward", player.isWalkingBackward);
        player.animator.SetFloat("BlinkTimer", blinkTimer);

        // Animate Crouching
        if (player.isCrouched)
        {
            player.animator.SetLayerWeight(1, 1);
        }
        else
        {
            player.animator.SetLayerWeight(1, 0);
        }
    }
}
