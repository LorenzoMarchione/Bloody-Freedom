using UnityEngine;
using System;

public class PlayerStamina : MonoBehaviour
{
    public event Action OnStaminaChanged;

    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float currentStamina;

    [SerializeField] private float staminaCostPerSecond = 20f;
    [SerializeField] private float staminaRecoveryPerSecond = 15f;

    public float CurrentStamina => currentStamina;
    public float MaxStamina => maxStamina;

    private bool estaCorriendo;

    private void Start()
    {
        currentStamina = maxStamina;
    }

    private void Update()
    {
        if (estaCorriendo && currentStamina > 0)
        {
            currentStamina -= staminaCostPerSecond * Time.deltaTime;

            if (currentStamina < 0)
            {
                currentStamina = 0;
            }

            OnStaminaChanged?.Invoke();
        }
        else if (!estaCorriendo && currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryPerSecond * Time.deltaTime;

            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }

            OnStaminaChanged?.Invoke();
        }
    }

    public void SetRunning(bool running)
    {
        estaCorriendo = running;
    }
}