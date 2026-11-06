using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [Tooltip("Velocidad de rotación alrededor del modelo.")]
    public float rotationSpeed = 5f;

    void Update()
    {
        // Rota el objeto pivote lentamente alrededor del eje Y (hacia los lados)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}