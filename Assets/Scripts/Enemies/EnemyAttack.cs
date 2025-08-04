using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float maxHoldTimer;
    public float holdTime = 3f;
    public float attackDamage = 40f;
    public bool readyAttack;
    public bool insideDamageZone;
    public bool isAttacking;
    bool tookDamage;
    Animator animator;

    PlayerXP playerXP;
    PlayerHealth playerHealth;
    private void Awake()
    {
        animator = gameObject.transform.parent.GetComponent<Animator>();
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerXP>();
    }

    private void Update()
    {
        if (animator != null)
        {
            {
                Debug.Log("animator");
            }
        }
        if (readyAttack)
        {

            holdTime += Time.deltaTime;
            isAttacking = false;
            animator.SetBool("isHolding", true);
        }

        if(holdTime > maxHoldTimer) //turn to coroutines
        {
            tookDamage = false;
            isAttacking = true;
            holdTime = 0f;
            readyAttack = false;
            animator.SetBool("isHolding", false);

            //animator
        }

        if (playerHealth != null)
        {
            if (isAttacking)
            {
                if (insideDamageZone && !tookDamage && !playerXP.levelingUp)
                {
                    Debug.Log("Enemy Attacking");
                    tookDamage = true;
                    playerHealth.PlayerTakeDamage(attackDamage);
                }
            }
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHealth == null)
            {
                playerHealth = collision.GetComponent<PlayerHealth>();
            }
            insideDamageZone = true;
            Debug.Log("Player in Enemy Danger Zone");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            insideDamageZone = false;
        }
    }

}
