using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float _moveSpeed;

    private bool _isWalking;

    void Update() {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W)) {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += _moveSpeed * Time.deltaTime * moveDir;

        _isWalking = moveDir != Vector3.zero;

        float moveSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, moveSpeed * Time.deltaTime);
    }

    public bool IsWalking() {
        return _isWalking;
    }
}
