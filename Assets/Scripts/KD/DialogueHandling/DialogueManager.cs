using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    static public DialogueManager Instance;

    private void Awake()
    {
        if(DialogueManager.Instance != null) { Destroy(gameObject); }
        else { DialogueManager.Instance = this; }
    }
}
