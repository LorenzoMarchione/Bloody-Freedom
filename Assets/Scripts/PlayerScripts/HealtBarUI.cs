//using UnityEngine;
//using UnityEngine.UI;

//public class HealtBarUI : MonoBehaviour
//{
//    [SerializeField] private Image relleno;
//    [SerializeField] private PlayerHealth playerHealth;

//    private void Start()
//    {
//        ActualizarBarra();

//        playerHealth.OnDamaged += ActualizarBarra;
//        playerHealth.OnHeal += ActualizarBarra;
//    }

//    private void OnDestroy()
//    {
//        if (playerHealth != null)
//        {
//            playerHealth.OnDamaged -= ActualizarBarra;
//            playerHealth.OnHeal -= ActualizarBarra;
//        }
//    }

//    private void ActualizarBarra()
//    {
//        relleno.fillAmount = (float)playerHealth.CurrentHealth / playerHealth.MaxHealth;
//    }
//}


using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealtBarUI : MonoBehaviour
{
    [SerializeField] private Image relleno;
    [SerializeField] private Health playerHealth;

    private IEnumerator Start()
    {
        if (relleno == null || playerHealth == null)
        {
            Debug.LogError("Falta asignar el relleno o el Health del jugador.", this);
            yield break;
        }

        playerHealth.OnHealthAmount += ActualizarBarra;

        // Esperar a que Health inicialice la vida.
        yield return null;

        ActualizarBarra(
            playerHealth.CurrentHealth,
            playerHealth.MaxHealth
        );
    }

    private void ActualizarBarra(int actual, int maxima)
    {
        relleno.fillAmount = maxima > 0
            ? Mathf.Clamp01((float)actual / maxima)
            : 0f;
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHealthAmount -= ActualizarBarra;
    }
}