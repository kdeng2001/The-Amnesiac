using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Tooltip("Time after player touches platform before it starts falling")]
    [SerializeField] float timeTilFall = 0.5f;
    [Tooltip("Time after player touches platform before it despawns")]
    [SerializeField] float timeTilDespawn = 0.5f;
    Rigidbody2D rb;

    Vector2 defaultPosition;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultPosition = transform.position;
        LevelEventsManager.resetLevel += Reset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(DelayFall());
    }

    IEnumerator DelayFall()
    {
        yield return new WaitForSeconds(timeTilFall);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.freezeRotation = true;
        Despawn();
    }

    private void Despawn()
    {
        StartCoroutine(DelayDespawn());
    }

    IEnumerator DelayDespawn()
    {
        yield return new WaitForSeconds(timeTilDespawn);
        enabled = false;
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        enabled = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = defaultPosition;
    }
}
