using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place in any dialogue object container
/// </summary>
public class Dialogue : MonoBehaviour
{
    bool start = true;
    public Transform spawnPosition {get; private set; }
    public bool interactDialogue {get; private set; }
    public bool timedDialogue { get; private set; }
    public float durationOfDialogue { get; private set; }
    [System.NonSerialized] public PlayerActionManager playerActionManager;
    

    public float elapsedTime { get; private set; }
    public virtual void Start()
    {
        playerActionManager = GameObject.Find("Player").GetComponent<PlayerActionManager>();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Starts dialgoue
    /// </summary>
    public virtual void OnEnable()
    {
        if(start) { start = false; return; }
        if(timedDialogue) { elapsedTime = 0; }
        if(transform.parent.GetComponent<DialogueChain>() == null) { transform.position = spawnPosition.position; }
        else { transform.position = Vector3.zero; }
        if (playerActionManager != null) { playerActionManager.SetInteract(false); }
    }

    /// <summary>
    /// Handles timed dialogue
    /// </summary>
    private void Update()
    {
        // Interact Dialogue
        if(!timedDialogue) { HandleInteractDialogue(); }
        // Timed Dialogue
        else { HandleTimedDialogue(); }
    }

    public virtual void HandleInteractDialogue()
    {
        if(playerActionManager.interactValue)
        {        
            playerActionManager.SetInteract(false);
            FinishDialogue(); 
        }
        //playerActionManager.SetInteract(false);
        //FinishDialogue();

    }
    public virtual void HandleTimedDialogue()
    {
        if(elapsedTime > durationOfDialogue) { FinishDialogue(); }
        else if(playerActionManager.interactValue) 
        {
            playerActionManager.SetInteract(false);
            FinishDialogue();
        }
        else { Timer(); }
    }

    public void Timer()
    {
        elapsedTime += Time.deltaTime;
    }


    /// <summary>
    /// Call to remove dialogue when finished
    /// </summary>
    public virtual void FinishDialogue()
    {
        gameObject.SetActive(false);
    }

    public virtual void SetVariables(Transform sp, bool id, bool td, float dod)
    {
        spawnPosition = sp;
        interactDialogue = id;
        timedDialogue = td;
        durationOfDialogue = dod;
    }
}
