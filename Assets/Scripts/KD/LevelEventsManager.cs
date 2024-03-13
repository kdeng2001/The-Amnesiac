using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEventsManager : MonoBehaviour
{
    public static LevelEventsManager Instance;
    

    private void Awake()
    {
        if (LevelEventsManager.Instance != null) { Destroy(gameObject); return; }
        else { Instance = this; }
    }

    public event Action onMemoryShardFound;
    public void MemoryShardFound()
    {
        if(onMemoryShardFound != null)
        {
            onMemoryShardFound();
        }
    }

    public event Action<int> onTriggerDialogue;
    public void TriggerDialogue(int dialogueNumber)
    {
        if(onTriggerDialogue != null)
        {
            onTriggerDialogue(dialogueNumber);
        }
    }
}
