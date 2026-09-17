using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    public GameObject prefabEnemigo; 
    public float intervaloSpawn = 3f; 
    public List<Transform> puntosSpawn; 

    void Start()
    {
        InvokeRepeating("GenerarEnemigo", 5f, intervaloSpawn);
    }

    void GenerarEnemigo()
    {
        if (prefabEnemigo == null || puntosSpawn.Count == 0)
        {
            Debug.LogWarning("Faltaa asignar el prefab o los puntos de spawn");
            return;
        }
        
        // Elegir un punto aleatorio de la lista
        int indiceAleatorio = Random.Range(0, puntosSpawn.Count);
        Transform puntoElegido = puntosSpawn[indiceAleatorio];

        // Crear el enemigo en la posición y rotación del punto elegido
        Instantiate(prefabEnemigo, puntoElegido.position, puntoElegido.rotation);
    }
}