using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]

public class PlayerMovement : MonoBehaviour {
    private bool shouldJump;
    private int jumpCount;
    private float horizontalInput;
    private bool isGoingRight = true;
    private Rigidbody2D rigidbodyComponent;
    private BoxCollider2D colliderComponent;

    [SerializeField] private float jumpPower = 7;
    [SerializeField] private int maxJumpCount = 2;
    [SerializeField] private float speed = 5;
    [SerializeField] private float speedSmoothing = 0.05f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Awake() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        colliderComponent = GetComponent<BoxCollider2D>();
    }

    void Update() {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump")) shouldJump = true;
    }

    private void FixedUpdate() {
        bool canJump = jumpCount < maxJumpCount;
        bool jumpInThisUpdate = shouldJump && canJump;

        if (jumpInThisUpdate) Jump();
        if (!jumpInThisUpdate && IsGrounded()) jumpCount = 0;

        float speedInput = horizontalInput * speed;
        Vector2 currentVelocity = rigidbodyComponent.velocity;
        Vector2 targetVelocity = new Vector2(speedInput, currentVelocity.y);
        Vector2 velocity = Vector2.zero;

        rigidbodyComponent.velocity = Vector2.SmoothDamp(currentVelocity, targetVelocity, ref velocity, speedSmoothing);
        shouldJump = false;

        if (isGoingRight && speedInput < 0) Flip();
        else if (!isGoingRight && speedInput > 0) Flip();
    }

    private bool IsGrounded() {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        return isGrounded;
    }
    private void Jump() {
        rigidbodyComponent.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        jumpCount += 1;
    }

    private void Flip() {
        isGoingRight = !isGoingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawSphere(groundCheck.position, 0.1f);
    }

}
