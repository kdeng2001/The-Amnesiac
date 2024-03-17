using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles Player Interact
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    private PlayerManager playerManager;
    public bool inDialogueTrigger { get; private set; }
    public DialogueTrigger dialogueTrigger { get; private set; }
    public bool inConversation { get; private set; }
    bool setEvent = false;
    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        inDialogueTrigger = false;
        dialogueTrigger = null;
        inConversation = false;
        
        LevelEventsManager.Instance.onInteract += HandleInteract;
        setEvent = true;
    }

    private void OnEnable()
    {
        if (!setEvent) { return; }
        LevelEventsManager.Instance.onInteract += HandleInteract;

    }
    private void OnDisable()
    {
        LevelEventsManager.Instance.onInteract -= HandleInteract;
    }

    public void HandleInteract()
    {
        // Player initiates conversation
        if(inDialogueTrigger && (dialogueTrigger.conversation.InStart() || dialogueTrigger.conversation.InMiddle())) 
        {
            playerManager.playerRB.velocity = Physics2D.gravity;
            dialogueTrigger.conversation.HandleConversation();
            inConversation = dialogueTrigger.conversation.InMiddle();
        }
        else if(inDialogueTrigger && dialogueTrigger.conversation.InEnd()) { inConversation = dialogueTrigger.conversation.InEnd(); }
    }

    public void SetInDialogueTrigger(bool value)
    {
        inDialogueTrigger = value;
    }
    public void SetDialogueTrigger(DialogueTrigger dt)
    {
        dialogueTrigger = dt;
    }
    public void SetInConversation(bool value) { inConversation = value; }
}
