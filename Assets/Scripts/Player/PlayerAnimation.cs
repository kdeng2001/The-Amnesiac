using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerActionManager playerActionManager;
    SpriteRenderer sprite;
    void Start()
    {
        playerActionManager = GetComponent<PlayerActionManager>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void HandlePlayerMoveAnimation()
    {
        SetAnimationLeft();
        SetAnimationRight();
    }

    public void SetAnimationRight()
    {
        if (playerActionManager.moveValue.x > 0) { sprite.flipX = false; }
    }

    public void SetAnimationLeft()
    {
        if (playerActionManager.moveValue.x < 0) { sprite.flipX = true; }
    }
}
