using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerActionManager playerActionManager;
    void Start()
    {
        playerActionManager = GetComponent<PlayerActionManager>();
    }

    public void Jump(float jumpHeight)
    {
        if(playerActionManager.jumpValue)
        {
            Debug.Log("Jump");
        }
        
    }

    bool IsGrounded()
    {
        return true;
    }
}
