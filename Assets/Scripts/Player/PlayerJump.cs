using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerJump : MonoBehaviour
{
    PlayerActionManager playerActionManager;
    PlayerManager playerManager;
    Rigidbody2D rb;

    /// <summary>
    /// variables that determine jump behavior
    /// </summary>
    bool jumping = false;
    float elapseTime = 0;
    float maxHoldTime = .1f;

    bool falling;

    void Awake()
    {
        playerActionManager = GetComponent<PlayerActionManager>();
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
        if(playerManager.jumpCancel) {  LevelEventsManager.Instance.onJumpCancel += JumpCancel; }
       
    }
    /// <summary>
    /// Called every frame to handle Jump behavior
    /// </summary>
    public void Jump(float baseJumpForce, float holdJumpForce)
    {
        if(rb.velocity.y < 0) { falling = true; }
        else { falling = false; }
        if(playerActionManager.jumpValue && playerManager.playerGrounded.IsGrounded() && !jumping)
        {   
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * baseJumpForce, ForceMode2D.Impulse);
            jumping = true;
            elapseTime = 0;
            rb.AddForce(Vector2.up * baseJumpForce);
        }
        else if(playerActionManager.jumpValue == false)
        {
            jumping = false;
        }
        else if(jumping && elapseTime < maxHoldTime && !playerManager.playerGrounded.IsGrounded())
        {
            rb.AddForce(Vector2.up * (holdJumpForce - elapseTime * 10) * 100);
            elapseTime += Time.deltaTime;
        }
        
    }
    private void JumpCancel()
    {
        if(!playerManager.jumpCancel) { return; }
        if(!falling)
        {
            StartCoroutine(delayJumpCancel());
        }

    }
    public IEnumerator delayJumpCancel()
    {
        yield return new WaitForSeconds(playerManager.delayJumpCancelTime);
        if(rb.velocity.y > 0) { rb.velocity = new Vector2(rb.velocity.x, 0); }
        
    }    
    
    /// <summary>
    /// Called to jump programatically
    /// </summary>
    /// <param name="baseJumpForce"></param>
    /// <param name="holdJumpForce"></param>
    /// <param name="jumpValue"> determines if jump is "held" </param>
    public void AutoJump(float baseJumpForce, float holdJumpForce, bool jumpValue)
    {
        if (rb.velocity.y < 0) { falling = true; }
        else { falling = false; }
        if (jumpValue && playerManager.playerGrounded.IsGrounded() && !jumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * baseJumpForce, ForceMode2D.Impulse);
            jumping = true;
            elapseTime = 0;
            rb.AddForce(Vector2.up * baseJumpForce);
        }
        else if (jumpValue == false)
        {
            jumping = false;
        }
        else if (jumping && elapseTime < maxHoldTime && !playerManager.playerGrounded.IsGrounded())
        {
            rb.AddForce(Vector2.up * (holdJumpForce - elapseTime * 10) * 100);
            elapseTime += Time.deltaTime;
        }
    }
}
