using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider XPSlider;
    [SerializeField] GameObject lowHealthUI;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject retryButton;

    private void Awake()
    {
        retryButton.GetComponent<Button>().onClick.AddListener(() => RetryButton());
    }


    public void LowHealthUI(float currentPlayerHealth, float playerMaxHealth)
    {
        if (currentPlayerHealth < (playerMaxHealth * .2f))
        {
            lowHealthUI.SetActive(true);
        }

        else
        {
            lowHealthUI.SetActive(false);
        }

    }


    private void RetryButton()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdatePlayerXPBar(float curretValue, float maxValue)
    {
        XPSlider.value = curretValue / maxValue;
    }
    public void UpdatePlayerHealthBar(float curretValue, float maxValue)
    {
        healthSlider.value = curretValue / maxValue;
        if (curretValue <=0)
        {
            deathScreen.SetActive(true);
        }
    }
}
