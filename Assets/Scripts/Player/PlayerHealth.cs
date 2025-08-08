using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerMaxHealth = 200;
    public float playerHealth;
    [SerializeField] PlayerUI playerUI;
    public bool playerTookDamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerUI = GetComponent<PlayerUI>();
    }

    private void Start()
    {
        playerTookDamage = false;
        playerHealth = playerMaxHealth;
        playerUI.UpdatePlayerHealthBar(playerHealth, playerMaxHealth);
    }

    public void PlayerTakeDamage(float damageValue)
    {
        playerHealth -= damageValue;
        playerTookDamage = true;
        playerUI.UpdatePlayerHealthBar(playerHealth, playerMaxHealth);
        playerUI.LowHealthUI(playerHealth, playerMaxHealth);
    }

    public void PlayerHealed(float healAmount) 
    {
        playerHealth += healAmount;
        playerUI.UpdatePlayerHealthBar(playerHealth, playerMaxHealth);
        playerUI.LowHealthUI(playerHealth, playerMaxHealth);
    }
}
