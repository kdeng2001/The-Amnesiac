using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlide : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerActionManager playerActionManager;
    Rigidbody2D rb;
    [SerializeField] float birdBasePower = 5f;
    [SerializeField] float birdDecreasePowerRate = 1f;
    [SerializeField] float glideTime = 2f;

    /// <summary>
    /// is true if player has started gliding
    /// </summary>
    bool glideStart = false;
    /// <summary>
    /// is true if player stopped gliding or ran out of glide time
    /// </summary>
    bool glideEnd = false;
    /// <summary>
    /// time passed while gliding
    /// </summary>
    float elapsedGlideTime = 0f;
    /// <summary>
    /// current effectiveness of glide
    /// </summary>
    float currentBirdPower;
    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerActionManager = GetComponent<PlayerActionManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Glide()
    {
        // if grounded, prepare for glide
        if(playerManager.playerJump.IsGrounded()) { glideStart = false; }
        
        
        // Handle glide
        if(!playerManager.playerJump.IsGrounded() && playerActionManager.glideValue)
        {
            // player only glides when descending
            if(rb.velocity.y > 0) { return; }

            // setup glide variables
            if(!glideStart) { SetUpGlideStart(); }
            else
            {
                // bird loses glide strength over time (linear rate)
                currentBirdPower += (birdDecreasePowerRate * Time.deltaTime);
                GlideCooldown();
            }
            // check if glide too long, else playerGlide
            if(glideEnd) { Debug.Log("Ran out of glide");  return; }
            Debug.Log("gliding at: " + currentBirdPower);
            rb.velocity = new Vector2(rb.velocity.x, -1 * (currentBirdPower));
        }
    }

    void SetUpGlideStart()
    {
        glideStart = true; 
        elapsedGlideTime = 0f;
        glideEnd = false;
        currentBirdPower = birdBasePower;
    }

    void GlideCooldown()
    {
        elapsedGlideTime += Time.deltaTime;
        if(elapsedGlideTime > glideTime) { glideEnd = true; }
    }
}
