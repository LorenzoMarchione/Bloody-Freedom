using UnityEngine;

public class ControladorPasos : MonoBehaviour
{
    public Animator animator;
    // Referencia al Audio Random Container o AudioSource que configuraste
    public AudioSource audioSourcePasos;

    [Header("Configuración de pasos")]
    public float intervaloPasos = 0.5f; // Tiempo entre paso y paso al caminar
    private float temporizador;

    void Update()
    {
        // Obtenemos el valor del parámetro "speed" del Animator
        float velocidadActual = animator.GetFloat("Speed");

        // Si el personaje se está moviendo (speed mayor a un umbral pequeño)
        if (velocidadActual > 0.1f)
        {
            temporizador -= Time.deltaTime;

            if (temporizador <= 0f)
            {
                // Reproduce el sonido (si usas el Audio Random Container o AudioSource)
                audioSourcePasos.Play();

                // Reinicia el temporizador (puedes acortarlo si corre más rápido basándote en la velocidad)
                temporizador = intervaloPasos;
            }
        }
        else
        {
            // Si está quieto, reiniciamos el temporizador para que suene de inmediato al volver a caminar
            temporizador = 0f;
        }
    }
}