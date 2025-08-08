using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    Transform playerPos;
    public float movementSpeed;
    public bool isMoving;

    PlayerXP playerXP;
    Animator animator;

    private void Awake()
    {
        rb =  GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform; //turn to "Objects" if multiplayer, get closest player or assign a random player per enemyMask
        playerXP = playerPos.GetComponent<PlayerXP>();
        animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        isMoving = true;
    }

    private void Update()
    {

        if (!playerXP.levelingUp && isMoving)
        {
            FollowPlayer();
        }

    }

    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, movementSpeed * Time.deltaTime);
        transform.up = -(playerPos.position - transform.position);

    }

}


