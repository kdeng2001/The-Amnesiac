using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFromUnder : MonoBehaviour
{
    [SerializeField] public Transform fromUnderCheck;
    [SerializeField] public Vector2 platformDimensions = Vector2.zero;
    [SerializeField] public LayerMask player;
    private void Start()
    {
        if(platformDimensions == Vector2.zero) { platformDimensions = GetComponentInChildren<BoxCollider2D>().size; } 
    }
    public bool PlayerCanJumpFromUnder()
    {
        if (Physics2D.OverlapBox(fromUnderCheck.position, platformDimensions, 0) == null) { return false; }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(fromUnderCheck.position, platformDimensions);
    }
}
