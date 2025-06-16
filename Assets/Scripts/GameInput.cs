using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions _playerInput;

    private void Awake() {
        _playerInput = new PlayerInputActions();
        _playerInput.Player.Enable();
    }
    public Vector2 getMovementVectorNormalized() {
        Vector2 inputVector = _playerInput.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
