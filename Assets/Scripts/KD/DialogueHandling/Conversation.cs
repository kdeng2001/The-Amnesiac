using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Every Conversation should have a group of Child Dialogue objects
/// These Dialogue Objects should be placed in the dialogueList in order 
/// starting from top (1st dialogue) to bottom (last dialogue)
/// </summary>
public class Conversation : MonoBehaviour
{
    [Tooltip("All dialogues in the conversation, in order from start to end.")]
    [SerializeField] public Dialogue[] dialogueList;
    [SerializeField] public bool oneTimeConversation = false;
    [Tooltip("Determines if dialogue starts from a collision or player interaction.")]
    [SerializeField] public bool collisionTrigger = true;
    public bool hasStarted { get; private set; }
    public bool hasEnded { get; private set; }

    private void Awake()
    {
        hasStarted = false;
        hasEnded = false;
    }
    private void OnEnable()
    {
        if(oneTimeConversation && hasEnded) { enabled = false; return; }
        ConversationManager.onSetCurrentConversation?.Invoke(this);
        if(collisionTrigger) { ConversationManager.onStartConversation?.Invoke(); }
    }
    public void SetStart() { hasStarted = true; }
    public void SetEnd() { hasEnded = true; }


}
