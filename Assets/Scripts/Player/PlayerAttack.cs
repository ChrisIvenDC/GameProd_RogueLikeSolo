using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.UI;

public class PlayerAttack : MonoBehaviour
{
    public bool attacking,attackOnce;
    public float attackSpeed = 1f;

    public float playerAttackDamage;
    public bool attackAnimatorPlayed;

    Animator animator;

    private void Awake()
    {
        animator = transform.parent.GetComponent<Animator>();
    }
    void Start()
    {
        attackAnimatorPlayed = false;
        attacking = false;
    }

    void Update()
    {

        Attack();
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!attackOnce)
            {
                animator.SetFloat("attackSpeed", attackSpeed); // call this everytime attack speed is updated
                StartCoroutine(WaitForAttackSpeed());
            }
        }
    }

    IEnumerator WaitForAttackSpeed()
    {

        attackOnce = true;
        attacking = true;
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(attackSpeed/ (Mathf.Pow(attackSpeed+ 0.2f, 3.5f)));

        animator.SetBool("isAttacking", false);
        attacking = false;


        yield return new WaitForSeconds(attackSpeed/ (Mathf.Pow(attackSpeed, 2f)));
        attackOnce = false;
    }

}