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
    public bool inConversation { get; private set; }
    public bool canStartConverse { get; private set; }
    public int currentIndex { get; private set; }
    public Dialogue currentDialogue { get; private set; }
    float startTime = 0;

    private void Awake()
    {
        if (ConversationManager.Instance != null) { Destroy(gameObject); }
        else { ConversationManager.Instance = this; }
        canStartConverse = false;
        inConversation = false;
        onSetCurrentConversation = SetUpConversation;
        onUnSetCurrentConversation = UnSetUpConversation;
        onStartConversation += PrepareConversation;
        onContinueConversation += ContinueConversation;
        onEndConversation += EndConversation;
    }

    private void OnDisable()
    {
        onStartConversation -= PrepareConversation;
        onContinueConversation -= ContinueConversation;
        onEndConversation -= EndConversation;
    }
    /// <summary>
    /// Sets current conversation being had
    /// </summary>
    /// <param name="c"></param>
    private void SetUpConversation(Conversation c) 
    { 
        currentConversation = c;
        canStartConverse = true;
        //Debug.Log("set up conversation: " + c.name);
    }
    /// <summary>
    /// called before conversation begins
    /// </summary>
    private void PrepareConversation()
    {
        currentIndex = 0;
        //canStartConverse = false;
        inConversation = true;
        //Debug.Log("prepare conversation");
        if (currentConversation != null) 
        {
            currentConversation.SetStart();
            PrepareDialogue();
            LevelEventsManager.Instance.PauseActivity();
            onStart?.Invoke();
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
            onContinue?.Invoke();
            //Debug.Log("currentIndex: " + currentIndex);
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
        startTime = Time.time;
        currentDialogue = currentConversation.dialogueList[currentIndex];
        currentDialogue.StartDialogue();
    }

    private void EndConversation()
    {
        currentConversation.SetEnd();
        currentConversation.enabled = false;
        currentConversation = null;
        canStartConverse = false; 
        inConversation = false;
        LevelEventsManager.Instance.UnPauseActivity();
        //Debug.Log("EndConversation event called");
    }

    private void UnSetUpConversation()
    {
        if (currentConversation != null) { currentConversation.enabled = false; }
        currentConversation = null;
        canStartConverse = false;
    }

    /// <summary>
    /// Event sets currentConversation to the c parameter
    /// </summary>
    /// <param name="c"></param>
    public delegate void OnSetCurrentConversation(Conversation c);
    public static OnSetCurrentConversation onSetCurrentConversation;

    public delegate void OnUnSetCurrentConversation();
    public static OnUnSetCurrentConversation onUnSetCurrentConversation;

    public delegate void OnStart();
    public static OnStart onStart;
    /// <summary>
    /// Calls functions that are subscribed to onStartConversation event
    /// </summary>
    public delegate void OnStartConversation();
    public static OnStartConversation onStartConversation;
    /// <summary>
    /// calls functions that are subscribed to the onContinue event
    /// </summary>
    public delegate void OnContinue();
    public static OnContinue onContinue;
    /// <summary>
    /// calls functions that are subscribed to the onContinueConversation event
    /// </summary>
    /// <returns>returns whether the conversation can continue</returns>
    public delegate bool OnContinueConversation();
    public static OnContinueConversation onContinueConversation;
    /// <summary>
    /// calls functions that are subscribed to the onEndConversation event
    /// </summary>
    public delegate void OnEndConversation();
    public static OnEndConversation onEndConversation;
}

