using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlide : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerActionManager playerActionManager;
    Rigidbody2D rb;

    /// <summary>
    /// is true if player is gliding
    /// </summary>
    public bool gliding {get; private set;}
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
    float currentGlidePower;
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        playerActionManager = GetComponent<PlayerActionManager>();
        rb = GetComponent<Rigidbody2D>();
        gliding = false;
    }

    /// <summary>
    /// Called every frame while Gliding to determine glide behavior
    /// </summary>
    public void Glide(float glideBasePower, float birdDecreasePowerRate, float glideTime)
    {
        Debug.Log("gliding var: " + gliding);
        // if grounded, prepare for glide
        gliding = playerActionManager.glideValue;
        if(playerManager.playerGrounded.IsGrounded()){ glideStart = false; gliding = false; }
        
        
        // Handle glide
        if(!playerManager.playerGrounded.IsGrounded() && playerActionManager.glideValue)
        {
            // player only glides when descending
            if(rb.velocity.y > 0) { return; }

            // setup glide variables
            if(!glideStart) { SetUpGlideStart(glideBasePower); }
            else
            {
                // bird loses glide strength over time (linear rate)
                currentGlidePower += (birdDecreasePowerRate * Time.deltaTime);
                GlideCooldown(glideTime);
            }
            // check if glide too long, else playerGlide
            if(glideEnd) 
            {
                gliding = false;
                return; 
            }
            gliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -1 * (currentGlidePower));
        }
    }

    /// <summary>
    /// sets variables that control glide
    /// </summary>
    void SetUpGlideStart(float glideBasePower)
    {
        glideStart = true; 
        elapsedGlideTime = 0f;
        glideEnd = false;
        currentGlidePower = glideBasePower;
    }
    /// <summary>
    /// counts time while glide is active, ends glide if enough time passes
    /// </summary>
    void GlideCooldown(float glideTime)
    {
        if (elapsedGlideTime > glideTime) { return; }
        elapsedGlideTime += Time.deltaTime;
        if(elapsedGlideTime > glideTime) { glideEnd = true; }
    }
}
