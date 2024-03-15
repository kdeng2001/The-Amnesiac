using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerActionManager playerActionManager;
    Rigidbody2D rb;

    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundCheckRadius = 1;
    [SerializeField] public int groundCheckLayerMask;

    bool jumping = false;
    float elapseTime = 0;
    float maxHoldTime = .1f;


    void Start()
    {
        playerActionManager = GetComponent<PlayerActionManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump(float baseJumpForce, float holdJumpHeight)
    {
        if(playerActionManager.jumpValue && IsGrounded() && !jumping)
        {
            //Debug.Log("jump");
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
        else if(jumping && elapseTime < maxHoldTime && !IsGrounded())
        {
            //rb.AddForce(Vector2.up * ((jumpHeight - elapseTime * 2) * 100));
            rb.AddForce(Vector2.up * (holdJumpHeight - elapseTime * 10) * 100);
            elapseTime += Time.deltaTime;
        }
        
    }

    public bool IsGrounded()
    {
        if(Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundCheckLayerMask) == null) { return false; }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
