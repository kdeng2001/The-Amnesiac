using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionManager : MonoBehaviour
{

    public Vector2 moveValue { get; private set; }
    public bool jumpValue { get; private set; }
    public bool glideValue { get; private set; }
    public bool interactValue { get; private set; }
    public bool escapeValue { get; private set; }
    private void OnEnable()
    {
        if(TryGetComponent(out PlayerInput input)) { RegisterActions(input); }
        
    }

    private void OnDisable()
    {
        if (TryGetComponent(out PlayerInput input)) { UnRegisterActions(input); }
    }

    void RegisterActions(PlayerInput input)
    {
        input.actions.FindActionMap("UI").Enable();
        InputAction moveAction = input.actions["Move"];
        if(moveAction != null) 
        { 
            moveAction.performed += context => SetMove(context.ReadValue<Vector2>());
        }
        InputAction jumpAction = input.actions["Jump"];
        if(jumpAction != null)
        {
            jumpAction.performed += context => SetJump(context.ReadValueAsButton());
            jumpAction.canceled += context => OnJumpCancel(context.ReadValueAsButton());
        }
        InputAction glideAction = input.actions["glide"];
        if(glideAction != null)
        {
            glideAction.performed += context => SetGlide(context.ReadValueAsButton());
            glideAction.canceled += context => SetGlide(context.ReadValueAsButton());
        }
        InputAction interactAction = input.actions["interact"];
        if (interactAction != null)
        {
            interactAction.canceled += context => OnInteractStart(context);
        }
        InputAction escapeAction = input.actions["Pausemenu"];
        if (escapeAction != null)
        {
            escapeAction.performed += context => OnEscape(context);
            escapeAction.canceled += context => SetEscape(context.ReadValueAsButton());
        }
    }

    void UnRegisterActions(PlayerInput input) 
    {
        InputAction moveAction = input.actions["Move"];
        if (moveAction != null)
        {
            moveAction.performed -= context => SetMove(context.ReadValue<Vector2>());
        }
        InputAction jumpAction = input.actions["Jump"];
        if (jumpAction != null)
        {
            jumpAction.performed -= context => SetJump(context.ReadValueAsButton());
            jumpAction.canceled -= context => OnJumpCancel(context.ReadValueAsButton());
        }
        InputAction glideAction = input.actions["glide"];
        if (glideAction != null)
        {
            glideAction.performed -= context => SetGlide(context.ReadValueAsButton());
            glideAction.canceled -= context => SetGlide(context.ReadValueAsButton());
        }
        InputAction interactAction = input.actions["interact"];
        if (interactAction != null)
        {
            interactAction.canceled -= context => OnInteractStart(context);
        }
        InputAction escapeAction = input.actions["Pausemenu"];
        if (escapeAction != null)
        {
            escapeAction.performed += context => OnEscape(context);
            escapeAction.canceled += context => SetEscape(context.ReadValueAsButton());
        }
    }

    void SetMove(Vector2 value) { moveValue = value; }
    void SetJump(bool value) { jumpValue = value; }
    void SetGlide(bool value) { glideValue = value; /*Debug.Log("glide value: " + value);*/ }
    void SetInteract(bool value) { interactValue = value; }
    void SetEscape(bool value) { escapeValue = value; }
    void OnJumpCancel(bool value) 
    {
        LevelEventsManager.Instance.JumpCancel();
        jumpValue = value;
    }
    void OnInteractStart(InputAction.CallbackContext context) 
    {
        SetInteract(context.ReadValueAsButton());
        LevelEventsManager.Instance.Interact();
        //Debug.Log("interact");
    }

    void OnEscape(InputAction.CallbackContext context)
    {
        Debug.Log("escape!");
        SetEscape(context.ReadValueAsButton());
        PauseMenu.togglePause();
    }
}

    