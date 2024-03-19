using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public virtual void DisableTrigger()
    {
        if(TryGetComponent(out BoxCollider2D collider))
        {
            collider.enabled = false;
            //this.enabled = false;
        }
            

    }
}
