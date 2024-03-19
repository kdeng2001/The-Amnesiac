using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdConversation : Conversation
{
    SpriteRenderer player;
    //UnityEvent myEvent = new UnityEvent();
    private void Start()
    {
        player = GameObject.Find("Player").GetComponentInChildren<SpriteRenderer>();
    }
    public override void StartSpecialDialogue()
    {        
        Camera.main.transform.localPosition += new Vector3(0, 8, 0);
        player.flipX = true;
        //myEvent.AddListener(HandleSpecial);
        //myEvent.Invoke();

        
    }
    public virtual void HandleSpecialDialogue() { }
    public override void EndSpecialDialogue()
    {
        Camera.main.transform.localPosition -= new Vector3(0, 8, 0);
        player.flipX = false;
        //Camera.main.orthographicSize -= 10;
        //myEvent.RemoveListener(HandleSpecial);
    }

    //void HandleSpecial()
    //{
    //}
}
