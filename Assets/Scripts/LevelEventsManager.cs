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
        if(onMemoryShardFound != null) { onMemoryShardFound(); }
    }

    public event Action<Conversation> onTriggerDialogue;
    public void TriggerDialogue(Conversation conversation)
    {
        if(onTriggerDialogue != null) { onTriggerDialogue(conversation); }
    }

    public event Action onInteract;
    public void Interact()
    {
        if(onInteract != null) { onInteract(); }
    }
}
