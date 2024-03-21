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
        if(collision.CompareTag("Player"))
        {
            if(outline) { GiveOutlineToNPC(); }
            conversation.enabled = true;
        }
      
    }
    private void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player"))
        {
            if(outline) { RemoveOutlineFromNPC(); }
            conversation.enabled = false;
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
