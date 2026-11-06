using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Health playerHealth;

    private void OnEnable()
    {
        playerHealth.OnHealthAmount += UpdateHealthBar;
    }

    private void Start()
    {
        UpdateHealthBar(
            playerHealth.CurrentHealth,
            playerHealth.MaxHealth
        );
    }

    private void OnDisable()
    {
        playerHealth.OnHealthAmount -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthSlider.value = maxHealth > 0
            ? (float)currentHealth / maxHealth
            : 0f;
    }
}