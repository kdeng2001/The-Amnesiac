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

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartDialogue()
    {
        transform.SetParent(speakPosition);
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
    }
    public void EndDialogue()
    {
        transform.SetParent(DialogueManager.Instance.transform);
        gameObject.SetActive(false);
    }
}
