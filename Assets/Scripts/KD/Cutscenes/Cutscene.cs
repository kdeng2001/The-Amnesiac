using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public PlayerManager player;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
    }
}
