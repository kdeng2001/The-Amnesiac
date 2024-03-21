using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles Player Interact
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    private PlayerManager playerManager;
    public bool inConversation { get; private set; }
    public bool canStartConverse { get; private set; }
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        inConversation = false;
    }

    private void OnEnable() { LevelEventsManager.Instance.onInteract += HandleInteract; }

    private void OnDisable() { LevelEventsManager.Instance.onInteract -= HandleInteract; }

    public delegate void OnEndConversation();
    OnEndConversation onEndConversation;

    public void HandleInteract()
    {
        HandleConversation();
    }

    private void InitiateConversation()
    {
        LevelEventsManager.Instance.PauseActivity();
        ConversationManager.onStartConversation?.Invoke();
        SetInConversation(true);
        SetCanStartConverse(false);
    }
    private void ContinueConversation()
    {
        if (ConversationManager.onContinueConversation == null) { return; }
        bool continueConvo = ConversationManager.onContinueConversation.Invoke();
        if(!continueConvo) { FinishConversation(); }
    }
    private void FinishConversation()
    {
        LevelEventsManager.Instance.UnPauseActivity();
        ConversationManager.onEndConversation?.Invoke();
        SetInConversation(false);
    }

    private void HandleConversation()
    {
        // Conditions to start conversation through interact
        if (canStartConverse && !inConversation) { InitiateConversation(); }
        else if (inConversation) { ContinueConversation(); }
    }
    public void SetInConversation(bool value) { inConversation = value; }
    public void SetCanStartConverse(bool value) { canStartConverse = value; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out DialogueTrigger dT)) 
        {
            if(!dT.gameObject.activeSelf) { return; }
            SetCanStartConverse(true);             
            if(!dT.conversation.enabled) { return; }
            if(dT.collisionTrigger && !inConversation) {Debug.Log("(enter)Initiate convo with " + dT.name); InitiateConversation(); }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DialogueTrigger dT))
        {
            Debug.Log("dT collision stay");
            if((!dT.collisionTrigger && !inConversation) /*|| (ConversationManager.Instance.hasEnded && !dT.conversation.oneTimeConversation)*/)
            {
                SetCanStartConverse(true);
                Debug.Log("canStartConverse: "+  canStartConverse);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DialogueTrigger dT)) { canStartConverse = false; }
    }

}
