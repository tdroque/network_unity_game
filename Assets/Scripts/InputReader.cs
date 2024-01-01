using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking.PlayerConnection;
using static Controls;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]

public class InputReader : ScriptableObject, IPlayerActions
{
    private Controls controls;
    public event Action<bool> primaryFireEvent;
    public event Action<Vector2> moveEvent;
    public Vector2 aimPostion;

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);
        }
   
        controls.Enable();     
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        moveEvent?.Invoke(movement);
    }

    public void OnPrimaryfire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            primaryFireEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            primaryFireEvent?.Invoke(false);
        }
        
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        aimPostion = context.ReadValue<Vector2>();
    }
}
