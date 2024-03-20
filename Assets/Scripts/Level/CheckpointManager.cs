using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    Checkpoint[] checkpoints;
    void Start()
    {
        checkpoints = GetComponentsInChildren<Checkpoint>();   
    }
}
