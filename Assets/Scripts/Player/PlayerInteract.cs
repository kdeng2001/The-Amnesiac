using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles Player Interact
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    private PlayerManager playerManager;
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void OnEnable() { LevelEventsManager.Instance.onInteract += HandleInteract; }
    private void OnDisable() { LevelEventsManager.Instance.onInteract -= HandleInteract; }
    //public delegate void OnEndConversation();
    //OnEndConversation onEndConversation;

    /// <summary>
    /// Handles conversation and potentially other events that trigger when pressing interact key
    /// </summary>
    public void HandleInteract()
    {
        HandleConversation();
    }
    /// <summary>
    /// Starts conversation, sets states, player cannot initiate a new conversation while one is already in progress
    /// </summary>
    private void InitiateConversation()
    {
        ConversationManager.onStartConversation?.Invoke();
    }
    /// <summary>
    /// Moves the conversation forward or finishes if no more dialogue
    /// </summary>
    private void ContinueConversation()
    {
        if (ConversationManager.onContinueConversation == null) { return; }
        bool continueConvo = ConversationManager.onContinueConversation.Invoke();
        if(!continueConvo) { FinishConversation(); }
    }
    /// <summary>
    /// Unpauses, sets state InConversation to false
    /// </summary>
    private void FinishConversation()
    {
        ConversationManager.onEndConversation?.Invoke();
    }
    /// <summary>
    /// Handles conversation depending on whether the conversation is in progress or hasn't started yet
    /// </summary>
    private void HandleConversation()
    {
        // Conditions to start conversation through interact
        if (ConversationManager.Instance.canStartConverse && !ConversationManager.Instance.inConversation) { InitiateConversation(); }
        else if (ConversationManager.Instance.inConversation) { ContinueConversation(); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject != null && collision.gameObject.TryGetComponent(out DialogueTrigger dT)) { ConversationManager.onUnSetCurrentConversation?.Invoke();  }
        
    }

}
