using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place in any object that will trigger dialogue
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    PlayerManager playerManager;
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
        if(!enabled) { return; }
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Conversation trigger enter: " + conversation.name);
            if(outline) { GiveOutlineToNPC(); }
            conversation.enabled = true;
        } 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!enabled) { return; }
        if (conversation.oneTimeConversation) { return; }
        if(conversation.collisionTrigger) { return; }
        if(!conversation.enabled) { conversation.enabled = true; }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (!enabled) { return; }
        if (collision.CompareTag("Player"))
        {
            if(outline) { RemoveOutlineFromNPC(); }
            Debug.Log("Conversation trigger exit: " + (conversation==null));
            conversation.enabled = false;
        }
    }

    private void GiveOutlineToNPC()
    {
        transform.parent.gameObject.GetComponentInChildren<Renderer>()
            .sharedMaterial.SetFloat("_OutlineThickness", outlineThickness);
    }

    private void RemoveOutlineFromNPC()
    {
        Debug.Log(gameObject.GetComponentInChildren<Renderer>());
        transform.parent.gameObject.GetComponentInChildren<Renderer>()
        .sharedMaterial.SetFloat("_OutlineThickness", 0);
    }
}
