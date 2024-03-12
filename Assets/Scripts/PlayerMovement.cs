using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerActionManager playerActionManager;
    PlayerManager playerManager;
    Rigidbody2D rb;
    public Vector2 moveVelocity { get; private set; }

    void Start()
    {
        playerActionManager = GetComponent<PlayerActionManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(float speed)
    {
        rb.velocity = moveVelocity = new Vector2(speed * playerActionManager.moveValue.x, rb.velocity.y);
    }
}
