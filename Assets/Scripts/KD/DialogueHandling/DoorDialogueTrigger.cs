using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDialogueTrigger : ConditionalDialogueTrigger
{
    bool condition;
    public override void CheckCondition()
    {
        condition = MemoryManager.Instance.FoundAllMemoryShards();
        if (condition) { SetUseIndex(0); }
        else { SetUseIndex(1); }
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(useIndex == 0)
        {
            if(dialogueTriggers[0].conversation.InEnd()) { gameObject.SetActive(false); }
        }
    }

}
