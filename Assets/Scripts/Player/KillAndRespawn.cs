using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAndRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            KillCharacter();
        }
    }

    public void KillCharacter()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;

        transform.position = respawnPoint.position + 7 * Vector3.up;

        Invoke("EnableCharacter", 1f);
    }

    private void EnableCharacter()
    {
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<Collider2D>().enabled = true;
        LevelEventsManager.resetLevel?.Invoke();
    }
}

