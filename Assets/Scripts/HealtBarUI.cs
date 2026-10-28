using UnityEngine;
using UnityEngine.UI;

public class HealtBarUI : MonoBehaviour
{
    [SerializeField] private Image relleno;
    [SerializeField] private PlayerHealth playerHealth;

    private void Start()
    {
        ActualizarBarra();

        playerHealth.OnDamaged += ActualizarBarra;
        playerHealth.OnHeal += ActualizarBarra;
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDamaged -= ActualizarBarra;
            playerHealth.OnHeal -= ActualizarBarra;
        }
    }

    private void ActualizarBarra()
    {
        relleno.fillAmount = (float)playerHealth.CurrentHealth / playerHealth.MaxHealth;
    }
}