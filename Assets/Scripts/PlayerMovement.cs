using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerActionManager playerActionManager;
    PlayerManager playerManager;
    Rigidbody2D rb;
    public Vector2 moveVelocity { get; private set; }
    [SerializeField] public float baseDownVelocity = -.1f; 
    float downVelocity = 0;

    void Start()
    {
        playerActionManager = GetComponent<PlayerActionManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(float speed)
    {
        if(playerActionManager.moveValue.y < 0) { downVelocity = baseDownVelocity * speed; }
        else { downVelocity = 0; }
        rb.velocity = moveVelocity = new Vector2(speed * playerActionManager.moveValue.x, rb.velocity.y + downVelocity);
    }
}
