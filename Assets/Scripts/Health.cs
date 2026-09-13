using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnHeal;
    public event Action OnDamaged;
    public event Action OnDeath;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private void Start() => currentHealth = maxHealth;
    public void ChangeHealth(int amount)
    {

        Debug.Log(
            $"Health de {gameObject.name} | ID: {GetInstanceID()} | " +
            $"Antes: {currentHealth} | Cambio: {amount}"
        );
        currentHealth += amount;
        Debug.Log(
            $"Health de {gameObject.name} | ID: {GetInstanceID()} | " +
            $"Después: {currentHealth}"
        );
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        if(amount > 0)
            OnHeal?.Invoke();
        else if(currentHealth <= 0)
            OnDeath?.Invoke();
        else
        {

            Debug.Log(currentHealth);
            OnDamaged?.Invoke();
        }
    }
}
