using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] bool interactable = true;
    [SerializeField] bool repeatsDialogue = true;
    [SerializeField] bool despawnsAfterInteract = false;

    bool finishedDialogue = false;
    public virtual void Move(Transform target) { }
}
