using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    EnemyHealth enemyHealth;
    [SerializeField]GameObject[] xPObj;
    [SerializeField] XP xp;
    [SerializeField]float xpJumpHeight;
    [SerializeField] PlayerXP playerXP;
    public bool isEnemyDead;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerXP>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Start()
    {
        isEnemyDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemyDead)
        {
            foreach (GameObject go in xPObj)
            {
                GameObject newXP = Instantiate(go, this.transform.position, Quaternion.identity);

                Rigidbody2D rb = newXP.GetComponent<Rigidbody2D>();
                xp = newXP.GetComponent<XP>();
                xp.xpAmount = xp.xpAmount + (Mathf.Round(playerXP.lvl) * (xp.xpAmount * 0.5f));

                rb.linearVelocityY = xpJumpHeight;
            }


            Destroy(this.gameObject); //object Pooling + add Animation/ Courutines
        }
    }

}
