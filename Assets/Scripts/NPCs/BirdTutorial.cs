using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTutorial : Conversation
{
    GameObject player;
    PlayerAnimation playerAnimation;
    BirdNPC bird;

    public delegate void CanFollowPlayer();
    public static CanFollowPlayer canFollowPlayer;
    public override void OnEnable()
    {
        ConversationManager.onStartConversation += HandleStartCutScene;
        base.OnEnable();
        ConversationManager.onContinue += HandleContinueCutScene;
        ConversationManager.onEndConversation += HandleEndCutScene;
    }
    private void OnDisable()
    {
        ConversationManager.onStart -= HandleStartCutScene;
        ConversationManager.onContinue -= HandleContinueCutScene;
        ConversationManager.onEndConversation -= HandleEndCutScene;
    }

    private void Awake()
    {
        player = GameObject.Find("Player");
        bird = GameObject.Find("NPCBird").GetComponent<BirdNPC>();
    }

    public void HandleStartCutScene() 
    {
        Debug.Log("start cutscene");
        if (!ConversationManager.Instance.currentConversation == this) { return; }
        Debug.Log("set idle left");
        playerAnimation = player.GetComponent<PlayerManager>().playerAnimation;
        playerAnimation.SetAnimationIdleLeft();
        playerAnimation.SetDirection("Left");
        
    }
    public void HandleContinueCutScene() 
    {
        Debug.Log("continue cutscene");
        if (!ConversationManager.Instance.currentConversation == this) { return; }
        if (ConversationManager.Instance.currentIndex == 1) { }
    }
    public void HandleEndCutScene() 
    {
        //if (!ConversationManager.Instance.currentConversation == this) { return; }
        Debug.Log("end cutscene");
        player.GetComponent<PlayerManager>().canGlide = true;
        canFollowPlayer?.Invoke();
        Debug.Log("can glide");
        enabled = false; 
    }
}
