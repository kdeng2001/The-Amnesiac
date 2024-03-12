using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerJump playerJump;
    [SerializeField] public float speed = 50f;
    [SerializeField] public float baseJumpForce = 5f;
    [SerializeField] public float holdJumpHeight = 15f;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement.Move(speed);
        playerJump.Jump(baseJumpForce, holdJumpHeight);
    }
}
