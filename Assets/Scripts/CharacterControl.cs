using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField]
    private float speed = 500.0f;

    [SerializeField]
    private float jumpForce = 6.0f;

    private Rigidbody2D rigidBody;
    private BoxCollider2D playerCollider;
    private Animator animator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody == null)
        {
            Debug.LogError("Failed to start. Rigid body component not found!");
        }

        playerCollider = GetComponent<BoxCollider2D>();
        if (playerCollider == null)
        {
            Debug.LogError("Failed to start. Collider component not found!");
        }


        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Failed to start. Animator component not found!");
        }

    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, rigidBody.velocity.y);
        rigidBody.velocity = movement;

        Vector2 colliderCorner1 = new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y - 0.1f);
        Vector2 colliderCorner2 = new Vector2(playerCollider.bounds.min.x, playerCollider.bounds.min.y - 0.2f);
        Collider2D hit = Physics2D.OverlapArea(colliderCorner1, colliderCorner2);

        bool isGrounded = (hit != null);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isJumping", !isGrounded);
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Make animation react to movement
        animator.SetFloat("horizontalSpeed", Mathf.Abs(deltaX));
        if (!Mathf.Approximately(deltaX, 0.0f))
        {
            // Scale x to either positive or negative 1 to 'turn' the character
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1.0f, 1.0f);
        }

        //animator.SetBool("isJumping", !isGrounded);
        animator.SetFloat("verticalSpeed", rigidBody.velocity.y);
    }
}
