using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueManager : MonoBehaviour
{
    /// <summary>
    /// All variables necessary for minor dialogue to work
    /// </summary>
    [Tooltip("DialogueObjects are objects with the Dialogue(script) and should contain a dialogue bubble and text.")]
    [SerializeField] GameObject[] dialogueObject;
    [Tooltip("Dialogue Triggers are placed on GameObjects that trigger dialogue." +
        " Example: If picking up a memory shard triggers dialogue, make sure the memory shard " +
        "has a DialogueTrigger(script)."
        )]
    [SerializeField] DialogueTrigger[] dialogueTrigger;
    [Tooltip("" +
        "SpawnPositions are where you want dialogue to come out from. " +
        "If you want dialogue to follow a moving object, make sure this is a child of that object."
        + " Example: This would be a child of the Player if you want the dialogue bubble to follow the player."
        )]
    [SerializeField] Transform[] spawnPosition;
    [Tooltip("(Not implemented, leave unchecked) This is if you want the player to interact with an object to trigger dialogue.")]
    [SerializeField] bool[] interactDialogue;
    [Tooltip("Check if you want dialogue to disappear after a set time.")]
    [SerializeField] bool[] timedDialogue;
    [Tooltip("If you checked the above, set the amount of time (seconds) until dialogue disappears.")]
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
        dialogueObject[dialogueNumber].transform.SetParent(spawnPosition[dialogueNumber]);
        dialogueObject[dialogueNumber].SetActive(true);
    }
}
