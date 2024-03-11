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
        moveVelocity = new Vector2(playerActionManager.moveValue.x,0);
        rb.velocity = speed * Time.deltaTime * moveVelocity;
    }
}
