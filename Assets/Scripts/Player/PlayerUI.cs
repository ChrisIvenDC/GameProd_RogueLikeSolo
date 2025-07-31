using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider XPSlider;
    [SerializeField] GameObject lowHealthUI;



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
    public void UpdatePlayerXPBar(float curretValue, float maxValue)
    {
        XPSlider.value = curretValue / maxValue;
    }
    public void UpdatePlayerHealthBar(float curretValue, float maxValue)
    {
        healthSlider.value = curretValue / maxValue;
    }
}
