using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyHealth : MonoBehaviour
{
    public float enemyMaxHealth = 200;
    public float enemyHealth;

    [SerializeField] EnemyUI enemyUI;
    [SerializeField] EnemyDeath enemyDeath;
    [SerializeField] PlayerAttack playerAttack;
    bool enemyTookDamage = false;
    bool inPlayerAttackRadius;
    private void Awake()
    {
        playerAttack = GameObject.FindGameObjectWithTag("PlayerAttack").GetComponent<PlayerAttack>();
        enemyDeath = GetComponent<EnemyDeath>();    
        enemyUI = GetComponentInChildren<EnemyUI>();
    }

    private void Update()
    {
        EnemyAttackedByPlayerOnce(); //we can make a new script for the enemyMask taking damage types
        if (enemyHealth <= 0)
        {
            enemyDeath.isEnemyDead = true;
        }
    }

    private void Start()
    {
        enemyTookDamage = false;

        enemyHealth = enemyMaxHealth;
        enemyUI.UpdateEnemyHealthBar(enemyHealth, enemyMaxHealth);
    }


    public void EnemyTakeDamage(float damageValue)
    {
        enemyHealth -= damageValue;
        enemyUI.UpdateEnemyDamageTook(damageValue);
        enemyUI.UpdateEnemyHealthBar(enemyHealth, enemyMaxHealth);
    }

    private void EnemyAttackedByPlayerOnce()
    {
        if (playerAttack.attacking && inPlayerAttackRadius)
        {
            if (!enemyTookDamage)
            {
                EnemyTakeDamage(playerAttack.playerAttackDamage);
                enemyTookDamage = true;
            }

        }
        else
        {
            enemyTookDamage = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // for getting hit once only
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            Debug.Log("playerattack in radius");
            inPlayerAttackRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            Debug.Log("playerattack not in radius");
            inPlayerAttackRadius = false;

        }
    }


}
