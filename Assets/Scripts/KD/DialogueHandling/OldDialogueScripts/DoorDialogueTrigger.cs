using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDialogueTrigger : DialogueTrigger
{
    //[Tooltip("Option 1: The number associating with the dialogue used when the player found all memory shards.")]
    //[System.NonSerialized] public int triggerNum1;
    //[Tooltip("Option 2: The number associating with the dialogue used when the player has not found all memory shards.")]
    //[System.NonSerialized] public int triggerNum2;

    //public void SetTriggerNumber(int num1, int num2)
    //{
    //    triggerNum1 = num1;
    //    triggerNum2 = num2;
    //}
    //public override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collisionTrigger)
    //    {
    //        if(MemoryManager.Instance.FoundAllMemoryShards())
    //        {
    //            Debug.Log("door trigger: " + triggerNum1);
    //            StartDialogue(triggerNum1);
    //            StartCoroutine(DelayDestroy());
    //        }
    //        else
    //        {
    //            Debug.Log("door trigger: " + triggerNum2);
    //            StartDialogue(triggerNum2);
                
    //        }
    //    }
    //}

    //IEnumerator DelayDestroy()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    Destroy(gameObject);
    //}
}
