using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance;
    private void Awake()
    {
        if (PlatformManager.Instance != null) { Destroy(gameObject); return; }
        else { Instance = this; }

        //LevelEventsManager.resetLevel += FallingPlatform.Reset;
    }
}
