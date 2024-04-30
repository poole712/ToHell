using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Animator animator;
    private bool isCharging;
    private float chargeTimer;
    public float maxChargeTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the player presses the "Jump" button (e.g., spacebar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Trigger the "Jump" animation
            animator.SetTrigger("Jump");
        }
        // if the player clicks right 
        if (Input.GetMouseButton(0))
        {
            // Start charging if not already charging
            if (!isCharging)
            {
                isCharging = true;
                // Trigger the "Hold" animation
                animator.SetTrigger("Hold");
            }
        }
        else // If the mouse button is released
        {
            // If the player was charging, trigger the "Release" animation
            if (isCharging)
            {
                isCharging = false;
                animator.SetTrigger("Attack");
            }

        }
    }
}
