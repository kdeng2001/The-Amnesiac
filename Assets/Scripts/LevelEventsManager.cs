using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelEventsManager : MonoBehaviour
{
    public static LevelEventsManager Instance;
    public int level { get; private set; }
    public bool canGlide = false;
    public bool finishedBirdDialogue = false;
    PlayerManager playerManager;

    private void Awake()
    {
        if (LevelEventsManager.Instance != null) { Destroy(gameObject); return; }
        else { Instance = this; }
        level = SceneManager.GetActiveScene().buildIndex;
        if (GameObject.Find("Player").TryGetComponent(out PlayerManager pm) )
        {
            playerManager = pm;
            playerManager.enabled = true;
            if(level != 2 || finishedBirdDialogue) { playerManager.canGlide = true; }
        }

    }

    private void OnEnable()
    {
        onPauseActivity += playerManager.PausePlayerActivity;
        onUnPauseActivity += playerManager.UnPausePlayerActivity;
 
    }

    private void OnDisable()
    {
        onPauseActivity -= playerManager.PausePlayerActivity;
        onUnPauseActivity -= playerManager.UnPausePlayerActivity;
 
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

    public event Action onPauseActivity;
    public void PauseActivity()
    {
        if(onPauseActivity != null) { onPauseActivity(); }
    }

    public event Action onUnPauseActivity;
    public void UnPauseActivity()
    {
        if (onUnPauseActivity != null) { onUnPauseActivity(); }
    }



    /// <summary>
    /// Finish tutorial level bird dialogue
    /// </summary>
    public event Action onFinishBirdDialogue;
    public void FinishBirdDialogue()
    {
        if (onFinishBirdDialogue != null) 
        { 
            onFinishBirdDialogue(); 
            playerManager.canGlide = true; 
        }
    }
}
