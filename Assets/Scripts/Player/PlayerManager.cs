using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMovement { get; private set; }
    public PlayerJump playerJump {get; private set;}
    public PlayerGlide playerGlide { get; private set; }
    public PlayerGrounded playerGrounded { get; private set; }
    public PlayerAnimation playerAnimation { get; private set; }
    /// <summary>
    /// playerMovement depends on speed
    /// </summary>
    [Tooltip("Determines movement speed")]
    [SerializeField] public float speed = 50f;
    /// <summary>
    /// playerJump depends on baseJumpForce and holdJumpHeight
    /// </summary>
    [Tooltip("Determines jump height without hold")]
    [SerializeField] public float baseJumpForce = 5f;
    [Tooltip("Determines jump height from holding")]
    [SerializeField] public float holdJumpHeight = 15f;
    /// <summary>
    /// playerGlide depends on birdBasePower, birdDecreasePowerRate, glideTime
    /// </summary>
    [Tooltip("Determines initial glide strength")]
    [SerializeField] public float birdBasePower = 5f;
    [Tooltip("Determines rate at which glide strength decreases")]
    [SerializeField] public float birdDecreasePowerRate = 1f;
    [Tooltip("Determines duration of gliding before player begins falling normally")]
    [SerializeField] public float glideTime = 2f;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerGlide = GetComponent<PlayerGlide>();
        playerGrounded = GetComponent<PlayerGrounded>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement.Move(speed);
        playerJump.Jump(baseJumpForce, holdJumpHeight);
        playerGlide.Glide(birdBasePower, birdDecreasePowerRate, glideTime);
        playerAnimation.HandlePlayerMoveAnimation();
    }
}
