using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueManager : MonoBehaviour
{
    /// <summary>
    /// All variables necessary for minor dialogue to work
    /// </summary>
    [SerializeField] GameObject[] dialogueObject;
    [SerializeField] DialogueTrigger[] dialogueTrigger;
    [SerializeField] Transform[] spawnPosition;
    [SerializeField] bool[] interactDialogue;
    [SerializeField] bool[] timedDialogue;
    [SerializeField] float[] durationOfDialogue;
    
    LevelEventsManager levelEventsManager;
    bool setEvent = false;

    private void Awake()
    {
        levelEventsManager = FindAnyObjectByType<LevelEventsManager>();
        levelEventsManager.onTriggerDialogue += StartDialogue;
        setEvent = true;
    }

    private void Start()
    {
        for(int i=0; i<dialogueObject.Length; i++)
        {
            dialogueObject[i].GetComponent<Dialogue>()
                .SetVariables(
                spawnPosition[i],
                interactDialogue[i],
                timedDialogue[i],
                durationOfDialogue[i]
                );
            dialogueTrigger[i].SetTriggerNumber(i);
        }
    }

    private void OnEnable()
    {
        if(!setEvent) { return; }
        levelEventsManager.onTriggerDialogue += StartDialogue;
    }
    private void OnDisable()
    {
        if(!setEvent) { return; }
        levelEventsManager.onTriggerDialogue -= StartDialogue;
    }

    void StartDialogue(int dialogueNumber)
    {
        Debug.Log("create dialogue");
        dialogueObject[dialogueNumber].SetActive(true);
    }
}
