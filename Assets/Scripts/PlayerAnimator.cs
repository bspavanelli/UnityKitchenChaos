using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "isWalking";

    [SerializeField] private Player player;
    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        if (player.IsWalking()) {
            _animator.SetBool(IS_WALKING, true);   
        } else {
            _animator.SetBool(IS_WALKING, false);
        }
    }
}
