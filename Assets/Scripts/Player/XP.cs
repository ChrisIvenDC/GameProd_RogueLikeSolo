using Unity.VisualScripting;
using UnityEngine;

public class XP : MonoBehaviour
{
    public float xpAmount;
    PlayerXP playerXP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerXP = collision.gameObject.GetComponent<PlayerXP>();
            playerXP.PlayerTookXP(xpAmount);
            Destroy(this.gameObject); //Object Pooling Better
        }

    }

}
