using UnityEngine;
using System.Collections;
using Unity.VisualScripting;


public class SibuyasAttack : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] FlyingEnemyMovement enemyMovement;
    [SerializeField] Animator animator;
    [SerializeField] PlayerXP playerXP;
    [SerializeField] float attackDamage;
    [SerializeField] float holdTime;
    [SerializeField] float maxHoldTime;

    [SerializeField] bool isHolding;
    [SerializeField] bool isAttacking;
    [SerializeField] bool isInsideAttack;
/*    [SerializeField] float attackSize;
    [SerializeField] LayerMask mobAttackLayer;

*/
    private void Start()
    {
        holdTime = 0f;
        isAttacking = false;
    }
    private void Awake()
    {
        animator = transform.parent.GetComponent<Animator>();
        enemyMovement = transform.parent.GetComponent<FlyingEnemyMovement>();
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerXP>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        if (isInsideAttack && !playerXP.levelingUp)
        {
            Debug.Log("player inside sibuyas. should hold");

            isHolding = true;

        }
        else 
        {
            animator.SetBool("isHolding", false);
            animator.SetBool("isAttacking", false);
            isHolding = false;
        }

        if (isHolding)
        {
            animator.SetBool("isHolding", true);
            enemyMovement.isMoving = false;
            holdTime += Time.deltaTime;

            if (holdTime >= maxHoldTime)
            {
                animator.SetBool("isAttacking", true);
                isAttacking = true;
                holdTime = 0;
            }
        }
        else 
        {
            enemyMovement.isMoving = true;
            holdTime = 0;
        }


        if (isAttacking)
        {
            if (isInsideAttack && !playerXP.levelingUp)
            {
                Debug.Log("playertook damage from sibuyas");
                playerHealth.PlayerTakeDamage(attackDamage);
            }
            StartCoroutine(WaitforAttackAnim());
            isAttacking = false;
            enemyMovement.isMoving = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player in sibuyas");
            isInsideAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player exit in sibuyas");
            isInsideAttack = false;
        }
    } 

    IEnumerator WaitforAttackAnim()
    {
        yield return new WaitForSeconds(.15f);
        animator.SetBool("isAttacking", false);
    }
}


