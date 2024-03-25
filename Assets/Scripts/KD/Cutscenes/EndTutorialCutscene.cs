using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorialCutscene : Cutscene
{
    [SerializeField] Conversation conv;
    float time0 = 0;
    float time3 = 0;
    // Update is called once per frame
    private void OnEnable()
    {
        if(ConversationManager.Instance.currentConversation == conv) 
        {
            Debug.Log("current conversation matches cutscene");
        }
        else { enabled = false; }
    }
    void FixedUpdate()
    {
        if(!ConversationManager.Instance.currentConversation == conv) { return; }
        if(ConversationManager.Instance.currentIndex == 0) 
        {
            if (time0 < conv.dialogueList[0].minAliveTime)
            {
                player.playerMovement.Move(0.5f * player.speed, Vector2.left);
                time0 += Time.fixedDeltaTime;
                //if(player.playerAnimation.currentAnimation == "PlayerMove") { return; }
                //Debug.Log("Set move left");
                //player.playerAnimation.SetAnimation("Move");
                player.playerAnimation.SetAnimationMoveLeft();
                player.playerAnimation.playerAnimator.speed = 0.5f;
            }
            else 
            {
                player.playerAnimation.playerAnimator.speed = 1;
                player.playerMovement.Move(0, Vector2.zero); 
                player.playerAnimation.SetAnimationIdleLeft(); 
            }
        }
        //else if (ConversationManager.Instance.currentIndex == 1) 
        //{
        //}
        else if (ConversationManager.Instance.currentIndex == 3) 
        {
            if(time3 < conv.dialogueList[3].minAliveTime)
            {

                player.playerGlide.Glide(player.birdBasePower, player.birdDecreasePowerRate, player.glideTime, true);
                player.playerJump.Jump(player.baseJumpForce, player.holdJumpForce, true);   
                player.playerMovement.Move(player.speed * 0.2f, Vector2.left);           
                if(time3 > conv.dialogueList[3].minAliveTime/2)
                {
                    player.playerAnimation.SetAnimationGlideLeft();
                }
                time3 += Time.fixedDeltaTime;


            }
            else { player.playerJump.Jump(0, 0, false); }
        }
    }
}
