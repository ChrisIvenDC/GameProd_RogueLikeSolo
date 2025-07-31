using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    EnemyHealth enemyHealth;
    [SerializeField]GameObject[] XP;
    [SerializeField]float xpJumpHeight;
    int xpAmount;
    public bool isEnemyDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
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
            foreach (GameObject go in XP)
            {
                GameObject newXP = Instantiate(go, this.transform.position, Quaternion.identity);
                
                Rigidbody2D rb = newXP.GetComponent<Rigidbody2D>();
                rb.linearVelocityY = xpJumpHeight;
            }


            Destroy(this.gameObject); //object Pooling + add Animation/ Courutines
        }
    }

}
