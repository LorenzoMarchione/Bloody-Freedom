using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Health playerHealth;

    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHealthBar;
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
        playerHealth.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthSlider.value = maxHealth > 0
            ? currentHealth / maxHealth
            : 0f;
    }
}