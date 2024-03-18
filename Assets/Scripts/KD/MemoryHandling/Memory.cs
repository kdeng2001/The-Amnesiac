using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    PlayerInteract playerInteract;
    [SerializeField] public int MemoryNumber;

    private void Start()
    {
        playerInteract = GameObject.Find("Player").GetComponent<PlayerInteract>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MemoryManager.Instance.FoundMemoryShard(MemoryNumber, gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!playerInteract.inConversation) { gameObject.SetActive(false); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
