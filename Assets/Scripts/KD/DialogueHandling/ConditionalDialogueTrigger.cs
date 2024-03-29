using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionalDialogueTrigger : MonoBehaviour
{
    [SerializeField] public DialogueTrigger[] dialogueTriggers;
    public int useIndex { get; private set; }
    /// <summary>
    /// Checks conditions defined in extended class
    /// Should call SetUseIndex, which determines which DialogueTrigger is used
    /// </summary>
    public abstract void CheckCondition();
    public void SetUseIndex(int i) { useIndex = i; }
    public void ActivateDialogueTrigger()
    {
        for(int i=0; i<dialogueTriggers.Length; i++)
        {
            if(i == useIndex) 
            {
                dialogueTriggers[i].conversation.enabled = true;
                dialogueTriggers[i].gameObject.SetActive(true); 
            }
            else 
            { dialogueTriggers[i].gameObject.SetActive(false); }
        }
    }
    public void DeactivateDialogueTriggers()
    {
        for (int i = 0; i < dialogueTriggers.Length; i++)
        {
            dialogueTriggers[i].conversation.enabled = false;
           dialogueTriggers[i].gameObject.SetActive(false);
        }
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) { return; }
        CheckCondition();
        ActivateDialogueTrigger();
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }
        DeactivateDialogueTriggers();
    }

}
