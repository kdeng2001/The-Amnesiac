using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place in any dialogue object container
/// Every Dialogue should start as a child of a Conversation GameObject
/// Every Dialogue should have a speaker and speakPosition (mouth)
/// </summary>
public class Dialogue : MonoBehaviour
{
    [Tooltip("The GameObject that will say the dialogue")]
    [SerializeField] GameObject speaker;
    [Tooltip("The Transform that dialogue will come from (mouth?)")]
    [SerializeField] Transform speakPosition;
    [SerializeField] public float minAliveTime = 0;
    Transform parentOG;

    private void Awake()
    {
        gameObject.SetActive(false);
        
    }

    public void StartDialogue()
    {
        parentOG = transform.parent;
        transform.SetParent(speakPosition);
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
    }
    public void EndDialogue()
    {
        transform.SetParent(ConversationManager.Instance.transform);
        transform.parent = parentOG;
        Debug.Log("End dialogue: " + gameObject.name);
        gameObject.SetActive(false);
    }
}
