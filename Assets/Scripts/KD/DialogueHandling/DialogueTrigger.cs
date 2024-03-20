using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place in any object that will trigger dialogue
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    PlayerManager playerManager;
    [Tooltip("Determines if dialogue starts from a collision or player interaction.")]
    [SerializeField] public bool collisionTrigger;
    [Tooltip("The conversation that starts when triggered.")]
    [SerializeField] public Conversation conversation;
    [SerializeField] public bool outline;
    [SerializeField] float outlineThickness = 0.2f;
    private void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(TryGetComponent(out NPC npc)) { npc.enabled = true; }
        if (collision.CompareTag("Player"))
        {
            playerManager.playerInteract.SetInDialogueTrigger(true);
            playerManager.playerInteract.SetDialogueTrigger(this);
            if (outline) { GiveOutlineToNPC(); }
            if(collisionTrigger) 
            {
 
                conversation.StartConversation();
                // freezes player until dialogue is finished
                playerManager.playerInteract.SetInConversation(true);
                playerManager.playerRB.velocity = Physics2D.gravity;
            }
        }
            
      
    }
    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            if (outline) { RemoveOutlineFromNPC(); }
            playerManager.playerInteract.SetInDialogueTrigger(false);
            if(TryGetComponent(out Memory memory)) { return; }
            if (gameObject.name == "DoorTrigger" || gameObject.name =="DoorTrigger (1)") { return; }
            if (gameObject.name == "NPCBird") { return; }
            playerManager.playerInteract.SetDialogueTrigger(null);
        }
    }

    private void GiveOutlineToNPC()
    {
        gameObject.GetComponentInChildren<Renderer>()
            .sharedMaterial.SetFloat("_OutlineThickness", outlineThickness);
    }

    private void RemoveOutlineFromNPC()
    {
        Debug.Log(gameObject.GetComponentInChildren<Renderer>());
        gameObject.GetComponentInChildren<Renderer>()
        .sharedMaterial.SetFloat("_OutlineThickness", 0);
    }
}
