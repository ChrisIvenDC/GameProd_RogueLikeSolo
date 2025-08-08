using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class HealingPerSecond : MonoBehaviour
{
    PlayerHealth playerHealth;
    ChosenCardEventHandler cardInfo;
    PlayerLevelingUp playerLevelingUp;

    [SerializeField]float healthRegenCooldown, healingInterval;
    [SerializeField]float healingAmount;
    [SerializeField]bool ishealing, tookHeal;
    private void Awake()
    {
        cardInfo = GetComponent<ChosenCardEventHandler>();
        playerHealth = transform.parent.transform.parent.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (cardInfo.cardLeveledUp)
        {
            cardInfo.Lvl++;
            healingAmount = (healingAmount + 0.5f);
            cardInfo.cardLeveledUp = false;
        }

        if (cardInfo.Lvl > 0 && playerHealth.playerHealth <= playerHealth.playerMaxHealth)
        {
            if (playerHealth.playerTookDamage)
            {
                ishealing = false;
                StartCoroutine(RestartHealingTimer());
            }

            if (ishealing)
            {
                StartCoroutine(PlayerHealingInterval());
            }
        }

    }

    IEnumerator RestartHealingTimer()
    {
        playerHealth.playerTookDamage = false;
        Debug.Log("playertookdamage, restarting cooldown");
        yield return new WaitForSeconds(healthRegenCooldown);
        Debug.Log("Healing is true, should start healing");
        ishealing = true;
    }

    IEnumerator PlayerHealingInterval()
    {
        if (!tookHeal)
        {
            tookHeal = true;
            playerHealth.PlayerHealed(healingAmount);
            Debug.Log("should heal Player");
            yield return new WaitForSeconds(healingInterval);
            tookHeal = false;
        }
    }
}
