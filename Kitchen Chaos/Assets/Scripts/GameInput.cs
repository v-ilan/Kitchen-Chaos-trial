using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnIneractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingRebind;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        Alternative_Interact,
        Toggle_Pause,
        Gamepad_Interact,
        Gamepad_Alternative_Interact,
        Gamepad_Toggle_Pause,
    }

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        if(PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += InteractPerformed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternatePerformed;
        playerInputActions.Player.Pause.performed += PausePerformed;

    }

    private void OnDestroy()
    {
        if(playerInputActions != null)
        {
            playerInputActions.Player.Interact.performed -= InteractPerformed;
            playerInputActions.Player.InteractAlternate.performed -= InteractAlternatePerformed;
            playerInputActions.Player.Pause.performed -= PausePerformed;

            playerInputActions.Dispose();
        }
        
    }

    private void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    private void InteractAlternatePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnIneractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
    private void PausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        string binding_str = "";
        switch(binding)
        {
            case Binding.Move_Up:
                binding_str = playerInputActions.Player.Move.bindings[1].ToDisplayString();
                break;
            case Binding.Move_Down:
                binding_str = playerInputActions.Player.Move.bindings[2].ToDisplayString();
                break;
            case Binding.Move_Left:
                binding_str = playerInputActions.Player.Move.bindings[3].ToDisplayString();
                break;
            case Binding.Move_Right:
                binding_str = playerInputActions.Player.Move.bindings[4].ToDisplayString();
                break;
            case Binding.Interact:
                binding_str = playerInputActions.Player.Interact.bindings[0].ToDisplayString();
                break;
            case Binding.Alternative_Interact:
                binding_str = playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
                break;
            case Binding.Toggle_Pause:
                binding_str = playerInputActions.Player.Pause.bindings[0].ToDisplayString();
                break;
            case Binding.Gamepad_Interact:
                binding_str = playerInputActions.Player.Interact.bindings[1].ToDisplayString();
                break;
            case Binding.Gamepad_Alternative_Interact:
                binding_str = playerInputActions.Player.InteractAlternate.bindings[1].ToDisplayString();
                break;
            case Binding.Gamepad_Toggle_Pause:
                binding_str = playerInputActions.Player.Pause.bindings[1].ToDisplayString();
                break;
            default:
                Debug.LogError("Can not get Binding");
                break;
        }
        return binding_str;
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        InputAction inputAction = null;
        int bindingIndex = 0;

        switch (binding)
        {
            case Binding.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.Alternative_Interact:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Toggle_Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
            case Binding.Gamepad_Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Alternative_Interact:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Toggle_Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 1;
                break;
            default:
                Debug.LogError("Can not get Binding"); 
                break;
        }

        playerInputActions.Player.Disable();
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback => {
            callback.Dispose();
            playerInputActions.Player.Enable();
            onActionRebound();
            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            OnBindingRebind?.Invoke(this, EventArgs.Empty);
        }).Start();
    }
}
