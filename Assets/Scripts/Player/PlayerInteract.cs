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
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        inDialogueTrigger = false;
        dialogueTrigger = null;
        inConversation = false;
    }

    private void OnEnable()
    {
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
            Debug.Log("is null conversation:" + (dialogueTrigger == null));
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
