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
    private void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            playerManager.playerInteract.SetInDialogueTrigger(true);
            playerManager.playerInteract.SetDialogueTrigger(this);
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
            playerManager.playerInteract.SetInDialogueTrigger(false);
            playerManager.playerInteract.SetDialogueTrigger(null);
        }
            
    }

}
