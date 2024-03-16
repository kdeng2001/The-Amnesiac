using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChain : Dialogue
{
    [SerializeField]Dialogue[] dialogues;
    int currentIndex = 0;
    int maxIndex = 0;
    Dialogue currentDialogue;

    bool startInteract = true;

    public override void OnEnable()
    {
        base.OnEnable();
        
        currentDialogue = dialogues[0];
        currentDialogue.gameObject.SetActive(true);
        Debug.Log("enable chain");
    }

    public override void HandleInteractDialogue()
    {
        Debug.Log("handle interact dialogue chain: " + currentIndex);
        if(startInteract) 
        {
            Debug.Log("StartInteract");
            startInteract = false;
            currentDialogue = dialogues[0];
            currentDialogue.gameObject.SetActive(true);
            currentDialogue.transform.localPosition = Vector3.zero;
        }
        if(currentIndex > maxIndex) { FinishDialogue(); }
        else if(playerActionManager.interactValue)
        {
            Debug.Log("interact with chain");        
            
            playerActionManager.SetInteract(false);
            currentIndex++;
            if(currentIndex > maxIndex) { return; }
            dialogues[currentIndex - 1].FinishDialogue();
            currentDialogue = dialogues[currentIndex];
            currentDialogue.gameObject.SetActive(true);
            currentDialogue.transform.localPosition = Vector3.zero;
        }
    }

    public override void HandleTimedDialogue()
    {
        if (currentIndex > maxIndex) { FinishDialogue(); }
        else
        {
            if(!currentDialogue.gameObject.activeSelf) 
            { 
                currentDialogue.gameObject.SetActive(true);
                currentDialogue.transform.localPosition = Vector3.zero;
            }
            if(currentDialogue.elapsedTime > currentDialogue.durationOfDialogue)
            {
                currentDialogue.FinishDialogue();
                Debug.Log(currentIndex);
                if(++currentIndex > maxIndex) { return; }
                currentDialogue = dialogues[currentIndex];
            }
            else
            {
                currentDialogue.Timer();
            }
        }
    }

    public override void FinishDialogue()
    {
        gameObject.SetActive(false);
    }


    public void SetUpChainVariables()
    {
        maxIndex = dialogues.Length - 1;
        Debug.Log("maxIndex: " + maxIndex);
        for(int i=0; i<=maxIndex; i++)
        {
            dialogues[i].SetVariables(spawnPosition, interactDialogue, timedDialogue, durationOfDialogue);
        }

    }
}
