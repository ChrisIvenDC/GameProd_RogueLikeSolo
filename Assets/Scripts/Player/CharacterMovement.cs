using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    float horizontalInput;
    public float moveSpeed;
    public float jumpHeight;
    public Vector2 groundBoxSize;
    public float castDistance;
    public LayerMask groundLayer;
    private bool isFacingLeft = false;
    private bool isJumping;
    bool grounded;

    PlayerXP playerXP;
    [SerializeField]Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerXP = GetComponent<PlayerXP>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!playerXP.levelingUp)
        {
            horizontalInput = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump") && isGrounded())
            {
                animator.SetBool("isGrounded", false);
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpHeight);
            }

            if (rb.linearVelocityY < 0 && isGrounded())
            {
                animator.SetBool("isGrounded", true);
            }
            FlipSprite();
        }
        else
        {
            rb.linearVelocity = new Vector2(0f,0f);
        }

    }

    private void FixedUpdate()
    {
        if (!playerXP.levelingUp)
        {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocityY);
        }
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocityX));
        animator.SetFloat("yVelocity", rb.linearVelocityY);
    }

    void FlipSprite()
    {
        if (horizontalInput > 0 && isFacingLeft || horizontalInput < 0 && !isFacingLeft) 
        { 
            isFacingLeft = !isFacingLeft;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            this.transform.localScale = ls;
        }
    }

    public bool isGrounded()
    {
        if(Physics2D.BoxCast(transform.position, groundBoxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, groundBoxSize);
    }

    /*    private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                grounded = true;
                animator.SetBool("isGrounded", grounded);
            }

        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                grounded = false;
                animator.SetBool("isGrounded", grounded);
            }

        }*/
}
