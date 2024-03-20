using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    int waypointIndex = 1;
    [SerializeField] Transform[] waypoints;
    [SerializeField] float Speed = 2.0f;
    [SerializeField] float platformWaitTime = 3.0f;

    [SerializeField] GameObject movingPlatform;
    bool isChangingDirection;
    PlayerInteract playerInteract;

    private void Start()
    {
        playerInteract = GameObject.Find("Player").GetComponent<PlayerInteract>();
    }

    private void FixedUpdate()
    {
        if (playerInteract.enabled && playerInteract.inConversation) { return; }
        //Moving to the desired position
        if (waypointIndex < waypoints.Length)
        {
            movingPlatform.transform.position = Vector3.MoveTowards(movingPlatform.transform.position, waypoints[waypointIndex].transform.position, Speed * Time.fixedDeltaTime);
            //Changing the direciton of movement
            if (movingPlatform.transform.position == waypoints[waypointIndex].transform.position)
            {
                isChangingDirection = !isChangingDirection;
                StartCoroutine(wait());
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(platformWaitTime);
        if (!isChangingDirection)
        {
            waypointIndex = 1;
        }
        else
        {
            waypointIndex = 0;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter: " + other.tag);
        other.transform.SetParent(movingPlatform.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.transform.SetParent(null);
    }
}