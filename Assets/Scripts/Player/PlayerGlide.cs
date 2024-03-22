using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlide : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerActionManager playerActionManager;
    Rigidbody2D rb;

    public delegate void GlidingStart();
    public static GlidingStart glidingStart;
    public delegate void GlidingEnd();
    public static GlidingEnd glidingEnd;

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
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        playerActionManager = GetComponent<PlayerActionManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Called every frame while Gliding to determine glide behavior
    /// </summary>
    public void Glide(float birdBasePower, float birdDecreasePowerRate, float glideTime)
    {
        // if grounded, prepare for glide
        if(playerManager.playerGrounded.IsGrounded()) 
        { 
            if(glideStart) { glidingEnd?.Invoke(); }
            glideStart = false; 
        }
        
        
        // Handle glide
        if(!playerManager.playerGrounded.IsGrounded() && playerActionManager.glideValue)
        {
            // player only glides when descending
            if(rb.velocity.y > 0) { return; }

            // setup glide variables
            if(!glideStart) { SetUpGlideStart(birdBasePower); }
            else
            {
                // bird loses glide strength over time (linear rate)
                currentBirdPower += (birdDecreasePowerRate * Time.deltaTime);
                GlideCooldown(glideTime);
            }
            // check if glide too long, else playerGlide
            if(glideEnd) 
            {
                glidingEnd?.Invoke();
                return; 
            }
            rb.velocity = new Vector2(rb.velocity.x, -1 * (currentBirdPower));
        }
    }

    /// <summary>
    /// sets variables that control glide
    /// </summary>
    void SetUpGlideStart(float birdBasePower)
    {
        glideStart = true; 
        elapsedGlideTime = 0f;
        glideEnd = false;
        currentBirdPower = birdBasePower;
        glidingStart?.Invoke();
    }
    /// <summary>
    /// counts time while glide is active, ends glide if enough time passes
    /// </summary>
    void GlideCooldown(float glideTime)
    {
        if (elapsedGlideTime > glideTime) { return; }
        elapsedGlideTime += Time.deltaTime;
        if(elapsedGlideTime > glideTime) { glideEnd = true; glidingEnd?.Invoke(); }
    }
}
