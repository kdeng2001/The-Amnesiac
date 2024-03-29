    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryManager : MonoBehaviour
{
    Image[] memoryUI;
    Memory[] memoriesInScene;
    public static MemoryManager Instance;
    int collected = 0;
    public int mainMemoryCount = 0;
    private void Awake()
    {
        if(MemoryManager.Instance != null) { Destroy(gameObject); return; }
        else { Instance = this; }
    }
    void Start()
    {
        memoryUI = GetComponentsInChildren<Image>();
        memoriesInScene = GetMemoriesInScene();

        for(int i=0; i<memoriesInScene.Length; i++) { if (!memoriesInScene[i].isBonus) { mainMemoryCount++; } }

        for(int i=memoryUI.Length - 1; i>=mainMemoryCount; i--)
        {
            //Debug.Log(memoryUI[i]);
            memoryUI[i].gameObject.SetActive(false);
        }
    }


    Memory[] GetMemoriesInScene()
    {
        Memory[] memoriesInScene = FindObjectsByType<Memory>(FindObjectsSortMode.InstanceID);
        return memoriesInScene;
    }

    public void FoundMemoryShard(int shardNumber, GameObject memory)
    {
        //Debug.Log("Player Collects Memory");
        if(memory.TryGetComponent(out Memory mem)) 
        { 
            if(mem != null && mem.isBonus) { }         
            else if(memory.TryGetComponent(out SpriteRenderer sprite))
            {
            memoryUI[shardNumber].color = sprite.color;
            collected++;
            }
        }

        StartCoroutine(DelayDisable(memory));
        //memory.SetActive(false);
    }

    IEnumerator DelayDisable(GameObject memory)
    {
        yield return new WaitForSeconds(.1f);
        memory.GetComponentInChildren<SpriteRenderer>().enabled = false;
        //memory.SetActive(false);
    }

    public bool FoundAllMemoryShards()
    {
        return collected == mainMemoryCount;
    }
}
