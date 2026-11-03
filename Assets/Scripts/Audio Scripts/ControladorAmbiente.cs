using UnityEngine;

public class ControladorAmbiente : MonoBehaviour
{
    public AudioSource ambienteInterior;
    public AudioSource ambienteExterior;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Al entrar al interior: apagamos el exterior y encendemos el interior
            if (ambienteExterior != null) ambienteExterior.Stop();
            if (ambienteInterior != null && !ambienteInterior.isPlaying) ambienteInterior.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Al salir al exterior: apagamos el interior y encendemos el exterior
            if (ambienteInterior != null) ambienteInterior.Stop();
            if (ambienteExterior != null && !ambienteExterior.isPlaying) ambienteExterior.Play();
        }
    }
}