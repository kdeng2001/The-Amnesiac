using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    /// <summary>
    /// Transform under player
    /// </summary>
    [SerializeField] public Transform groundCheck;
    /// <summary>
    /// radius of ground check
    /// </summary>
    [SerializeField] public float groundCheckRadius = 1;
    /// <summary>
    /// layers that are considered "ground"
    /// </summary>
    [SerializeField] public LayerMask groundCheckLayerMask;
    
    /// <summary>
    /// returns true if player is touching ground, else returns false
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundCheckLayerMask) == null) { return false; }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
