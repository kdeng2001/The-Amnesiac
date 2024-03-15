using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place in any dialogue object container
/// </summary>
public class Dialogue : MonoBehaviour
{
    bool start = true;
    private Transform spawnPosition;
    private bool interactDialogue;
    private bool timedDialogue;
    private float durationOfDialogue;
    

    float elapsedTime;
    void Start()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Starts dialgoue
    /// </summary>
    private void OnEnable()
    {
        if(start) { start = false; return; }
        if(timedDialogue) { elapsedTime = 0; }
        transform.position = spawnPosition.position;
        
    }

    /// <summary>
    /// Handles timed dialogue
    /// </summary>
    private void Update()
    {
        if(!timedDialogue) { return; }
        if(elapsedTime > durationOfDialogue) 
        {
            FinishDialogue();
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
    }
    /// <summary>
    /// Call to remove dialogue when finished
    /// </summary>
    public void FinishDialogue()
    {
        gameObject.SetActive(false);
    }

    public void SetVariables(Transform sp, bool id, bool td, float dod)
    {
        spawnPosition = sp;
        interactDialogue = id;
        timedDialogue = td;
        durationOfDialogue = dod;
    }
}
