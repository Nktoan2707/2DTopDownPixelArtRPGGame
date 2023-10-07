using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool _isMoving = false;

    private bool IsMoving
    {
        set
        {
            _isMoving = value;
            animator.SetBool("isMoving", _isMoving);
        }
        get { return _isMoving; }
    }

    private bool canMove = true;

     float moveSpeed = 1400f;

    public float collisionOffset = 0.01f;
    public ContactFilter2D movementFilter;

    private Vector2 moveInput = Vector2.zero;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject swordHitbox;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        // If movement input is not 0, try to move
        if (canMove && moveInput != Vector2.zero)
        {
            rb.AddForce(moveInput * moveSpeed * Time.deltaTime);

            if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false;
                gameObject.BroadcastMessage("IsFacingRight", true);
            }
            else if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
            }

            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
    }

    private void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    {
        LockMovement();
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}