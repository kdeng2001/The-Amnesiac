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
    void Start()
    {
        playerActionManager = GetComponent<PlayerActionManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump(float jumpHeight)
    {
        if(playerActionManager.jumpValue && IsGrounded())
        {
            //Debug.Log("jump");
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
        
    }

    bool IsGrounded()
    {
        if(Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundCheckLayerMask) == null) { return false; }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
