using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCallbackSystem
{
    #region Component Configs
    
    private InputSetting _inputSetting;

    public event Action<Vector2> OnMove;
    public event Action OnJump;
    public event Action OnSprint;
    public event Action OnChangeColor;

    #endregion
    

    public void Init()
    {
        _inputSetting = new InputSetting();
        EnableInput();
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        OnMove?.Invoke(direction);
    }
    private void Stop(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        OnMove?.Invoke(direction);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        OnSprint?.Invoke();
    }

    private void ChangeColor(InputAction.CallbackContext context)
    {
        OnChangeColor?.Invoke();
    }

    private void EnableInput()
    {
        _inputSetting.Player.Move.performed += Move;
        _inputSetting.Player.Move.canceled += Stop;
        _inputSetting.Player.Jump.performed += Jump;
        _inputSetting.Player.Sprint.performed += Sprint;
        _inputSetting.Player.Sprint.canceled += Sprint;
        _inputSetting.Player.Interact.performed += ChangeColor;
        _inputSetting.Enable();
    }

    private void DisableInput()
    {
        _inputSetting.Player.Move.performed -= Move;
        _inputSetting.Player.Move.canceled -= Stop;
        _inputSetting.Player.Jump.performed -= Jump;
        _inputSetting.Player.Sprint.performed -= Sprint;
        _inputSetting.Player.Sprint.canceled -= Sprint;
        _inputSetting.Player.Interact.performed -= ChangeColor;
        _inputSetting.Disable();
    }
}
