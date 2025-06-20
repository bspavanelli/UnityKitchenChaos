using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private PlayerInputActions _playerInput;

    private void Awake() {
        _playerInput = new PlayerInputActions();
        _playerInput.Player.Enable();

        _playerInput.Player.Interact.performed += Interact_performed;
        _playerInput.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        // Dispara o evento apenas se ele não for null
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 getMovementVectorNormalized() {
        Vector2 inputVector = _playerInput.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
