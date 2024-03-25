using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    SpriteRenderer sprite;
    [SerializeField] public int MemoryNumber;
    [SerializeField] public bool isBonus = false;
    [SerializeField] public AudioSource audioClip;
    private void Awake() { sprite = GetComponent<SpriteRenderer>(); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!isBonus) { MemoryManager.Instance.FoundMemoryShard(MemoryNumber, gameObject); }
            sprite.enabled = false;
            audioClip.Play();
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
