using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place in any object that will trigger dialogue 
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    /// <summary>
    /// maybe will have other ways to trigger dialogue?
    /// </summary>
    [SerializeField] bool collisionTrigger;
    int triggerNumber;

    public void StartDialogue(int dialogueNumber)
    {
        LevelEventsManager.Instance.TriggerDialogue(dialogueNumber);
    }
    
    public void SetTriggerNumber(int number) { triggerNumber = number; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collisionTrigger) { StartDialogue(triggerNumber); }
    }

}
