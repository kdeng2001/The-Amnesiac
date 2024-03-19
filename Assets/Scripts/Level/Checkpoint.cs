using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject player;
    private KillAndRespawn killAndRespawn;
    [SerializeField] public SpriteRenderer flag;
    [SerializeField] public Color color;
    public bool canRespawnHere = false;
    public int checkpointNumber;
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    void Start()
    {
        player = GameObject.Find("Player");
        killAndRespawn = player.GetComponent<KillAndRespawn>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            flag.material.color = Color.red;
            canRespawnHere = true;
            killAndRespawn.respawnPoint = transform;
        }
    }
}
