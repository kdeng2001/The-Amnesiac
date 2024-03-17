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
    [SerializeField] Dialogue[] dialogueList;

    bool start = true;
    bool middle = false;
    bool end = false;
    int currentPosition = 0;

    public void HandleConversation()
    {
        /* 
         * should I include a check for interact action,
         * or have this be called on an event?
        */
        if(start && currentPosition==0) { StartConversation(); }
        else { NextDialogue(); }
    }

    /// <summary>
    /// starts conversation
    /// </summary>
    public void StartConversation() 
    {
        PauseActivity();
        currentPosition = 0;
        start = false;
        middle = true;
        end = false;
        dialogueList[currentPosition].StartDialogue();

    }
    /// <summary>
    /// moves conversation forward
    /// </summary>
    public void NextDialogue() 
    {

        dialogueList[currentPosition].EndDialogue();
        // reach end of dialogue, or start next dialogue
        if (++currentPosition >= dialogueList.Length)
        {
            EndConversation();
            return;
        }
        else { dialogueList[currentPosition].StartDialogue(); }
    }
    /// <summary>
    /// ends the conversation
    /// </summary>
    public void EndConversation()
    {
        currentPosition = 0;
        start = true;
        middle = false;
        end = true;
        UnPauseActivity();
    }
    public bool InStart() { return start; }
    public bool InMiddle() { return middle; }
    public bool InEnd() { return end; }
    /// <summary>
    /// pauses movement of players, platforms, etc... while dialogue is happening
    /// </summary>
    public void PauseActivity()
    {

    }

    public void UnPauseActivity()
    {

    }
}
