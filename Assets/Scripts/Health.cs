using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnHeal;
    public event Action OnDamaged;
    public event Action OnDeath;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void ChangeHealth(int health)
    {
        currentHealth += health;
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        if(health > 0)
            OnHeal?.Invoke();
        else if(currentHealth <= 0)
            OnDeath?.Invoke();
        else 
            OnDamaged?.Invoke();
    }
}
