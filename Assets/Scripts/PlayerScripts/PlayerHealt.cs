using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public event Action OnHeal;
    public event Action OnDamaged;
    public event Action OnDeath;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        Debug.Log("Vida actual: " + currentHealth);
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath?.Invoke();
        }
        else if (amount > 0)
        {
            OnHeal?.Invoke();
        }
        else if (amount < 0)
        {
            OnDamaged?.Invoke();
        }
    }
}