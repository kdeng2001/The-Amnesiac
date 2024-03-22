using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    SpriteRenderer sprite;
    [SerializeField] public int MemoryNumber;

    private void Awake() { sprite = GetComponent<SpriteRenderer>(); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MemoryManager.Instance.FoundMemoryShard(MemoryNumber, gameObject);
            sprite.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
