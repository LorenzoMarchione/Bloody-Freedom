
using UnityEngine;

public class ControladorPasos : MonoBehaviour
{
    [Header("Animator")]
    public Animator animator;

    [Header("Audio Sources")]
    public AudioSource audioConcreto;
    public AudioSource audioTierra;
    public AudioSource audioMadera;

    [Header("Deteccion del suelo")]
    public Transform puntoSuelo;
    public float distanciaSuelo = 0.5f;
    public LayerMask capasSuelo;

    [Header("Configuracion de pasos")]
    public float intervaloPasos = 0.5f;

    private float temporizador;

    void Update()
    {
        if (animator == null || puntoSuelo == null)
            return;

        if (animator.GetFloat("Speed") > 0.1f)
        {
            temporizador -= Time.deltaTime;

            if (temporizador <= 0f)
            {
                DetectarSuelo();
                temporizador = intervaloPasos;
            }
        }
        else
        {
            temporizador = 0f;
        }
    }

    void DetectarSuelo()
    {
        Debug.DrawRay(
            puntoSuelo.position,
            Vector3.down * distanciaSuelo,
            Color.red,
            1f
        );

        if (Physics.Raycast(
            puntoSuelo.position,
            Vector3.down,
            out RaycastHit hit,
            distanciaSuelo,
            capasSuelo))
        {
            int layerDetectado = hit.collider.gameObject.layer;

            if (layerDetectado == LayerMask.NameToLayer("Concreto"))
            {
                if (audioConcreto != null)
                {
                    audioConcreto.Play();
                   // Debug.Log("Paso sobre CONCRETO");
                }
            }
            else if (layerDetectado == LayerMask.NameToLayer("Tierra"))
            {
                if (audioTierra != null)
                {
                    audioTierra.Play();
                   // Debug.Log("Paso sobre TIERRA");
                }
            }
            else if (layerDetectado == LayerMask.NameToLayer("Madera"))
            {
                if (audioMadera != null)
                {
                    audioMadera.Play();
                   // Debug.Log("Paso sobre MADERA");
                }
            }
        }
    }
}

