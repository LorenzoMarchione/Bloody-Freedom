using UnityEngine;
using UnityEngine.Audio;

public class CambiarMusicaZona : MonoBehaviour
{
    public AudioMixerSnapshot snapshotACambiar;
    public float tiempoTransicion = 2.0f; // Tiempo de fundido (fade) elegido a propósito

    private void OnTriggerEnter(Collider other)
    {
        // Asegúrate de que tu personaje principal tenga la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            snapshotACambiar.TransitionTo(tiempoTransicion);
        }
    }
}