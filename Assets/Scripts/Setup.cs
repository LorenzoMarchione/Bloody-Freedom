using UnityEngine;

public class Setup : MonoBehaviour
{
    public GameObject bloque;
    public int cantidad = 60;
    public float radio = 5f;
    public float altura = 0f;

    void Start()
    {
        CrearPared();
    }

    void CrearPared()
    {
        if (bloque == null)
        {
            Debug.LogWarning("Por favor, asigna un prefab en la variable 'bloque'.");
            return;
        }

        for (int i = 0; i < cantidad; i++)
        {
            float angulo = i * (360f / cantidad);
            float rad = angulo * Mathf.Deg2Rad;

            float x = transform.position.x + Mathf.Cos(rad) * radio;
            float z = transform.position.z + Mathf.Sin(rad) * radio;

            Vector3 posicion = new Vector3(x, altura, z);

            GameObject nuevoBloque = (GameObject)Instantiate(bloque, posicion, Quaternion.identity);
            nuevoBloque.transform.LookAt(new Vector3(transform.position.x, nuevoBloque.transform.position.y, transform.position.z));
        }
    }
}