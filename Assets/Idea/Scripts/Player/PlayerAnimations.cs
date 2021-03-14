using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]

public class PlayerAnimations : MonoBehaviour {
    private Rigidbody2D rigidbodyComponent;
    [SerializeField] private Animator animator;

    void Awake() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    void Update() {
        UpdateAnimator();
    }

    void UpdateAnimator() {
        Vector2 velocity = rigidbodyComponent.velocity;

        bool isMovingHorizontally = Mathf.Abs(velocity.x) > 0.1f;
        bool isFalling = velocity.y < -0.1f;
        bool isJumping = velocity.y > 0.1f;

        animator.SetBool("isMovingHorizontally", isMovingHorizontally);
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("isJumping", isJumping);
    }
}
