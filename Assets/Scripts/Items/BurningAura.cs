using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;

public class BurningAura : MonoBehaviour
{
    [SerializeField] float radiusSize, burnCooldown, burnTime;
    ChosenCardEventHandler cardInfo;
    [SerializeField] bool isBurning, tookBurn, cooldownFinish;
    [SerializeField] float burnIframe;
    [SerializeField] GameObject burningSprite;
    [SerializeField] float burnDamage;

    [SerializeField] Collider2D[] enemies;
    [SerializeField] EnemyHealth[] enemyHealth;

    bool startBurningTime;
    public LayerMask enemyMask;

    private void Awake()
    {
        cooldownFinish = false;
        cardInfo = GetComponent<ChosenCardEventHandler>();
    }


    private void Update()
    {
        if (cardInfo.cardLeveledUp)
        {
            cardInfo.Lvl++;
            burnDamage = (burnDamage + 10);
            cardInfo.cardLeveledUp = false;
        }

        if (cardInfo.Lvl > 0)
        {
            if (!cooldownFinish)
            {
                tookBurn = false;
                StartCoroutine(BurnCooldown());
            }

            if (isBurning)
            {
                StartCoroutine(BurningTime());
                StartCoroutine(TakingBurn());
            }
        }


    }


    void  isInAuraRadius()
    {
        enemies = Physics2D.OverlapCircleAll(transform.position, radiusSize, enemyMask);
        enemyHealth = new EnemyHealth[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyHealth[i] = enemies[i].GetComponent<EnemyHealth>();
            enemyHealth[i].EnemyTakeDamage(burnDamage);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusSize);
    }

    IEnumerator BurnCooldown()
    {
        yield return new WaitForSeconds(burnCooldown);
        burningSprite.SetActive(true);
        isBurning = true;

        cooldownFinish = true;
    }

    IEnumerator BurningTime()
    {
   
        yield return new WaitForSeconds(burnTime);
        burningSprite.SetActive(false);
        isBurning = false;
        cooldownFinish = false;

    }

    IEnumerator TakingBurn()
    {
        if (!tookBurn)
        {
            tookBurn = true;
            isInAuraRadius();
            yield return new WaitForSeconds(burnIframe);
            tookBurn = false;
        }

    }

}
