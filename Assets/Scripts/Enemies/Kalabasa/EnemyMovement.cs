using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    Transform playerPos;
    public float movementSpeed;
    float xVelocity, yVelocity;
    public bool isFacingLeft;

    EnemyAttack enemyAttack;

    PlayerXP playerXP;
    Animator animator;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform; //turn to "Objects" if multiplayer, get closest player or assign a random player per enemyMask
        playerXP = playerPos.GetComponent<PlayerXP>();
        enemyAttack = this.gameObject.GetComponentInChildren<EnemyAttack>();
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!playerXP.levelingUp)
        {
            FlipSprite();
            if (!enemyAttack.readyAttack)
            {
                FollowPlayer();
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, 0f);
        }

    }

    private void FixedUpdate()
    {
        animator.SetFloat("xVelocityKal", Mathf.Abs(rb.linearVelocityX));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Player inside Enemy");
            enemyAttack.readyAttack = true;
        }
    }


    private void FollowPlayer()
    {
        xVelocity = new Vector2(transform.position.x - playerPos.position.x, yVelocity).normalized.x;
        rb.linearVelocity = new Vector2 (-xVelocity * movementSpeed, rb.linearVelocityY);

    }
    void FlipSprite()
    {
        if (-xVelocity < 0 && isFacingLeft || -xVelocity > 0 && !isFacingLeft)
        {

            isFacingLeft = !isFacingLeft;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            this.transform.localScale = ls;
        }
    }

}


