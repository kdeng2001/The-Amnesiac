using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place in any object that will trigger dialogue
/// </summary>
public class DialogueTrigger : MonoBehaviour
{

    [SerializeField] public bool collisionTrigger;
    PlayerActionManager playerActionManager;
    PlayerManager playerManager;
    int triggerNumber;

    private void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        playerActionManager = GameObject.Find("Player").GetComponent<PlayerActionManager>();
    }

    public void StartDialogue(int dialogueNumber)
    {
        LevelEventsManager.Instance.TriggerDialogue(dialogueNumber);
    }
    
    public virtual void SetTriggerNumber(int number) { triggerNumber = number; }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collisionTrigger) { StartDialogue(triggerNumber); }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player") && playerActionManager.interactValue) 
    //    {
    //        StartDialogue(triggerNumber);   
    //    }
    //}

}
