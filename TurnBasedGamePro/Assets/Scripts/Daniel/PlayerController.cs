using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float snapDistance = 0.6f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Input")]
    [SerializeField] private InputActionReference inputActions;

    private Rigidbody2D rb;
    private Vector2 direction;

    public bool canStickToGround = true;
    private float raycastOffset = 0.05f;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        direction = inputActions.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        HandleMovement();

        if (canStickToGround)
            StickYToGround();
    }

    private void HandleMovement()
    {
        // Move normally (horizontal only)
        Vector2 velocity = rb.linearVelocity;
        velocity.x = direction.x * moveSpeed;
        rb.linearVelocity = velocity;
    }

    private void StickYToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, snapDistance, groundLayer);

        if (hit.collider)
        {
            isGrounded = true;
            rb.gravityScale = 0f;

            // ✅ Only adjust vertical position
            rb.position = new Vector2(rb.position.x, hit.point.y + raycastOffset);

            // Prevent any unwanted downward motion
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }
        else
        {
            isGrounded = false;
            rb.gravityScale = 1f;
        }
    }

    public void OnHit(float disableDuration = 2f)
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(DisableStickTemporarily(disableDuration));
    }

    private IEnumerator DisableStickTemporarily(float duration)
    {
        canStickToGround = false;
        rb.gravityScale = 1f;
        yield return new WaitForSeconds(duration);
        canStickToGround = true;
    }
}
