using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdNPC : NPC
{
    [SerializeField] Transform target;
    [SerializeField] bool followPlayer = false;
    [SerializeField] SpriteRenderer playerSprite;
    public override void Start()
    {
        base.Start();
        BirdTutorial.canFollowPlayer += FollowPlayer;
    }
    private void FixedUpdate()
    {
        if(followPlayer) 
        { 
            Move(target);         
            if(transform.position.x > target.position.x) { sprite.flipX = false; }
            else { sprite.flipX = true; }
        }
        if(transform.position == target.position)
        {
            sprite.flipX = !playerSprite.flipX;
        }

    }

    public void FollowPlayer() 
    {
        animator.Play("Move");
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        playerSprite = target.gameObject.GetComponentInChildren<SpriteRenderer>();
        followPlayer = true;
        //PlayerGlide.glidingStart += GlidingStart;
        //PlayerGlide.glidingEnd += GlidingEnd;
    }

    public void GlidingStart()
    {
        speed *= 30;
        Debug.Log("glide start");
    }
    public void GlidingEnd()
    {
        speed /= 30;
        Debug.Log("glide end");
    }
}
