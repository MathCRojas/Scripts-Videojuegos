using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, PlayerControls.IPlayerActions
{

    public Vector2 movementValue { get; private set; }
    public event Action JumpEvent;
    public event Action DashEvent;
    private PlayerControls controls;


    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }
    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }
        DashEvent?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        JumpEvent?.Invoke();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<Vector2>();
    }

}
