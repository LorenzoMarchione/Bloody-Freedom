using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnHeal;
    public event Action OnDamaged;
    public event Action OnDeath;
    public event Action<int, int> OnHealthAmount;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    private void Start() => currentHealth = maxHealth;
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        if(amount > 0)
            OnHeal?.Invoke();
        else if(currentHealth <= 0)
            OnDeath?.Invoke();
        else
            OnDamaged?.Invoke();

        OnHealthAmount?.Invoke(currentHealth, maxHealth);
    }
}
