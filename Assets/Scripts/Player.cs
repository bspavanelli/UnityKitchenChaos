using System;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private Vector3 _lastInteractDirection;
    private bool _isWalking;
    private ClearCounter _selectedCounter;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player Instance");
        }
        Instance = this;
    }
    private void Start() {
        _gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (_selectedCounter != null) {
            _selectedCounter.Interact();
        }
    }

    void Update() {
        HandleMovement();
        HandleInteractions();
    }
    public bool IsWalking() {
        return _isWalking;
    }

    private void HandleInteractions() {
        Vector3 moveDirection = GetMoveDirection();

        if (moveDirection != Vector3.zero) {
            _lastInteractDirection = moveDirection;
        }

        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                // Has ClearCounter
                if (clearCounter != _selectedCounter) {
                    SetSelectedCounter(clearCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement() {
        Vector3 moveDirection = GetMoveDirection();

        float moveDistance = _moveSpeed * Time.deltaTime;
        float playerRadius = .68f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove) {
            // Cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove) {
                // Can move only on the X
                moveDirection = moveDirectionX;
            } else {
                // Cannot move only on the X

                // Attempt only Z movement
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if (canMove) {
                    // Can move only on the Z
                    moveDirection = moveDirectionZ;
                } else {
                    // Cannot move in any direction
                }
            }
        }

        if (canMove) {
            transform.position += moveDirection * moveDistance;
        }

        _isWalking = moveDirection != Vector3.zero;

        float moveSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, moveSpeed * Time.deltaTime);
    }
    private Vector3 GetMoveDirection() {
        Vector2 inputVector = _gameInput.getMovementVectorNormalized();

        return new Vector3(inputVector.x, 0, inputVector.y);
    }

    private void SetSelectedCounter(ClearCounter selectedCounter) {
        this._selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = _selectedCounter });
    }
}
