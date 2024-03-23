using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //[SerializeField] bool interactable = true;
    //[SerializeField] bool repeatsDialogue = true;
    //[SerializeField] bool despawnsAfterInteract = false;
    [SerializeField] public float speed = 15f;
    [SerializeField] public Animator animator;
    [SerializeField] public SpriteRenderer sprite;

    public virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    bool finishedDialogue = false;
    public virtual void Move(Transform target) 
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
    }
}
