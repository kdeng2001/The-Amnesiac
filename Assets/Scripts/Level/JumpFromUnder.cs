using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFromUnder : MonoBehaviour
{
    [SerializeField] public Transform fromUnderCheck;
    [SerializeField] public Vector2 platformDimensions = Vector2.zero;
    [SerializeField] public LayerMask player;
    [SerializeField] BoxCollider2D physicalCollider;

    private void Start()
    {
        if(platformDimensions == Vector2.zero) { platformDimensions = GetComponentInChildren<BoxCollider2D>().size; } 
    }

    private void FixedUpdate()
    {
        if(PlayerCanJumpFromUnder()) 
        {
            //Debug.Log("can jump from under");
            physicalCollider.isTrigger = true;
        }
        else
        {
            //Debug.Log("cannot jump from under");
            physicalCollider.isTrigger = false;
        }
    }

    public bool PlayerCanJumpFromUnder()
    {
        if (Physics2D.OverlapBox(fromUnderCheck.position, platformDimensions, 0, player) == null) { return false; }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(fromUnderCheck.position, platformDimensions);
    }
}
