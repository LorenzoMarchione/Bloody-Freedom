using UnityEngine;

public class Moneda : MonoBehaviour
{
    private AudioSource audioSource;
    private bool fueRecogida = false;

    void Start()
    {
        // Obtenemos el AudioSource que está en este mismo objeto
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos que sea el jugador quien toca la moneda y que no haya sido recogida ya
        if (other.CompareTag("Player") && !fueRecogida)
        {
            fueRecogida = true;

            // Reproduce el sonido aleatorio de la moneda
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Desactivamos el renderizador y el colisionador para que la moneda "desaparezca" visualmente de inmediato
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            // Destruimos el objeto de la moneda después de que termine de sonar el audio (o un pequeño margen de seguridad)
            float tiempoEspera = audioSource != null && audioSource.clip != null ? audioSource.clip.length : 1f;
            Destroy(gameObject, tiempoEspera);
        }
    }
}