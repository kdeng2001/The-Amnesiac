using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    public static ConversationManager Instance;
    public Conversation currentConversation;

    /// <summary>
    /// These variables handle conversation and dialogue switching
    /// </summary>
    public int currentIndex { get; private set; }
    public Dialogue currentDialogue { get; private set; }
    float startTime = 0;

    private void Awake()
    {
        if (ConversationManager.Instance != null) { Destroy(gameObject); }
        else { ConversationManager.Instance = this; }
        onSetCurrentConversation = SetCurrentConversation;
        onStartConversation += PrepareConversation;
        onContinueConversation += ContinueConversation;
        onEndConversation += EndConversation;
    }
    /// <summary>
    /// Sets current conversation being had
    /// </summary>
    /// <param name="c"></param>
    private void SetCurrentConversation(Conversation c) { currentConversation = c; }
    /// <summary>
    /// called before conversation begins
    /// </summary>
    private void PrepareConversation()
    {
        currentIndex = 0;
        Debug.Log("prepare conversation");
        if (currentConversation != null) 
        {
            currentConversation.SetStart();
            PrepareDialogue(); 
        }
    }
    /// <summary>
    /// Continues conversation
    /// </summary>
    /// <returns> true if conversation continues, false otherwise </returns>
    private bool ContinueConversation()
    {
        if (currentDialogue.minAliveTime > (Time.time - startTime)) { return true; }
        else { currentDialogue.EndDialogue(); }
        if (currentIndex < currentConversation.dialogueList.Length - 1) 
        {
            currentIndex++;
            Debug.Log("currentIndex: " + currentIndex);
            PrepareDialogue();
            return true;
        }
        return false;
    }
    /// <summary>
    /// called right when a new dialogue should be displayed
    /// </summary>
    private void PrepareDialogue() 
    {
        Debug.Log("currentIndex: " + currentIndex);
        startTime = Time.time;
        currentDialogue = currentConversation.dialogueList[currentIndex];
        currentDialogue.StartDialogue();
    }

    private void EndConversation()
    {
        currentConversation.SetEnd();
        if(currentConversation.oneTimeConversation) { currentConversation = null; }
    }

    public delegate void OnSetCurrentConversation(Conversation c);
    public static OnSetCurrentConversation onSetCurrentConversation;

    public delegate void OnStartConversation();
    public static OnStartConversation onStartConversation;

    public delegate bool OnContinueConversation();
    public static OnContinueConversation onContinueConversation;

    public delegate void OnEndConversation();
    public static OnEndConversation onEndConversation;
}

